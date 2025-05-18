using UnityEngine;
using System.Collections;
using PlayerInputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;

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
    [SerializeField] private SceneEventChannelSO _onSceneChange;
    //[SerializeField] private BoolEventChannelSO _toggleLoadingScreen;
    [SerializeField] private FadeEventChannelSO _fadeEvent;
    [SerializeField] private BoolEventChannelSO _showInGameScreenUI;

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
        _loadMenu.OnLoadingRequested += LoadMenu;
        _loadLocation.OnLoadingRequested += LoadLocation;
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        _onEditorStartup.OnLoadingRequested -= EditorStartupMethod;
#endif

        _loadMenu.OnLoadingRequested -= LoadMenu;
        _loadLocation.OnLoadingRequested -= LoadLocation;
    }

#if UNITY_EDITOR
    private void EditorStartupMethod(GameSceneSO scene)
    { 
        _loadedScene = scene;

        if (_loadedScene.sceneType == GameSceneSO.GameSceneType.Location_BossRoom || 
            _loadedScene.sceneType == GameSceneSO.GameSceneType.Location_Village || 
            _loadedScene.sceneType == GameSceneSO.GameSceneType.Location_Dungeon)
        {
            _gameplaySceneLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            _gameplaySceneLoadingOpHandle.Completed += OnGameplaySceneLoaded;
        }
        else
        {
            _onSceneReady.RaiseEvent();
            _onSceneChange.RaiseEvent(_loadedScene.sceneType);
        }
    }

    private void OnGameplaySceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _gameplaySceneInstance = obj.Result;
        _onSceneReady.RaiseEvent();
        _onSceneChange.RaiseEvent(_loadedScene.sceneType);
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
        { 
            StartCoroutine(UnloadPreviousScene()); 
        }
    }

    private void LoadMenu(GameSceneSO menuToLoad)
    {
        //unload previous scene and gameplay scene
        _sceneToLoad = menuToLoad;

        if (_gameplaySceneInstance.Scene != null
            && _gameplaySceneInstance.Scene.isLoaded)
            Addressables.UnloadSceneAsync(_gameplaySceneLoadingOpHandle);

        StartCoroutine(UnloadPreviousScene());
    }

    private void OnGameplayManagerLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _gameplaySceneInstance = obj.Result;
        StartCoroutine(UnloadPreviousScene());
    }

    private IEnumerator UnloadPreviousScene()
    {
        _inputReader.DisableAllInput();
        _showInGameScreenUI.RaiseEvent(false);
        _fadeEvent.FadeIn(1f);

        yield return new WaitForSeconds(1f);

        if (_loadedScene != null)
        {
            if (_loadedScene.sceneReference.OperationHandle.IsValid())
            {
                var unloadHandle = _loadedScene.sceneReference.UnLoadScene();
                yield return unloadHandle;
            }
#if UNITY_EDITOR
            else
            {

                var unloadOp = SceneManager.UnloadSceneAsync(_loadedScene.sceneReference.editorAsset.name);
                if (unloadOp != null)
                    yield return unloadOp;
            }
#endif
            LoadNewScene();
        }
        else
        { LoadNewScene(); }
    }

    private void LoadNewScene()
    {
        if (_sceneToLoad.sceneReference.OperationHandle.IsValid())
            return;

        _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        _loadingOperationHandle.Completed += OnNewSceneLoaded;
    }

    private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        _loadedScene = _sceneToLoad;

        Scene newScene = obj.Result.Scene;
        SceneManager.SetActiveScene(newScene);

        _fadeEvent.FadeOut(1f);

        _onSceneReady.RaiseEvent();
        _onSceneChange.RaiseEvent(_loadedScene.sceneType);

        if (_loadedScene.sceneType == GameSceneSO.GameSceneType.Cutscene)
            _showInGameScreenUI.RaiseEvent(false);
        else
            _showInGameScreenUI.RaiseEvent(true);
    }


    private void ExitGame()
    { 
        Application.Quit();
    }
}
