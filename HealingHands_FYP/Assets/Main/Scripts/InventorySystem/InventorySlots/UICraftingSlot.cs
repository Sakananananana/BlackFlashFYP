using System;
using Inventory.Model;
using UnityEngine;

namespace Inventory.UI
{
    public class UICraftingSlot : UIInventoryItem
    {
        [HideInInspector] public ItemSOBase _item;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void ResetData()
        {
            base.ResetData();
            _item = null;
        }

        public override void SetData(InventoryItem item)
        {
            _item = item.Item;
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = item.Item.ItemImage;
            IsSlotEmpty = false;
        }

        public override void OnItemSlotPressed()
        {
            if (IsSlotEmpty == true)
            { return; }

            //selected animation
            SelectedForAction(true);
            OnItemPressed?.Invoke(this, BtnInteractionType.Craft_Remove);
        }
    }

}

