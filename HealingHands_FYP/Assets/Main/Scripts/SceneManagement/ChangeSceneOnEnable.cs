using UnityEngine;

public class ChangeSceneOnEnable : MonoBehaviour
{
    [SerializeField] private GameSceneSO _sceneToLoad;
    [SerializeField] private LoadEventChannelSO _raiseLoadEvent;

    private void OnEnable()
    {
        _raiseLoadEvent.OnLoadingRequested(_sceneToLoad);
    }

}
