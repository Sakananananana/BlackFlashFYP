using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO _inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemBehaviour item = collision.GetComponent<ItemBehaviour>();

        if (item != null)
        {
            int remainder = _inventoryData.AddItem(item.InventoryItem, item.Quantity);

            if (remainder == 0)
            { item.DestroyItem(); }
            else
            { item.Quantity = remainder; }
        }
        
    }
}
