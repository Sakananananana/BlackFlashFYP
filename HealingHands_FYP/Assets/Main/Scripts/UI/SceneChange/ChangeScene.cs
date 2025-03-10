using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameSceneSO _sceneToLoad;
    [SerializeField] private LoadEventChannelSO _raiseLoadEvent;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _raiseLoadEvent.OnLoadingRequested(_sceneToLoad);
        }
    }
}
