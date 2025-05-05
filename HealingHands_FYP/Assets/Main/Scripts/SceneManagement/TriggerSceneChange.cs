using UnityEngine;
using PlayerInputSystem;
using UnityEngine.SceneManagement;

public class TriggerSceneChange : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private GameSceneSO _sceneToLoad;
    [SerializeField] private LoadEventChannelSO _raiseLoadEvent;
    [SerializeField] private BoolEventChannelSO _onOpenRoom;
    [SerializeField] private InterectionManager _interactionManager;
    [SerializeField] private TriggerSceneChange _roomSceneChanger;


    public void triggerSceneChange()
    {
        //SceneHistoryManager.Instance?.RecordScene(currentSceneSO); // assign current scene in inspector
        _raiseLoadEvent.OnLoadingRequested(_sceneToLoad);
    }
    void OpenRoomUI()
    {

        _roomSceneChanger.triggerSceneChange();

        _onOpenRoom.RaiseEvent(true);
    }
    private void OnEnable()
    {
        _onOpenRoom.OnEventRaised += OnOpenRoomRequested;
    }

    void OnOpenRoomRequested(bool val)
    {
        if ((val == true))
        {
            _inputReader.InteractEvent += OpenRoomUI;
        }
        else
        {
            _inputReader.InteractEvent -= OpenRoomUI;
        }
    }
    private void OnDisable()
    {
        _onOpenRoom.OnEventRaised -= OnOpenRoomRequested;
    }

    }
