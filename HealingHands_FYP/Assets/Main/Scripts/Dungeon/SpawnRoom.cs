using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    [SerializeField] private int _minRoom;
    [SerializeField] private int _maxRoom;
    [SerializeField] private List<RoomData> _roomDataList = new List<RoomData>();

    //private parameters
    private RoomData _startRoom;
    private Vector2Int _startPos = Vector2Int.zero;
    //To store processed room data & spawned position
    private Dictionary<Vector2Int, RoomData> _dungeonLayout = new Dictionary<Vector2Int, RoomData>();
    //To add unprocessed position in grid
    private Queue<Vector2Int> _roomToProcess = new Queue<Vector2Int>();
    
    //Generate Async later 
    void Start() => GenerateDungeon();

    private void GenerateDungeon()
    {
    
        GenerateCorePath();

        foreach (var room in _dungeonLayout)
        {
            _roomToProcess.Enqueue(room.Key);
        }

        while (_roomToProcess.Count > 0)
        {
            Vector2Int currentPos = _roomToProcess.Dequeue();
            RoomData curentRoom = _dungeonLayout[currentPos];

            foreach (Direction exitDir in curentRoom.RoomExits)
            {
                Vector2Int newPos = GetNewPosition(currentPos, exitDir); //Get position of the new room
                Direction requiredEntry = GetOppositeDirection(exitDir);

                if (_dungeonLayout.ContainsKey(newPos)) //Check if this position alrd contains a room
                {
                    RoomData temp = _dungeonLayout[newPos]; //Get the existed room data from the dictionary

                    if (temp.RoomExits.Contains(requiredEntry)) //check if the new pos's room has the entry to the current room
                    {
                        continue;
                    }
                    else
                    {
                        int rand = Random.Range(0, 2);

                        switch (rand)
                        {
                            case 0:
                                {
                                    List<Direction> tempDir = new List<Direction>();
                                    for (int i = 0; i < temp.RoomExits.Count; i++)
                                    {
                                        tempDir.Add(temp.RoomExits[i]);
                                    }
                                    tempDir.Add(requiredEntry);

                                    for (int i = 0; i < _roomDataList.Count; i++)
                                    {
                                        if (AreListsEqual(_roomDataList[i].RoomExits, tempDir))
                                        {
                                            _dungeonLayout[newPos] = _roomDataList[i];
                                            continue;
                                        }
                                    }

                                    break;
                                }

                            case 1:
                                {
                                    List<Direction> tempDirList = new List<Direction>();
                                    for (int i = 0; i < curentRoom.RoomExits.Count; i++)
                                    {
                                        tempDirList.Add(curentRoom.RoomExits[i]);
                                    }
                                    tempDirList.Remove(exitDir);

                                    for (int i = 0; i < _roomDataList.Count; i++)
                                    {
                                        if (AreListsEqual(_roomDataList[i].RoomExits, tempDirList))
                                        {
                                            _dungeonLayout[currentPos] = _roomDataList[i];
                                            _roomToProcess.Enqueue(currentPos);
                                            continue;
                                        }
                                    }

                                    break;
                                }
                        }

                    }
                }
                else
                {
                    List<Direction> tempDirList = new List<Direction>();
                    for (int i = 0; i < curentRoom.RoomExits.Count; i++)
                    {
                        tempDirList.Add(curentRoom.RoomExits[i]);
                    }
                    tempDirList.Remove(exitDir);

                    for (int i = 0; i < _roomDataList.Count; i++)
                    {
                        if (AreListsEqual(_roomDataList[i].RoomExits, tempDirList))
                        {
                            _dungeonLayout[currentPos] = _roomDataList[i];
                            _roomToProcess.Enqueue(currentPos);
                            continue;
                        }
                    }
                }
            }
        }

        InstantiateRooms();
    }

    bool AreListsEqual<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count) return false;

        return list1.All(list2.Contains);        
    }

    //check if the entry has valid path if not replace with other room
    private void GenerateCorePath()
    {
        _startRoom = _roomDataList[Random.Range(0, _roomDataList.Count + 1)];

        _dungeonLayout.Add(_startPos, _startRoom);
        _roomToProcess.Enqueue(_startPos);

        while (_dungeonLayout.Count < _minRoom && _roomToProcess.Count > 0)
        { 
            Vector2Int pos = _roomToProcess.Dequeue();
            RoomData currentRoom = _dungeonLayout[pos];

            foreach (Direction dir in currentRoom.RoomExits.OrderByDescending(d => IsForwardDirection(d)))
            {
                Vector2Int newPos = GetNewPosition(pos, dir);
                if (_dungeonLayout.ContainsKey(newPos))
                { continue; }

                Direction requiredEntry = GetOppositeDirection(dir);
                List<RoomData> validRooms = GetRoomsWithEntry(requiredEntry, RoomType.Normal);

                if (validRooms.Count == 0)
                { continue; }

                validRooms = validRooms.OrderByDescending(r => r.RoomExits.Count).ToList();
                RoomData selectedRoom = validRooms[Random.Range(0, 2)];
                //.First();

                //RoomData selectedRoom = validRooms.First();

                _dungeonLayout.Add(newPos, selectedRoom);
                _roomToProcess.Enqueue(newPos);

                if (_dungeonLayout.Count >= _minRoom) break;
            }
        }

        _roomToProcess.Clear();
    }

    bool IsForwardDirection(Direction dir)
    { 
        return dir == Direction.Right || dir == Direction.Top;
    }

    Direction GetOppositeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Bottom: return Direction.Top;
            case Direction.Top: return Direction.Bottom;
            case Direction.Left: return Direction.Right;
            case Direction.Right: return Direction.Left;
            default:
                {
                    Debug.LogWarning($"Direction passed in has no exit");
                    return dir;
                } 
        }
    }
    Vector2Int GetNewPosition(Vector2Int currentPos, Direction dir)
    {
        switch (dir)
        {
            case Direction.Bottom: return currentPos + new Vector2Int(0, -1);
            case Direction.Top: return currentPos + new Vector2Int(0, 1);
            case Direction.Left: return currentPos + new Vector2Int(-1, 0);
            case Direction.Right: return currentPos + new Vector2Int(1, 0);
            default: return currentPos;
        }
    }

    private void InstantiateRooms()
    {
        foreach(var roomToSpawn in _dungeonLayout)
        {
            Vector2Int gridPos = roomToSpawn.Key;
            Vector3 spawnPos = new Vector3(gridPos.x * 20, gridPos.y * 12, 0);
            GameObject room = Instantiate(roomToSpawn.Value.RoomPrefab, spawnPos, Quaternion.identity);
            room.transform.SetParent(transform);
        }
    }

    private List<RoomData> GetRoomsWithEntry(Direction requiredEntry, RoomType normal)
    {
        List<RoomData> validRooms = new List<RoomData>();

        for (int i = 0; i < _roomDataList.Count; i++)
        {
            if (_roomDataList[i].RoomExits.Contains(requiredEntry) && _roomDataList[i].RoomObjType == normal)
            {
                validRooms.Add(_roomDataList[i]);
            }
        }
        
        return validRooms;
    }

}


