using System.IO;
using UnityEngine;
using Inventory.Model;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    [SerializeField] private InventorySO _invetorySO;
    private string _savePath;

    private void Start()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadInventory(); 
    }


    private void OnEnable()
    {
        _invetorySO.OnInventoryUpdated += SaveInventory;
    }

    private void OnDisable()
    {
        _invetorySO.OnInventoryUpdated -= SaveInventory;
    }

    public void SaveInventory(Dictionary<int, InventoryItem> obj)
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();

        foreach (var item in obj)
        {
            invData.Add(new InventorySaveData { SlotIndex = item.Key, ItemID = item.Value.Item.ID, ItemQuantity = item.Value.ItemQuantity });
        }

        SaveData saveData = new SaveData
        {
            InventorySavedData = invData,
        };

        File.WriteAllText(_savePath, JsonUtility.ToJson(saveData));
    }

    public void LoadInventory()
    {
        if (File.Exists(_savePath))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_savePath));

            _invetorySO.AddItemFromSavedFile(saveData.InventorySavedData);

        }
        else
        { SaveInventory(_invetorySO.GetCurrentInventoryState()); }
    }
}
