using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Inventory SO", menuName = "Scriptable Objects /Inventory /Inventory Data /InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> _inventoryItems;
        [SerializeField] private ItemEventChannelSO _onInventoryUpdated;

        [field: SerializeField] public int InventorySize { get; private set; } = 15;

        //For saving,list a number of items' unique id to find back from save files
        //Move To Dedicated Save System Later
        public List<ItemSOBase> ItemIDList = new List<ItemSOBase>();
        private Dictionary<int, ItemSOBase> instanceIDItem;

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

        #region Add Item Logic
        internal void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.ItemQuantity);
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

                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            return quantity;
        }

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

        #endregion

        #region Remove Item Logic

        public void RemoveItem(int slotIndex)
        {
            if (_inventoryItems[slotIndex].ItemQuantity > 0)
            {
                _inventoryItems[slotIndex] = _inventoryItems[slotIndex].ChangeQuantity(_inventoryItems[slotIndex].ItemQuantity - 1);

                if (_inventoryItems[slotIndex].ItemQuantity <= 0)
                {
                    _inventoryItems.RemoveAt(slotIndex);
                    _inventoryItems.Add(InventoryItem.GetEmptyItem());
                }
            }
        }
        
        public void DropItem(int slotIndex)
        {
            _inventoryItems.RemoveAt(slotIndex);
            _inventoryItems.Add(InventoryItem.GetEmptyItem());
        }

        public void RemoveAllItems()
        {
            _inventoryItems.Clear();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        #endregion

        #region Saving Related

        //Get SO by Instance ID
        public ItemSOBase GetItemByID(int instanceId)
        {
            instanceIDItem.TryGetValue(instanceId, out ItemSOBase item);
            return item;
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
        }

        #endregion
        
        private bool IsInventoryFull() => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

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

        #region Swapping Items (Just in Case Need)

        //public void SwapItems(int itemIndex1, int itemIndex2)
        //{
        //    InventoryItem item1 = _inventoryItems[itemIndex1];
        //    _inventoryItems[itemIndex1] = _inventoryItems[itemIndex2];
        //    _inventoryItems[itemIndex2] = item1;
        //    UpdateInventoryList();
        //}

        #endregion
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
