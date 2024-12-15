using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Inventory.Model
{

    [CreateAssetMenu(fileName = "Inventory SO", menuName = "Scriptable Objects/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> _inventoryItems;

        public List<ItemSOBase> ItemIDList = new List<ItemSOBase>();
        private Dictionary<int, ItemSOBase> instanceIDItem;

        [field: SerializeField]
        public int InventorySize { get; private set; } = 15;

        public Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        private void OnEnable()
        {
            //Retrieve Instance ID 
            instanceIDItem = new Dictionary<int, ItemSOBase>();

            foreach (var so in ItemIDList)
            {
                if (so != null)
                {
                    instanceIDItem[so.ID] = so;
                }
            }
        }

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

        public int AddItem(ItemSOBase item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < InventorySize;)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);

                    }
                    UpdateInventoryList();
                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            UpdateInventoryList();
            return quantity;
        }

        public void AddItemFromSavedFile(List<InventorySaveData> invData)
        {
            for (int i = 0; i < invData.Count; i++)
            {
                InventoryItem item = new InventoryItem
                {
                    Item = GetItemByID(invData[i].ItemID),
                    ItemQuantity = invData[i].ItemQuantity,
                };

                _inventoryItems.Insert(invData[i].SlotIndex, item);
            }

            UpdateInventoryList();
        }

        public void RemoveAllItems()
        {
            _inventoryItems.Clear();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }

            UpdateInventoryList();
        }

        //Get SO by Instance ID
        public ItemSOBase GetItemByID(int instanceId)
        {
            instanceIDItem.TryGetValue(instanceId, out ItemSOBase item);
            return item;
        }

        private int AddItemToFirstFreeSlot(ItemSOBase item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                Item = item,
                ItemQuantity = quantity,
            };

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = newItem;
                    return quantity;
                } 
            }

            return 0;
        }

        private bool IsInventoryFull() => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSOBase item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                { continue; }

                if (_inventoryItems[i].Item.ID == item.ID)
                { 
                    int amountPossibleToTake = _inventoryItems[i].Item.MaxStackSize - _inventoryItems[i].ItemQuantity;

                    if (quantity > amountPossibleToTake)
                    {
                        _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].ItemQuantity + quantity);
                        UpdateInventoryList();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

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
