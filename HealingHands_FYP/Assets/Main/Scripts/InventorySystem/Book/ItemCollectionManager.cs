using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class ItemCollectionManager : MonoBehaviour
{
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private List<ItemSOBase> _itemList;
    [SerializeField] private ItemSOBase _unknownItem;

    public List<ItemSOBase> CollectedItems { get; private set; } = new();

    private void Awake()
    {
        _inventorySO.GetCurrentInventoryState(); // ensure IsItemCollected is up to date
        BookOfRecordUpdate();
    }
    private void BookOfRecordUpdate()
    {
        CollectedItems.Clear();

        foreach (var item in _itemList)
        {
            if (item != null && item.IsItemCollected)
                CollectedItems.Add(item);
        }
    }
}

