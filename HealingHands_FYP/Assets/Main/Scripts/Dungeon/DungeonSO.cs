using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSO", menuName = "Scriptable Objects/DungeonSO")]
public class DungeonSO : ScriptableObject
{
    public int CurrentLvL;
    public int BaseRoomCount;
    public int CurrentRoomCount;
    public bool HasBossFightComplete;

    public List<RoomData> _roomDataList = new List<RoomData>();

    //private parameters
    private RoomData _startRoom;
    private Vector2Int _startPos = Vector2Int.zero;

    //To store processed room data & spawned position
    public Dictionary<Vector2Int, RoomData> DungeonLayout = new Dictionary<Vector2Int, RoomData>();

    //To add unprocessed position in grid
    private Queue<Vector2Int> _roomToProcess = new Queue<Vector2Int>();

    public void DungeonProgress()
    {
        _startRoom = null;
        DungeonLayout.Clear();

        if (CurrentLvL <= 1)
            CurrentLvL++;
        else
        {
            HasBossFightComplete = true;
        }
    }

    public void ResetDungeonProgress()
    {
        _startRoom = null;
        DungeonLayout.Clear();

        CurrentLvL = 0;
        CurrentRoomCount = BaseRoomCount;
        HasBossFightComplete = false;
    }

    public void GetDungeonPath()
    {
        DungeonLayout.Clear();
        _roomToProcess.Clear();
        CurrentRoomCount = 0;
        CorePathGenerator();
        RoomValidationCheck();
    }

    private void CorePathGenerator()
    {
        CurrentRoomCount = BaseRoomCount + (CurrentLvL * 2);
        _startRoom = GetRoomsWithEntry(Direction.Bottom, true);

        DungeonLayout.Add(_startPos, _startRoom);
        _roomToProcess.Enqueue(_startPos);

        while (DungeonLayout.Count < CurrentRoomCount && _roomToProcess.Count > 0)
        {
            Vector2Int pos = _roomToProcess.Dequeue();
            RoomData currentRoom = DungeonLayout[pos];

            foreach (Direction dir in currentRoom.RoomExits)
            {
                Vector2Int newPos = GetNewPosition(pos, dir);
                if (DungeonLayout.ContainsKey(newPos) || newPos.y < 0) { continue; }

                Direction requiredEntry = GetOppositeDirection(dir);
                RoomData selectedRoom = GetRoomsWithEntry(requiredEntry);
                if (selectedRoom == null) { continue; }

                DungeonLayout.Add(newPos, selectedRoom);
                _roomToProcess.Enqueue(newPos);
                if (DungeonLayout.Count >= CurrentRoomCount) break;
            }
        }

        _roomToProcess.Clear();
    }

    private void RoomValidationCheck()
    {
        foreach (var room in DungeonLayout)
        {
            _roomToProcess.Enqueue(room.Key);
        }

        while (_roomToProcess.Count > 0)
        {
            Vector2Int currentPos = _roomToProcess.Dequeue();
            RoomData currentRoom = DungeonLayout[currentPos];

            foreach (Direction exitDir in currentRoom.RoomExits)
            {
                Vector2Int newPos = GetNewPosition(currentPos, exitDir); //Get position of the new room
                Direction requiredEntry = GetOppositeDirection(exitDir);

                if (DungeonLayout.ContainsKey(newPos)) //Check if this position alrd contains a room
                {
                    RoomData temp = DungeonLayout[newPos]; //Get the existed room data from the dictionary

                    if (temp.RoomExits.Contains(requiredEntry)) { continue; } //check if the new pos's room has the entry to the current room{ continue; }
                    else { EditRoomExit(currentPos, exitDir); }
                }
                else { EditRoomExit(currentPos, exitDir); }
            }
        }
    }

    private void EditRoomExit(Vector2Int pos, Direction dir)
    {
        List<Direction> dirList = new List<Direction>();
        for (int i = 0; i < DungeonLayout[pos].RoomExits.Count; i++)
        {
            dirList.Add(DungeonLayout[pos].RoomExits[i]);
        }
        dirList.Remove(dir);

        for (int i = 0; i < _roomDataList.Count; i++)
        {
            if (AreListsEqual(_roomDataList[i].RoomExits, dirList))
            {
                DungeonLayout[pos] = _roomDataList[i];
                _roomToProcess.Enqueue(pos);
                break;
            }
        }
    }

    bool AreListsEqual<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count) return false;

        return list1.All(list2.Contains);
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

    private RoomData GetRoomsWithEntry(Direction requiredEntry, bool exclude = false)
    {
        List<RoomData> validRooms = new List<RoomData>();
        RoomData room = null;

        if (exclude == true)
        {
            for (int i = 0; i < _roomDataList.Count; i++)
            {
                if (!_roomDataList[i].RoomExits.Contains(requiredEntry) && _roomDataList[i].RoomExits.Count > 1)
                {
                    validRooms.Add(_roomDataList[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < _roomDataList.Count; i++)
            {
                if (_roomDataList[i].RoomExits.Contains(requiredEntry) && _roomDataList[i].RoomExits.Count > 1)
                {
                    validRooms.Add(_roomDataList[i]);
                }
            }
        }

        room = validRooms[Random.Range(0, validRooms.Count)];
        return room;
    }
}
