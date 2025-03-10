using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    //Have a Item Pick Up Braodcast, Pick up By Manager and Manager updates the data
    [SerializeField] private InventorySO _inventoryData;

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
