using Inventory.Model;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _selIndicator;
        [SerializeField] private TMP_Text _itemQuantityTxt;

        public bool IsSlotEmpty = true;

        //pressed means selected to do further action = move/consume/drop
        public Action<UIInventoryItem> OnItemPressed, OnItemSelected, OnItemDeselected;

        private void Awake()
        {
            ResetData();
            _animator = GetComponentInParent<Animator>();
        }

        public void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
            IsSlotEmpty = true;
        }

        public void SetData(InventoryItem item)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = item.Item.ItemImage;
            _itemQuantityTxt.text = item.ItemQuantity.ToString();
            IsSlotEmpty = false;
        }

        public void OnItemSlotPressed()
        {
            if (IsSlotEmpty)
            { return; }

            //selected animation
            SelectedForAction(true);
            OnItemPressed?.Invoke(this);
        }

        public void OnItemSlotSelected()
        {
            OnItemSelected?.Invoke(this);
        }

        public void SelectedForAction(bool val)
        {
            _selIndicator.enabled = val;
            //_animator.SetBool("MovingItem", val);
        }

    }
}