using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new RoomData", menuName = "Scriptable Objects /DungeonRoomSO")]
public class RoomData : ScriptableObject
{
    [Header("Room Object")]
    public RoomManager RoomPrefab;
    public RoomType RoomObjType;

    [Header("Room Exit(s)")]
    public List<Direction> RoomExits;
}

public enum Direction
{
    Top, Bottom, Left, Right,
}

public enum RoomType
{
    Normal,
    BossRoom,
    SafeRoom,
}
