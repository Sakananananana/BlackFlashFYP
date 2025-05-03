using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonSO _dungeonSO;
    [SerializeField] private VoidEventChannelSO _resetDungeonLayerOnPlayerDeath;
    private Vector2Int _endPos;


    private void OnEnable()
    {
        _resetDungeonLayerOnPlayerDeath.OnEventRaised += ResetDungeonLayer;
    }

    private void OnDisable()
    {
        _resetDungeonLayerOnPlayerDeath.OnEventRaised -= ResetDungeonLayer;
    }

    private void ResetDungeonLayer()
    { 
        _dungeonSO.ResetDungeonProgress();
    }

    //Generate Async later 
    void Start() => GenerateDungeon();

    private void GenerateDungeon()
    {
        _dungeonSO.GetDungeonPath();
        SpawnRooms();
    }



    private void SpawnRooms()
    {
        GetFinalRoom();

        foreach (var roomToSpawn in _dungeonSO.DungeonLayout)
        {
            Vector2Int gridPos = roomToSpawn.Key;
            Vector3 spawnPos = new Vector3(gridPos.x * 20, gridPos.y * 12, 0);
            RoomManager room = Instantiate(roomToSpawn.Value.RoomPrefab, spawnPos, Quaternion.identity);
            room.transform.SetParent(transform);

            if (gridPos == Vector2Int.zero)
            {
                room.IsStartRoom = true;
            }

            if (gridPos == _endPos)
            {
                room.IsEndRoom = true;
            }
        }
    }

    private void GetFinalRoom()
    {
        float longestDist = 0;
        float currentDist;

        foreach (var room in _dungeonSO.DungeonLayout)
        {
            currentDist = (room.Key - Vector2Int.zero).magnitude;
            if (currentDist > longestDist)
            {
                longestDist = currentDist;
                _endPos = room.Key;
            }
        }
    }
}
