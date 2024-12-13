using Inventory.Model;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [field:SerializeField]
    public ItemSOBase InventoryItem { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;


    internal void DestroyItem()
    {
        Destroy(gameObject);
    }
}
