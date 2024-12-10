using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        //Moving Item State, Default State

        [SerializeField]
        private UIInventoryItem _itemPrefab;

        [SerializeField]
        private RectTransform _contentPanel;

        [SerializeField]
        private UIInventoryDescription _itemDescription;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        public Action<int> OnDescriptionRequested, OnItemActionRequested, OnMoveItemRequested;
        public Action<int, int> OnSwapItems;

        [SerializeField]
        private bool _isMovingItem;
        [SerializeField]
        private int _currentMovingItem = -1;

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                listOfUIItems.Add(uiItem);

                uiItem.OnItemPressed += HandleShowItemActions;
                uiItem.OnItemSelected += HandleSelectItem;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleSelectItem(UIInventoryItem obj)
        {
            int index = listOfUIItems.IndexOf(obj);
            OnDescriptionRequested?.Invoke(index);

            listOfUIItems[index].ShowMovingItemPointer(_isMovingItem);
        }

        //currently use as MOVING ITEMS
        private void HandleShowItemActions(UIInventoryItem obj)
        {
            int index = listOfUIItems.IndexOf(obj);

            if (_isMovingItem == false && listOfUIItems[index].IsSlotEmpty == false)
            {
                _isMovingItem = true;
                _currentMovingItem = index;

                //set to show moving pointer
                listOfUIItems[index].ShowMovingItemPointer(_isMovingItem);

                //remove press listener, change to swap listener
                foreach (var item in listOfUIItems)
                {
                    item.OnItemPressed -= HandleShowItemActions;
                    item.OnItemPressed += HandleSwap;  
                }
            }
        }

        private void HandleSwap(UIInventoryItem obj)
        {
            int index = listOfUIItems.IndexOf(obj);

            _isMovingItem = false;
            OnSwapItems?.Invoke(_currentMovingItem, index);
            listOfUIItems[index].ShowMovingItemPointer(_isMovingItem);

            _currentMovingItem = -1;

            foreach (var item in listOfUIItems)
            {
                item.OnItemPressed += HandleShowItemActions;
                item.OnItemPressed -= HandleSwap;
            } 
        }

        #region Show and Hide Inventory Panel
        public void ShowInventoryPanel()
        {
            gameObject.SetActive(true);
            _itemDescription.ResetDescription();

            if (listOfUIItems != null)
            {
                //set the first slot as selected
                EventSystem.current.SetSelectedGameObject(listOfUIItems[0].gameObject);
            }
        }

        public void HideInventoryPanel()
        {
            EventSystem.current.SetSelectedGameObject(null);
            gameObject.SetActive(false);
        }
        #endregion

        internal void UpdateDescriptionPanel(int itemIndex, string itemName, Sprite itemImage, string itemDescription)
        {
            _itemDescription.SetDescription(itemImage, itemName, itemDescription);
        }

        internal void ResetDescriptionPanel()
        {
            _itemDescription.ResetDescription();
        }

        internal void ResetCurrentPage()
        {
            foreach (var item in listOfUIItems)
            { 
                item.ResetData();
            }
        }
    }
}