using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class RoomData
{
    [Header("Room Object")]
    public RoomManager RoomPrefab;

    [Header("Room Exit(s)")]
    public List<Direction> RoomExits;
}

public enum Direction
{
    Top, Bottom, Left, Right,
}

