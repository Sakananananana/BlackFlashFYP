using System;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using PlayerInputSystem;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventoryData;

        [Header("Listening to...")]
        [SerializeField] private IntEventChannelSO _onItemUsed;
        [SerializeField] private IntEventChannelSO _onItemDropped;

        private void Awake()
        {
            PrepareInventory();
        }

        private void PrepareInventory()
        {
            _inventoryData.Initialize();
        }

        private void OnEnable()
        {
            _onItemUsed.OnEventRaised += UseItem;
            _onItemDropped.OnEventRaised += DropItem;
        }

        private void OnDisable()
        {
            _onItemUsed.OnEventRaised -= UseItem;
            _onItemDropped.OnEventRaised -= DropItem;
        }

        private void UseItem(int index)
        {
            _inventoryData.RemoveItem(index);
            //Update Save Data
        }

        private void DropItem(int index)
        {
            _inventoryData.DropItem(index);
            //Update Save Data
        }

        private void RemoveItemsOnDeath()
        {
            _inventoryData.RemoveAllItems();
            //Update Save Data
        }

        #region Moving and Swapping Item (Just in Case Need)
        //private void SwapItemHandler(int itemIndex1, int itemIndex2)
        //{
        //    //Update Data & Save
        //    _inventoryData.SwapItems(itemIndex1, itemIndex2);

        //    //to update the Description Panel upon swapping
        //    InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex2);
        //    ItemSOBase item = inventoryItem.Item;
        //    _inventoryPage.UpdateDescriptionPanel(itemIndex2, item.ItemName, item.ItemImage, item.ItemDescription);
        //}
        #endregion
    }
}