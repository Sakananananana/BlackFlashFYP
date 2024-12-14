using UnityEngine;
using System.IO;
using Inventory.Model;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance { get; private set; }
    [SerializeField] private InventorySO _invetorySO;

    private string _savePath;

    private void Start()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadInventory();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        { 
            Instance = this;
        }    
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
        foreach (var item in obj)
        {
            SaveData saveData = new SaveData
            {
                SlotIndex = item.Key,
                ItemID = item.Value.Item.ID,
                ItemQuantity = item.Value.ItemQuantity,
            };

            Debug.Log(saveData.SlotIndex);
            Debug.Log(saveData.ItemID);
            Debug.Log(saveData.ItemQuantity);

            File.WriteAllText(_savePath, JsonUtility.ToJson(saveData));
        }
    }

    public void LoadInventory()
    {
        if (File.Exists(_savePath))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_savePath));
            _invetorySO.AddItemFromSaveFile(saveData.SlotIndex, saveData.ItemID, saveData.ItemQuantity);

            Debug.Log(saveData.SlotIndex);
            Debug.Log(saveData.ItemID);
            Debug.Log(saveData.ItemQuantity);
        }
        else
        { SaveInventory(_invetorySO.GetCurrentInventoryState()); }
    }
}
