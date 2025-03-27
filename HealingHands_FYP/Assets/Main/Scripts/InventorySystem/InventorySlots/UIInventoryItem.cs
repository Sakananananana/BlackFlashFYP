using Inventory.Model;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField] protected Button _itemSlot;
        [SerializeField] protected Image _itemImage;
        [SerializeField] protected Image _pressedIndicator;
 
        public bool IsSlotEmpty = true;
        protected InspectorType _inspectorType;

        //pressed means selected to do further action = move/consume/drop
        public Action<UIInventoryItem> OnItemSelected, OnItemDeselected;
        public Action<UIInventoryItem, BtnInteractionType> OnItemPressed;

        protected virtual void Awake()
        {
            ResetData();
        }

        public virtual void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
            IsSlotEmpty = true;
        }

        public virtual void SetData(InventoryItem item) { }

        public virtual void OnItemSlotPressed() { }

        public virtual void OnItemSlotSelected()
        {
            OnItemSelected?.Invoke(this);
        }

        public void SelectedForAction(bool val)
        {
            _pressedIndicator.enabled = val;
        }

        public void ButtonNavigation(bool val)
        {
            Navigation buttonNav = _itemSlot.navigation;

            buttonNav.mode = (val)? Navigation.Mode.None : Navigation.Mode.Automatic;
            _itemSlot.navigation = buttonNav;
        }

        public void GetInspector(InspectorType type) => _inspectorType = type;
    }

    public enum BtnInteractionType
    { Normal, Craft_Remove, Craft_Add}
}