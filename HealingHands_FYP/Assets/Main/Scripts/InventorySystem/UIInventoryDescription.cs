using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Inventory.Model;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField]
        private Image _itemImage;

        [SerializeField]
        private TMP_Text _itemTitle;

        [SerializeField]
        private TMP_Text _itemDescription;

        public void ResetDescription()
        {
            _itemImage.gameObject.SetActive(false);
            _itemTitle.text = string.Empty;
            _itemDescription.text = string.Empty;
        }

        public void SetDescription(ItemSOBase item)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = item.ItemImage;
            _itemTitle.text = item.ItemName;
            _itemDescription.text = item.ItemDescription;
        }
    }
}