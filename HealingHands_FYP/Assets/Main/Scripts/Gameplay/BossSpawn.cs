using UnityEngine;
using System.Collections;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyBoss;
    [SerializeField] private GameObject _exit;

    [SerializeField] private VoidEventChannelSO _onSceneReady;
    [SerializeField] private BossHealthUIItem _bossUIItems;
    [SerializeField] private BossEventChannelSO _updateBossUIItems;

    [SerializeField] private BoolEventChannelSO _onCombatEvent;

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
        _onCombatEvent.RaiseEvent(true);
        StartCoroutine(SpawnExitOnBossDeath());
    }


    private IEnumerator SpawnExitOnBossDeath()
    {

        while (_enemyBoss != null)
        yield return null;

        _onCombatEvent.RaiseEvent(false);
        _exit.SetActive(true);
        PlayerPrefs.SetInt("Level1BossDefeated", 1);
        PlayerPrefs.Save();
    }
}
