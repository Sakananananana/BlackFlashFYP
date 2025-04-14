using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSO", menuName = "Scriptable Objects/DungeonSO")]
public class DungeonSO : ScriptableObject
{
    private int _baseRoomCount = 5;
    public int RoomCount {get; set;}
    public int CurrentLvL { get; set; }

    public void DungeonProgress()
    { 
        CurrentLvL = (CurrentLvL != 2) ? CurrentLvL++ : 0;
        RoomCount = _baseRoomCount + (CurrentLvL * 2);
    }
}
