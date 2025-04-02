using UnityEngine;

[CreateAssetMenu(fileName = "RoomChannelSO", menuName = "Scriptable Objects /Channels /RoomChannelSO")]
public class RoomChannelSO : ScriptableObject
{
    public delegate void RoomSpawn(RoomData room, Vector2Int pos);
}
