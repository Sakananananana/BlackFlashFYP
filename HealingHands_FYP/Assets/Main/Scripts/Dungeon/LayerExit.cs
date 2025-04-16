using UnityEngine;
using System.Collections.Generic;

public class LayerExit : MonoBehaviour
{
    [SerializeField] private DungeonSO _dungeonSO;
    [SerializeField] private List<GameSceneSO> _sceneToLoad;
    [SerializeField] private LoadEventChannelSO _raiseLoadEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _dungeonSO.DungeonProgress();

            if (_dungeonSO.HasBossFightComplete)
                _raiseLoadEvent.OnLoadingRequested(_sceneToLoad[0]);
            else
                _raiseLoadEvent.OnLoadingRequested(_sceneToLoad[1]);
        }
    }
}
