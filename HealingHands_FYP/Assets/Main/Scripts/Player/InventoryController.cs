using System;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using PlayerInputSystem;
using System.Collections.Generic;
using static UnityEditor.Progress;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        [SerializeField]
        private UIInventoryPage _inventoryPage;

        [SerializeField]
        private InventorySO _inventoryData;

        //remove later
        public List<InventoryItem> InitialItems = new List<InventoryItem>();

        private void Awake()
        {
            _inventoryPage.InitializeInventoryUI(_inventoryData.InventorySize);
            PrepareInventoryData();
        }

        private void OnEnable()
        {
            _inputReader.OpenInventoryEvent += InventoryControl;

            _inventoryPage.OnDescriptionRequested += DecriptionRequestHandler;
            _inventoryPage.OnItemActionRequested += ItemActionRequestHandler;
            _inventoryPage.OnSwapItems += SwapItemHandler;

            _inventoryData.OnInventoryUpdated += UpdateInventoryPage;
        }

        private void OnDisable()
        {
            _inputReader.OpenInventoryEvent -= InventoryControl;

            _inventoryPage.OnDescriptionRequested -= DecriptionRequestHandler;
            _inventoryPage.OnItemActionRequested -= ItemActionRequestHandler;
            _inventoryPage.OnSwapItems -= SwapItemHandler;

            _inventoryData.OnInventoryUpdated -= UpdateInventoryPage;
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Initialize();
            foreach (var item in InitialItems) 
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                _inventoryData.AddItem(item);
            }
        }

        private void InventoryControl()
        {
            if (!_inventoryPage.isActiveAndEnabled)
            {
                foreach (var item in _inventoryData.GetCurrentInventoryState())
                {
                    _inventoryPage.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.ItemQuantity);
                }

                _inventoryPage.ShowInventoryPanel();
                _inputReader.CloseInventoryEvent += InventoryControl;
            }
            else
            {
                _inventoryPage.HideInventoryPanel();
                _inputReader.CloseInventoryEvent -= InventoryControl;
            }
        }

        private void UpdateInventoryPage(Dictionary<int, InventoryItem> obj)
        {
            _inventoryPage.ResetCurrentPage();
            foreach (var item in obj) 
            {
                _inventoryPage.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.ItemQuantity);
            }
        }

        private void SwapItemHandler(int itemIndex1, int itemIndex2)
        {
            _inventoryData.SwapItems(itemIndex1, itemIndex2);

            //to update the Description Panel
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex2);
            ItemSOBase item = inventoryItem.Item;
            _inventoryPage.UpdateDescriptionPanel(itemIndex2, item.ItemName, item.ItemImage, item.ItemDescription);
        }

        private void ItemActionRequestHandler(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            //Pass in the item to move (to show the UI one) 
            //_inventoryPage.
        } 

        private void DecriptionRequestHandler(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

            if (inventoryItem.IsEmpty)
            {
                _inventoryPage.ResetDescriptionPanel();
                return;
            }

            ItemSOBase item = inventoryItem.Item;
            _inventoryPage.UpdateDescriptionPanel(itemIndex, item.ItemName, item.ItemImage, item.ItemDescription);
        }
    }
}