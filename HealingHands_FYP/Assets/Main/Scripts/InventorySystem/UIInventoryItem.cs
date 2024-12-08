using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField]
        private Image _itemImage;

        [SerializeField]
        private TMP_Text _itemQuantityTxt;

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

        public void SetData(Sprite sprite, int quantity)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _itemQuantityTxt.text = quantity.ToString();
            IsSlotEmpty = false;
        }

        public void OnItemSlotPressed()
        {
            //if (_isSlotEmpty)
            //{ return; }

            OnItemPressed?.Invoke(this);
        }

        public void OnItemSlotSelected()
        {
            OnItemSelected?.Invoke(this);
        }


        public void ShowMovingItemPointer(bool val)
        {
            _animator.SetBool("MovingItem", val);
        }

    }
}