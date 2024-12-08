using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _itemTitle.text = itemName;
            _itemDescription.text = itemDescription;
        }
    }
}