using UnityEngine;
using UnityEngine.Events;
using Inventory.Model;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemEventChannelSO", menuName = "Scriptable Objects /Channels /ItemEventChannelSO")]
public class ItemEventChannelSO : ScriptableObject
{
    public UnityAction<Dictionary<int, InventoryItem>> OnEventRaised;

    public void RaiseEvent(Dictionary<int, InventoryItem> obj)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(obj);
    }
}
