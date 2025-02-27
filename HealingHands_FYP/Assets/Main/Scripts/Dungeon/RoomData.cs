using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new RoomData", menuName = "Scriptable Objects /DungeonRoomSO")]
public class RoomData : ScriptableObject
{
    public GameObject RoomPrefab;
    public RoomType RoomObjType;
    public List<Direction> RoomExits;
}

public enum Direction
{
    Top, Bottom, Left, Right,
}

public enum RoomType
{
    Normal,
    StartRoom,
    BossRoom,
    SafeRoom,
}
