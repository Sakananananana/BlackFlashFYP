using System;
using UnityEngine;
using PlayerInputSystem;

public class SpawnSystem : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private GameObject _playerPrefab;

    [Header("Listening to...")]
    [SerializeField] private VoidEventChannelSO _onSceneReady;

    [Header("Broadcasting on...")]
    [SerializeField] private TransformEventChannelSO _setCameraPosition;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += SpawnProtagonist;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnProtagonist;   
    }

    private void SpawnProtagonist()
    {
        //spawn player at the location
        GameObject obj = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        _setCameraPosition.RaiseEvent(obj.transform);
        _inputReader.SetGameplay();
    }

}
