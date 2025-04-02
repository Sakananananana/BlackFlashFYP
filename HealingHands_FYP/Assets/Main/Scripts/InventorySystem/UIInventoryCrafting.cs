using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace Inventory.UI
{
    public class UIInventoryCrafting : MonoBehaviour
    {
        [SerializeField] private InventorySO _inventorySO;
        [SerializeField] private UIInventoryAction _itemAction;
        [SerializeField] private UIResultSlot _resultSlot;

        [SerializeField] private List<UICraftingSlot> _craftingSlots = new List<UICraftingSlot>();
        [SerializeField] private List<CraftableItemSO> _craftingList = new List<CraftableItemSO>();

        [Header("Broadcasting to...")]
        [SerializeField] private VoidEventChannelSO _craftingUpdates;

        [Header("Listening to...")]
        [SerializeField] private IntEventChannelSO _onItemAdd;
        [SerializeField] private IntEventChannelSO _onItemRemove;
        [SerializeField] private IntEventChannelSO _onSlotPressed;
        [SerializeField] private IntEventChannelSO _slotEventEnded;
        [SerializeField] private BoolEventChannelSO _onActionStarted;
        [SerializeField] private VoidEventChannelSO _onResultSlotPressed;

        private List<UIInventoryItem> _listOfCraftingSlots = new List<UIInventoryItem>();
        private bool _isPerformingAction = false;
        private int _currentPressedItem;

        private void OnEnable()
        {
            _onItemAdd.OnEventRaised += AddItemToCraftingSlot;
            _onItemRemove.OnEventRaised += RemoveItemFromCraftingSlot;
            _onActionStarted.OnEventRaised += ButtonNavigationToggle;

            for (int i = 0; i < _craftingSlots.Count; i++)
            {
                if (!_listOfCraftingSlots.Contains(_craftingSlots[i]))
                { _listOfCraftingSlots.Add(_craftingSlots[i]); }

                _listOfCraftingSlots[i].OnItemPressed += SlotEventStarted;
            }

            ResetCraftedItemDescription();
        }

        private void OnDisable()
        {
            _onItemAdd.OnEventRaised -= AddItemToCraftingSlot;
            _onItemRemove.OnEventRaised -= RemoveItemFromCraftingSlot;
            _onActionStarted.OnEventRaised -= ButtonNavigationToggle;

            for (int i = 0; i < _craftingSlots.Count; i++)
            {
                _listOfCraftingSlots[i].OnItemPressed -= SlotEventStarted;
            }
        }

        private void CraftableCheck()
        {
            if (_craftingSlots.Any(x => x.IsSlotEmpty))
                ResetCraftedItemDescription();
            else
            {
                List<ItemSOBase> ingredients = new List<ItemSOBase>();
                for (int i = 0; i < _craftingSlots.Count; i++)
                {
                    ingredients.Add(_craftingSlots[i]._item);
                }

                for (int i = 0; i < _craftingList.Count; i++)
                {
                    if (ingredients.All(ingredients => _craftingList[i].Ingredients.Contains(ingredients)))
                    {
                        SetCraftedItemDescription(_craftingList[i]);
                        break;
                    }
                }
            }
        }

        private void SetCraftedItemDescription(ItemSOBase item)
        {
            _resultSlot.SetResult(item);
            _resultSlot._itemTitle.text = item.name;

            _onResultSlotPressed.OnEventRaised += AddCraftedItemToInventory;
        }

        private void ResetCraftedItemDescription()
        {
            _resultSlot.ResetData();
            _resultSlot._itemTitle.text = string.Empty;

            _onResultSlotPressed.OnEventRaised -= AddCraftedItemToInventory;
        }

        private void AddCraftedItemToInventory()
        {
            if (_resultSlot.IsSlotEmpty)
                return;
            else
            {
                _inventorySO.AddItem(_resultSlot._item, 1); //add item to inventory
                ResetCraftedItemDescription();
                for (int i = 0; i < _craftingSlots.Count; i++)
                {
                    _craftingSlots[i].ResetData();
                }
                _craftingUpdates.RaiseEvent();
            }
        }

        private void AddItemToCraftingSlot(int index)
        {
            for (int i = 0; i < _craftingSlots.Count; i++)
            {
                if (_craftingSlots[i].IsSlotEmpty)
                {
                    InventoryItem item = _inventorySO.GetItemAt(index);
                    _craftingSlots[i].SetData(item);
                    _inventorySO.RemoveItem(index);

                    CraftableCheck();
                    return;
                }
            }
        }

        private void RemoveItemFromCraftingSlot(int index)
        {
            _inventorySO.AddItem(_craftingSlots[index]._item, 1);
            _craftingUpdates.RaiseEvent();
            _craftingSlots[index].ResetData();

            CraftableCheck();
        }

        private void SlotEventStarted(UIInventoryItem obj, BtnInteractionType type)
        {
            _slotEventEnded.OnEventRaised += SlotEventEnded;
            _isPerformingAction = true;
            _onActionStarted.RaiseEvent(_isPerformingAction);

            int index = _listOfCraftingSlots.IndexOf(obj);

            _itemAction.OpenActionPanel(type);
            _onSlotPressed.RaiseEvent(index);
        }

        public void SlotEventEnded(int index) //When Slot Action Finished
        {
            _slotEventEnded.OnEventRaised -= SlotEventEnded;
            _itemAction.CloseActionPanel();

            _listOfCraftingSlots[index].SelectedForAction(false);
            EventSystem.current.SetSelectedGameObject(_craftingSlots[index].gameObject);

            _isPerformingAction = false;
            _onActionStarted.RaiseEvent(_isPerformingAction);
        }

        private void ButtonNavigationToggle(bool val)
        {
            for (int i = 0; i < _listOfCraftingSlots.Count; i++)
            { _listOfCraftingSlots[i].ButtonNavigation(val); }
        }

        public void OnInventoryClose()
        {
            if (_isPerformingAction == true)
            {
                SlotEventEnded(_currentPressedItem);
                _currentPressedItem = 0;
            }

            if (_craftingSlots.All(x => x.IsSlotEmpty))
                return;
            else 
            {
                for (int i = 0; i < _craftingSlots.Count; i++)
                {
                    if (!_craftingSlots[i].IsSlotEmpty)
                        RemoveItemFromCraftingSlot(i);
                }
            }
        }
    }
}
