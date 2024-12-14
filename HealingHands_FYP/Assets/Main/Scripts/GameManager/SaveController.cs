using System.IO;
using UnityEngine;
using Inventory.Model;
using Newtonsoft.Json;
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
        List<SaveData> saveDatas = new List<SaveData>();

        foreach (var item in obj)
        {
            saveDatas.Add(new SaveData
            {
                SlotIndex = item.Key,
                ItemID = item.Value.Item.ID,
                ItemQuantity = item.Value.ItemQuantity,
            });
        }

        string json = JsonConvert.SerializeObject(saveDatas, Formatting.Indented);
        File.WriteAllText(_savePath, json);
    }

    public void LoadInventory()
    {

        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);

            List<SaveData> saveDatas = JsonConvert.DeserializeObject<List<SaveData>>(json);

            foreach (var saveData in saveDatas)
            {
                _invetorySO.AddItemFromSaveFile(saveData.SlotIndex, saveData.ItemID, saveData.ItemQuantity);
            }
        }
        else
        { SaveInventory(_invetorySO.GetCurrentInventoryState()); }
    }
}
