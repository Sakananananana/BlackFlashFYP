using Inventory.Model;
using TMPro;
using UnityEngine;

namespace Inventory.UI
{
    public class UIResultSlot : UIInventoryItem
    {
        [HideInInspector] public ItemSOBase _item;

        [SerializeField] public TMP_Text _itemTitle;
        [SerializeField] public TMP_Text _itemDescription;
        [SerializeField] private VoidEventChannelSO _onResultSlotPressed;

        public void SetResult(ItemSOBase item)
        {
            _item = item;
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = item.ItemImage;
            IsSlotEmpty = false;
        }

        public override void ResetData()
        {
            base.ResetData();
            _item = null;
        }

        public override void OnItemSlotPressed()
        {
            if (IsSlotEmpty == true)
            { return; }

            _onResultSlotPressed.RaiseEvent();
        }
    }
}
