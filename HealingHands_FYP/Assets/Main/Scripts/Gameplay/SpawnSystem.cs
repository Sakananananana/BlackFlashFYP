using System;
using UnityEngine;
using PlayerInputSystem;

public class SpawnSystem : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private Protagonist _playerPrefab;

    [Header("Listening to...")]
    [SerializeField] private VoidEventChannelSO _onSceneReady;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += SpawnProtagonist;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= SpawnProtagonist;   
    }

    //set path?

    private void SpawnProtagonist()
    {
        //get the previous path (path = from where to current location)
        //spawn player at the location
        _inputReader.SetGameplay();
    }

}
