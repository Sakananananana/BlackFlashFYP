using UnityEngine;
using Inventory.Model;

namespace Inventory.UI
{
    public class UIInventoryInspector : MonoBehaviour
    {
        public UIInventoryCrafting _craftingPanel;
        public UIInventoryDescription _descPanel;

        public void FillDescription(ItemSOBase item, bool isEmpty = true)
        {
            if (isEmpty)
            { _descPanel.ResetDescription(); }
            else
            { _descPanel.SetDescription(item); }
        }

        public void OnInventoryClose()
        { 
            _craftingPanel.OnInventoryClose();
        }
    }
}

