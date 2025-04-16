using UnityEngine;
using Inventory.Model;

public class ShopManager : MonoBehaviour
{
    public InventorySO inventory;
    public int totalCoins;

    public void SellItem(ItemSOBase itemToSell, int quantity)
    {
        int totalOwned = 0;

        // Count how many items the player owns
        var inventoryState = inventory.GetCurrentInventoryState();
        foreach (var slot in inventoryState.Values)
        {
            if (slot.Item == itemToSell)
            {
                totalOwned += slot.ItemQuantity;
            }
        }

        if (totalOwned < quantity)
        {
            Debug.Log("Not enough items to sell.");
            return;
        }

        // Remove the item
        int remaining = quantity;
        foreach (var slotIndex in inventoryState.Keys)
        {
            var slot = inventoryState[slotIndex];
            if (slot.Item == itemToSell)
            {
                int toRemove = Mathf.Min(remaining, slot.ItemQuantity);
                for (int i = 0; i < toRemove; i++)
                {
                    inventory.RemoveItem(slotIndex);
                }

                remaining -= toRemove;
                if (remaining <= 0) break;
            }
        }

        // Determine price
        int valuePerItem = 0;
        if (itemToSell is CraftableItemSO)
            valuePerItem = 500;
        else if (itemToSell is MaterialItemSO)
            valuePerItem = 200;

        int earnedCoins = valuePerItem * quantity;
        totalCoins += earnedCoins;

        Debug.Log($"Sold {quantity} {itemToSell.ItemName} for {earnedCoins} coins!");
    }
}

