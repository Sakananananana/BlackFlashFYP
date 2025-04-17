using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonSO _dungeonSO;
    private Vector2Int _endPos;

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
