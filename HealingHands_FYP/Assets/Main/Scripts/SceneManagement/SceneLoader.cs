using UnityEngine;
using System.Collections;
using PlayerInputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameSceneSO _gameplayScene;
    [SerializeField] private InputReader _inputReader;

    [Header("Listening to...")]
    [SerializeField] private LoadEventChannelSO _onEditorStartup;
    [SerializeField] private LoadEventChannelSO _loadLocation;
    [SerializeField] private LoadEventChannelSO _loadMenu;

    [Header("Broadcasting on...")]
    [SerializeField] private VoidEventChannelSO _onSceneReady; //later pick up by spawn
    //[SerializeField] private BoolEventChannelSO _toggleLoadingScreen;

    //parameter for scene
    private GameSceneSO _loadedScene;
    private GameSceneSO _sceneToLoad;

    private AsyncOperationHandle<SceneInstance> _gameplaySceneLoadingOpHandle;
    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;

    private SceneInstance _gameplaySceneInstance = new SceneInstance();

    private void OnEnable()
    {
#if UNITY_EDITOR
        _onEditorStartup.OnLoadingRequested += EditorStartupMethod;
#endif

        _loadLocation.OnLoadingRequested += LoadLocation;
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        _onEditorStartup.OnLoadingRequested -= EditorStartupMethod;
#endif

        _loadLocation.OnLoadingRequested -= LoadLocation;
    }

#if UNITY_EDITOR
    private void EditorStartupMethod(GameSceneSO scene)
    { 
        _loadedScene = scene;

        if (_loadedScene.sceneType == GameSceneSO.GameSceneType.Location)
        {
            _gameplaySceneLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _gameplaySceneLoadingOpHandle.WaitForCompletion();
            _gameplaySceneInstance = _gameplaySceneLoadingOpHandle.Result;
        }
    }
#endif

    private void LoadLocation(GameSceneSO scene)
    {
        _sceneToLoad = scene;

        //to ensure that the Gameplay Manager is loaded before anything prevent error!
        if (_gameplaySceneInstance.Scene == null || !_gameplaySceneInstance.Scene.isLoaded)
        {
            _gameplaySceneLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _gameplaySceneLoadingOpHandle.Completed += OnGameplayManagerLoaded;
        }
        else
        { StartCoroutine(UnloadPreviousScene()); }
    }

    private void LoadMenu(GameSceneSO scene)
    { 
        
    }

    private void OnGameplayManagerLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _gameplaySceneInstance = obj.Result;
        StartCoroutine(UnloadPreviousScene());
    }

    private IEnumerator UnloadPreviousScene()
    {
        _inputReader.DisableAllInput();

        yield return new WaitForSeconds(0.5f);

        if (_loadedScene != null)
        {
            if (_loadedScene.sceneReference.OperationHandle.IsValid())
            {
                _loadedScene.sceneReference.UnLoadScene();
            }
            else
            {
                SceneManager.UnloadSceneAsync(_loadedScene.sceneReference.editorAsset.name);
            }
        }

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _loadedScene = _sceneToLoad;

        Scene newScene = obj.Result.Scene;
        SceneManager.SetActiveScene(newScene);

        //Later Move to Spawn System Ensure Protagonist is Spawned before enabling
        _inputReader.SetGameplay();
    }

    private void ExitGame()
    { 
        Application.Quit();
    }
}
