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
        [SerializeField] private UIItemSlot _itemPrefab;
        [SerializeField] private UIInventoryAction _itemAction;
        [SerializeField] private UIInventoryInspector _inspector;

        [Header("Broadcasting to...")]
        [SerializeField] private IntEventChannelSO _onSlotPressed;

        [Header("Listening to...")]
        [SerializeField] private IntEventChannelSO _slotEventEnded;
        [SerializeField] private BoolEventChannelSO _onActionStarted;
        [SerializeField] private VoidEventChannelSO _craftingUpdates;

        private List<UIInventoryItem> _listOfUIItems;
        private InspectorType _currentInspector;
        private bool _isPeformingAction = false;
        private int _currentPressedItem = 0;

        private void OnEnable()
        {
            _onActionStarted.OnEventRaised += ButtonNavigationToggle;
            _craftingUpdates.OnEventRaised += FillInventory;

            for (int i = 0; i < _listOfUIItems.Count; i++)
            {
                _listOfUIItems[i].OnItemPressed += SlotEventStarted;
                _listOfUIItems[i].OnItemSelected += SlotSelectedAction;

                _listOfUIItems[i].GetInspector(_currentInspector);
            }

            EventSystem.current.SetSelectedGameObject(_listOfUIItems[0].gameObject);
            SetItemDescription(0);
        }

        private void OnDisable()
        {
            _onActionStarted.OnEventRaised -= ButtonNavigationToggle;
            _craftingUpdates.OnEventRaised -= FillInventory;

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
                UIItemSlot uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
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
            _inspector.FillDescription(null);

            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                UpdateInventorySlot(item.Key, item.Value);
            }
        }

        public void SetInspector(InspectorType type = InspectorType.Normal)
        {
            _currentInspector = type;

            switch (type)
            {
                case InspectorType.Crafting:
                    {
                        if (_inspector._descPanel.gameObject.activeSelf)
                        { _inspector._descPanel.gameObject.SetActive(false); }

                        _inspector._craftingPanel.gameObject.SetActive(true);
                        break;
                    }

                default:
                    {
                        if (_inspector._craftingPanel.gameObject.activeSelf)
                        { _inspector._craftingPanel.gameObject.SetActive(false); }

                        _inspector._descPanel.gameObject.SetActive(true);
                        break;
                    }
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
        
        void SlotEventStarted(UIInventoryItem obj, BtnInteractionType type) //When Slot is Being Pressed
        {
            _slotEventEnded.OnEventRaised += SlotEventEnded;
            _isPeformingAction = true;
            _onActionStarted.RaiseEvent(_isPeformingAction);

            int index = _listOfUIItems.IndexOf(obj);
            _currentPressedItem = index;

            _itemAction.OpenActionPanel(type);
            _onSlotPressed.RaiseEvent(index);
        }
        
        public void SlotEventEnded(int index) //When Slot Action Finished
        {
            _slotEventEnded.OnEventRaised -= SlotEventEnded;
            _itemAction.CloseActionPanel();

            FillInventory();
            _listOfUIItems[index].SelectedForAction(false);
            EventSystem.current.SetSelectedGameObject(_listOfUIItems[index].gameObject);
            SetItemDescription(index);

            _isPeformingAction = false;
            _onActionStarted.RaiseEvent(_isPeformingAction);
        }

        private void ButtonNavigationToggle(bool val)
        {
            for (int i = 0; i < _listOfUIItems.Count; i++)
            { _listOfUIItems[i].ButtonNavigation(val); }
        }

        void SetItemDescription(int index)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(index);
            _inspector.FillDescription(null);

            if (!inventoryItem.IsEmpty)
            {
                ItemSOBase item = inventoryItem.Item;
                _inspector.FillDescription(item, false);
            }
        }

        public void CloseInventory()
        {
            if (_isPeformingAction == true)
            {
                SlotEventEnded(_currentPressedItem);
                _currentPressedItem = 0;
            }

            _inspector.OnInventoryClose();
            gameObject.SetActive(false);
        }
    }

    public enum InspectorType 
    {
        Normal,
        Crafting,
    }
}