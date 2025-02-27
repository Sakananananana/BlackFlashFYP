using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class EditorStartup : MonoBehaviour
{
    [SerializeField] private GameSceneSO _currentSceneSO;
    [SerializeField] private GameSceneSO _persistentManagerSO;

    [SerializeField] private AssetReference _onEditorStartup;

    private void Start()
    {
        if (!SceneManager.GetSceneByName(_persistentManagerSO.sceneReference.editorAsset.name).isLoaded)
        {
            //Broadcast whether persistentManager is loaded
            _persistentManagerSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += LoadEventChannel;
        }
    }

    //Listen if persistentManager is loaded
    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        //Broadcast if _onEditorStartup is loaded
        _onEditorStartup.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoaded;
    }

    //Listen if _onEditorStartup is loaded, then raise event
    private void OnNotifyChannelLoaded(AsyncOperationHandle<LoadEventChannelSO> obj) 
    {
        if (_currentSceneSO != null)
        {
            obj.Result.RaiseEvent(_currentSceneSO);
        }
    }
}
