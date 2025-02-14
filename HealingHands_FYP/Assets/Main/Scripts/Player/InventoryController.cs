using System;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using PlayerInputSystem;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        //remove later audio reference
        [SerializeField]
        private PlayerAudio _playerAudio;

        [SerializeField]
        private InputReader _inputReader;

        [SerializeField]
        private UIInventoryPage _inventoryPage;

        [SerializeField]
        private InventorySO _inventoryData;

        private Protagonist _player;

        private void Awake()
        {
            _player = GetComponent<Protagonist>();

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

            _player.DeathEvent += RemoveItemsOnDeath;
        }

        private void OnDisable()
        {
            _inputReader.OpenInventoryEvent -= InventoryControl;

            _inventoryPage.OnDescriptionRequested -= DecriptionRequestHandler;
            _inventoryPage.OnItemActionRequested -= ItemActionRequestHandler;
            _inventoryPage.OnSwapItems -= SwapItemHandler;

            _inventoryData.OnInventoryUpdated -= UpdateInventoryPage;

            _player.DeathEvent += RemoveItemsOnDeath;
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Initialize(); 
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

                Time.timeScale = 0;
                _playerAudio.PlayInventoryAudio();
                _inputReader.CloseInventoryEvent += InventoryControl;
            }
            else
            {
                _inventoryPage.HideInventoryPanel();

                Time.timeScale = 1;
                _inputReader.SetGameplay();
                _playerAudio.PlayInventoryAudio();
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

        private void RemoveItemsOnDeath()
        {
            _inventoryData.RemoveAllItems();
        }
    }
}