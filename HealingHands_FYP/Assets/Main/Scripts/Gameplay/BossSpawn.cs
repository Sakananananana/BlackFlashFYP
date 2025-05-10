using UnityEngine;
using System.Collections;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyBoss;
    [SerializeField] private GameObject _exit;

    [SerializeField] private VoidEventChannelSO _onSceneReady;
    [SerializeField] private BossHealthUIItem _bossUIItems;
    [SerializeField] private BossEventChannelSO _updateBossUIItems;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += EnableBossOnSceneReady;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= EnableBossOnSceneReady;
    }

    private void EnableBossOnSceneReady()
    {
        _enemyBoss.SetActive(true);
        _updateBossUIItems.RaiseEvent(_bossUIItems);
        StartCoroutine(SpawnExitOnBossDeath());
    }

    private IEnumerator SpawnExitOnBossDeath()
    {

        while (_enemyBoss != null)
        yield return null;

        _exit.SetActive(true);
    }
}
