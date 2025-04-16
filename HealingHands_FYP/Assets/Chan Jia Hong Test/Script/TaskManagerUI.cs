using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory.Model;

public class TaskUIManager : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public ItemSOBase requiredItem;
        public int requiredAmount;

        public string GetDisplayText()
        {
            Debug.Log(requiredItem.ItemName);
            return $"Collect {requiredAmount} {requiredItem.ItemName}(s)";
        }

        public bool IsCompleted(InventorySO inventory)
        {
            var currentInventory = inventory.GetCurrentInventoryState();
            int totalAmount = 0;

            foreach (var item in currentInventory.Values)
            {
                if (item.Item == requiredItem)
                {
                    totalAmount += item.ItemQuantity;
                }
            }

            return totalAmount >= requiredAmount;
        }
    }

    [Header("References")]
    public InventorySO playerInventory;
    public List<ItemSOBase> itemPool; // Assign item SOs in Inspector
    public GameObject[] taskSlots; // UI panels with Text & Icon child
    public GameObject taskUIPrefab;

    private List<Task> activeTasks = new List<Task>();

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            activeTasks.Add(GenerateRandomTask());
        }

        UpdateUI();
    }

    public void CompleteTask(int index)
    {
        if (index < 0 || index >= activeTasks.Count) return;

        activeTasks.RemoveAt(index);

        // Rearrange: shift remaining tasks left
        UpdateUI();

        StartCoroutine(SpawnNewTaskAfterDelay());
    }

    IEnumerator SpawnNewTaskAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (activeTasks.Count < 3)
        {
            activeTasks.Add(GenerateRandomTask());
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < taskSlots.Length; i++)
        {
            if (i < activeTasks.Count)
            {
                var task = activeTasks[i];
                taskSlots[i].SetActive(true);

                Text taskText = taskSlots[i].GetComponentInChildren<Text>();
                Image iconImage = taskSlots[i].transform.Find("Icon").GetComponent<Image>();

                taskText.text = task.GetDisplayText();
                iconImage.sprite = task.requiredItem.ItemImage;
            }
            else
            {
                taskSlots[i].SetActive(false);
            }
        }
    }

    private Task GenerateRandomTask()
    {
        var randomItem = itemPool[Random.Range(0, itemPool.Count)];
        int randomAmount = Random.Range(1, 4); 

        return new Task
        {
            requiredItem = randomItem,
            requiredAmount = randomAmount
        };
    }
}


