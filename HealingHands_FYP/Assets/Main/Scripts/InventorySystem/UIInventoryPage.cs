using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Inventory.Model;
using PlayerInputSystem;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventoryData;

        [Header("Inventory Content")]
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private UIInventoryAction _itemAction;
        [SerializeField] private UIInventoryDescription _itemDescription;

        [Header("Broadcasting to...")]
        [SerializeField] private IntEventChannelSO _onSlotPressed;

        [Header("Listening to...")]
        [SerializeField] private IntEventChannelSO _slotEventEnded;

        List<UIInventoryItem> _listOfUIItems;
        private bool _isPeformingAction = false;
        private int _currentPressedItem = 0;

        private void OnEnable()
        {
            for (int i = 0; i < _listOfUIItems.Count; i++)
            {
                _listOfUIItems[i].OnItemPressed += SlotEventStarted;
                _listOfUIItems[i].OnItemSelected += SlotSelectedAction;
            }

            EventSystem.current.SetSelectedGameObject(_listOfUIItems[0].gameObject);
        }

        private void OnDisable()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            { EventSystem.current.SetSelectedGameObject(null); }

            for (int i = 0; i < _listOfUIItems.Count; i++)
            {
                _listOfUIItems[i].OnItemPressed -= SlotEventStarted;
                _listOfUIItems[i].OnItemSelected -= SlotSelectedAction;
            }
        }

        public void UIInventoryInit()
        {
            for (int i = 0; i < 15; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                _listOfUIItems.Add(uiItem);
            }
        }

        public void FillInventory()
        {
            if (_listOfUIItems == null)
            {
                _listOfUIItems = new List<UIInventoryItem>();
                UIInventoryInit();
            }

            ResetCurrentPage();
            _itemDescription.ResetDescription();

            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                UpdateInventorySlot(item.Key, item.Value);
            }
        }

        public void UpdateInventorySlot(int itemIndex, InventoryItem obj)
        {
            if (_listOfUIItems.Count > itemIndex)
            {
                _listOfUIItems[itemIndex].SetData(obj);
            }
        }

        internal void ResetCurrentPage()
        {
            foreach (var item in _listOfUIItems)
            {
                item.ResetData();
            }
        }

        //When Slot is Being Selected
        void SlotSelectedAction(UIInventoryItem obj)
        {
            int index = _listOfUIItems.IndexOf(obj);
            SetItemDescription(index);
        }

        //When Slot is Being Pressed
        void SlotEventStarted(UIInventoryItem obj)
        {
            _slotEventEnded.OnEventRaised += SlotEventEnded;
            _isPeformingAction = true;

            int index = _listOfUIItems.IndexOf(obj);
            _currentPressedItem = index;

            _itemAction.OpenActionPanel();
            _onSlotPressed.RaiseEvent(index);
        }

        //When Slot Action Finished
        public void SlotEventEnded(int index)
        {
            _slotEventEnded.OnEventRaised -= SlotEventEnded;
            _itemAction.CloseActionPanel();

            FillInventory();
            _listOfUIItems[index].SelectedForAction(false);
            EventSystem.current.SetSelectedGameObject(_listOfUIItems[index].gameObject);
            SetItemDescription(index);

            _isPeformingAction = false;
        }

        void SetItemDescription(int index)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(index);
            if (!inventoryItem.IsEmpty)
            {
                ItemSOBase item = inventoryItem.Item;
                _itemDescription.SetDescription(item);
            }
            else
            {
                _itemDescription.ResetDescription();
                return;
            }
        }

        public void CloseInventory()
        {
            if (_isPeformingAction == true)
            {
                SlotEventEnded(_currentPressedItem);
                _currentPressedItem = 0;
            }

            gameObject.SetActive(false);
        }

        #region Moving & Swapping Items (Just in Case Need)

        //private void HandleShowItemActions(UIInventoryItem obj)
        //{
        //    int index = _listOfUIItems.IndexOf(obj);

        //_inventoryData.RemoveItem(index);

        //if (_isMovingItem == false && _listOfUIItems[index].IsSlotEmpty == false)
        //{
        //    _isMovingItem = true;
        //    _currentMovingItem = index;

        //    //set to show moving pointer
        //    _listOfUIItems[index].ShowMovingItemPointer(_isMovingItem);

        //    //remove press listener, change to swap listener
        //    foreach (var item in _listOfUIItems)
        //    {
        //        item.OnItemPressed -= HandleShowItemActions;
        //        item.OnItemPressed += HandleSwap;  
        //    }
        //}
        //}

        //private void HandleSwap(UIInventoryItem obj)
        //{
        //    int index = _listOfUIItems.IndexOf(obj);

        //    _isMovingItem = false;

        //    _inventoryData.SwapItems(_currentMovingItem, index);
        //    OnSwapItems?.Invoke(_currentMovingItem, index);
        //    _listOfUIItems[index].ShowMovingItemPointer(_isMovingItem);

        //    _currentMovingItem = -1;

        //    foreach (var item in _listOfUIItems)
        //    {
        //        item.OnItemPressed += HandleShowItemActions;
        //        item.OnItemPressed -= HandleSwap;
        //    } 
        //}

        #endregion
    }
}