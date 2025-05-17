using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory.Model;
using UnityEngine.EventSystems;

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
            return $"<size=24>Need</size>\n<size=48>{requiredAmount}</size>\n <size=30>{requiredItem.ItemName}(s)</size>";
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
    public ShopManager shopManager;

    private List<Task> activeTasks = new List<Task>();

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            activeTasks.Add(GenerateRandomTask());
        }

        UpdateUI();
    }

    //public void CompleteTask(int index)
    //{
    //    if (index < 0 || index >= activeTasks.Count) return;

    //    activeTasks.RemoveAt(index);

    //    // Rearrange: shift remaining tasks left
    //    UpdateUI();

    //    StartCoroutine(SpawnNewTaskAfterDelay());
    //}


    //IEnumerator SpawnNewTaskAfterDelay()
    //{
    //    yield return new WaitForSecondsRealtime(2f);

    //    if (activeTasks.Count < 3)
    //    {
    //        Task newTask = GenerateRandomTask();
    //        activeTasks.Add(newTask);
    //        Debug.Log($"New Task: {newTask.GetDisplayText()}");
    //        UpdateUI();
    //    }
    //}


    private void UpdateUI()
    {
        GameObject fallbackButton = null;
        for (int i = 0; i < taskSlots.Length; i++)
        {
            if (i < activeTasks.Count)
            {
                var task = activeTasks[i];
                taskSlots[i].SetActive(true);

                Text taskText = taskSlots[i].GetComponentInChildren<Text>();
                Image iconImage = taskSlots[i].transform.Find("Icon").GetComponent<Image>();
                Image shaodowImage = taskSlots[i].transform.Find("Shadow").GetComponent<Image>();

                taskText.text = task.GetDisplayText();
                iconImage.sprite = task.requiredItem.ItemImage;
                shaodowImage.sprite = task.requiredItem.ItemImage;

                shaodowImage.color = new Color(0, 0, 0, 0.5f);

                if (fallbackButton == null)
                {
                    Button btn = taskSlots[i].GetComponentInChildren<Button>();
                    if (btn != null && btn.gameObject.activeInHierarchy)
                        fallbackButton = btn.gameObject;
                }
            }
            else
            {
                GameObject selected = EventSystem.current.currentSelectedGameObject;
                if (selected != null && selected.transform.IsChildOf(taskSlots[i].transform))
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }

                taskSlots[i].SetActive(false);
            }
        }
        GameObject selected1 = EventSystem.current.currentSelectedGameObject;
        if (selected1 == null || !selected1.activeInHierarchy)
        {
            if (fallbackButton != null)
                EventSystem.current.SetSelectedGameObject(fallbackButton);
        }

    }

    private Task GenerateRandomTask()
    {
        //if (itemPool.Count == 0)
        //    return null;

        ItemSOBase selectedItem;
        //do
        //{
           selectedItem = itemPool[Random.Range(0, itemPool.Count)];
        //}
        //while (activeTasks.Exists(task => task.requiredItem == selectedItem) && activeTasks.Count < itemPool.Count);
        int randomAmount = Random.Range(1, 4); 

        return new Task
        {
            requiredItem = selectedItem,
            requiredAmount = randomAmount
        };
    }
    public void CompleteTaskItem(int index)
    {
        if (index < 0 || index >= activeTasks.Count) return;

        Task task = activeTasks[index];

        bool success = playerInventory.RemoveTaskItem(task.requiredItem, task.requiredAmount);
        Debug.Log(task.requiredAmount);
        shopManager.SellItem(task.requiredItem, task.requiredAmount);
        Debug.Log(task.requiredAmount);
        // Example call

        if (success)
        {
            Debug.Log("Task completed and items removed!");

            StartCoroutine(PlayCompleteEffectAndRemove(index, task));
        }
        else
        {
            Debug.Log("Task not completed. Not enough items.");
        }
    }

    private IEnumerator PlayCompleteEffectAndRemove(int index, Task task)
    {
        GameObject slot = taskSlots[index];
        Transform effect = slot.transform.Find("CompleteEffect");

        if (effect != null)
        {
            effect.gameObject.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(1.5f); // Wait for visual feedback

        if (effect != null)
        {
            effect.gameObject.SetActive(false);
        }

        // Sell the item after effect is shown
        shopManager.SellItem(task.requiredItem, task.requiredAmount);

        // Safely remove the task
        if (index >= 0 && index < activeTasks.Count)
        {
            activeTasks.RemoveAt(index);
        }

        UpdateUI();

        // Spawn new task after additional delay
        yield return new WaitForSecondsRealtime(4f);

        if (activeTasks.Count < 3)
        {
            Task newTask = GenerateRandomTask();
            activeTasks.Add(newTask);
            Debug.Log($"New Task: {newTask.GetDisplayText()}");
            UpdateUI();
        }
    }

}


