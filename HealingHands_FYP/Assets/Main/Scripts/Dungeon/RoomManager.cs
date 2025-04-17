using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Room Manager
public class RoomManager : MonoBehaviour
{
    [Header("Camera Boundary")]
    [SerializeField] private BoxCollider2D _mapBoundaryBox;
    [SerializeField] private Transform _exits;

    [Header("Broadcasting on...")]
    [SerializeField] private ColliderEventChannelSO _onNewRoomEntered;

    [SerializeField] private GameObject _exitsObj;
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [System.NonSerialized] public bool IsStartRoom = false;
    [System.NonSerialized] public bool IsEndRoom = false;
    private int _enemyCount;
    private bool _enteredRoom;
    private Transform _enemyPool;
    private Dictionary<Vector2Int, GameObject> _enemyToSpawn = new Dictionary<Vector2Int, GameObject>();
    private List<Damageable> _damageables = new List<Damageable>();

    private void Start()
    {
        if (_enemyPool == null)
        {
            _enemyPool = new GameObject("Enemy_Pool").transform;
            _enemyPool.SetParent(transform);
        }

        if(IsStartRoom == false)
        PrewarmEnemyPool();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            _onNewRoomEntered.RaiseEvent(_mapBoundaryBox);

            if (_enteredRoom == false)
            {
                for (int i = 0; i < _exits.childCount; i++)
                {
                    _exits.GetChild(i).GetComponent<Collider2D>().isTrigger = false;
                }

                _enteredRoom = true;

                for (int i = 0; i < _enemyPool.childCount; i++)
                {
                    _enemyPool.GetChild(i).gameObject.SetActive(true);
                }

                StartCoroutine(BattleFinished());
            }
        }
    }

    private void PrewarmEnemyPool()
    {
        _enemyCount = Random.Range(1, 4);

        while (_enemyCount > 0)
        {
            Vector2Int spawnPos = new Vector2Int((int)transform.position.x + Random.Range(-6, 7), (int)transform.position.y + Random.Range(-2, 3));

            if (_enemyToSpawn.ContainsKey(spawnPos)) { continue; }

            bool tooClose = false;
            foreach (var obj in _enemyToSpawn)
            { 
                if ((obj.Key - spawnPos).magnitude < 2)
                { 
                    tooClose = true;
                    break; 
                }
            }

            if (tooClose)
                continue;

            GameObject enemyObj = _enemyList[Random.Range(0, _enemyList.Count)];
            _enemyToSpawn.Add(spawnPos, enemyObj);
            _enemyCount--;
        }

        foreach (var item in _enemyToSpawn)
        {
            GameObject enemyObj = Instantiate(item.Value.gameObject, new Vector2(item.Key.x, item.Key.y), Quaternion.identity);
            enemyObj.transform.SetParent(_enemyPool);

            if (enemyObj.TryGetComponent(out Damageable damageable))
                _damageables.Add(damageable);
            
            enemyObj.SetActive(false);
        }
    }

    private IEnumerator BattleFinished()
    {
        while (_damageables.Any(e => !e.IsDead))
        {
            yield return null;
        }

        for (int i = 0; i < _exits.childCount; i++)
        {
            _exits.GetChild(i).GetComponent<Collider2D>().isTrigger = true;
        }

        if (IsEndRoom)
        {
            GameObject exit = Instantiate(_exitsObj, transform.position, Quaternion.identity);
            exit.transform.SetParent(transform);
        }
    }

}
