using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{

    [CreateAssetMenu(fileName = "Inventory SO", menuName = "Scriptable Objects/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> _inventoryItems;

        [field: SerializeField]
        public int InventorySize { get; private set; } = 15;

        public Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            _inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < InventorySize; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        internal InventoryItem GetItemAt(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }

        public void AddItem(ItemSOBase item, int quantity)
        {
            for (int i = 0; i < InventorySize; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = new InventoryItem
                    {
                        Item = item,
                        ItemQuantity = quantity
                    };
                    return;
                }
            }
        }

        //prolly delete later?
        internal void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.ItemQuantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                returnValue[i] = _inventoryItems[i];
            }

            return returnValue;
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = _inventoryItems[itemIndex1];
            _inventoryItems[itemIndex1] = _inventoryItems[itemIndex2];
            _inventoryItems[itemIndex2] = item1;
            UpdateInventoryList();
        }

        private void UpdateInventoryList()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int ItemQuantity;
        public ItemSOBase Item;

        public bool IsEmpty => Item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                Item = this.Item,
                ItemQuantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            Item = null,
            ItemQuantity = 0,
        };
    }

}
