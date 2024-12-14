using UnityEngine;
using Inventory.Model;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    //dictionary key for the index of the item and the item
    public int SlotIndex;
    public int ItemID;
    public int ItemQuantity;
}
