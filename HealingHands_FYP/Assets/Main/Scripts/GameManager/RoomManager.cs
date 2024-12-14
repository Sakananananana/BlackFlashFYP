using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private bool _inFight = false;
    [SerializeField] private bool _enteredRoom = false;
    [SerializeField] private List<EnemyToSpawn> _enemyToSpawn = new List<EnemyToSpawn>();
    [SerializeField] private Collider2D[] _colliders;
    [SerializeField] private Transform _enemyPool;

    //inherit from game manager
    //game manager have an event to call when a fights end and can collect things

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_enteredRoom == false)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].isTrigger = false;
            }

            for (int i = 0; i < _enemyToSpawn.Count; i++)
            {
                var enemySpawned = Instantiate(_enemyToSpawn[i].Enemy, _enemyToSpawn[i].Location, Quaternion.identity);
                enemySpawned.transform.SetParent(_enemyPool);
            }

            _enteredRoom = true;
            _inFight = true;

            StartCoroutine(BattleFinished());
        }
    }

    private IEnumerator BattleFinished()
    {
        while (_enemyPool.childCount > 0)
        { 
            yield return null;
        }

        _inFight = false;
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].isTrigger = true;
        }

    }
}

[Serializable]
public class EnemyToSpawn
{
    public GameObject Enemy;
    public Vector2 Location;
}
