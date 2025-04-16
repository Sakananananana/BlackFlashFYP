using UnityEngine;
using Inventory.Model;
public class Inventorytest : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private ItemSOBase inventoryItem;
    [SerializeField] VoidEventChannelSO updateInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        inventory.RemoveItem(0);
        updateInventory.RaiseEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
