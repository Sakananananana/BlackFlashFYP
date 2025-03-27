using Inventory.Model;
using UnityEngine;
using System;
using TMPro;


namespace Inventory.UI
{
    public class UIItemSlot : UIInventoryItem
    {
        [SerializeField] private TMP_Text _itemQuantityTxt;
        protected override void Awake()
        { 
            base.Awake();
        }

        public override void SetData(InventoryItem item)
        {
            //base.SetData(item);

            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = item.Item.ItemImage;
            _itemQuantityTxt.text = item.ItemQuantity.ToString();
            IsSlotEmpty = false;
        }

        public override void OnItemSlotSelected()
        {
            base.OnItemSlotSelected();
        }

        public override void OnItemSlotPressed()
        {
            if (IsSlotEmpty)
            { return; }

            //selected animation
            SelectedForAction(true);

            if (_inspectorType == InspectorType.Normal)
            OnItemPressed?.Invoke(this, BtnInteractionType.Normal);
            else if(_inspectorType == InspectorType.Crafting)
            OnItemPressed?.Invoke(this, BtnInteractionType.Craft_Add);
        }
    }
}
