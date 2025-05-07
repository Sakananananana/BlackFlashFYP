using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Inventory.Model;
using PlayerInputSystem;

public class BookManager : MonoBehaviour
{
    [SerializeField] private InventorySO inventorySO;
    [SerializeField] private List<ItemSOBase> allItems;
    [SerializeField] private ItemSOBase unknownItem;
    [SerializeField] private GameObject pagePrefab;
    [SerializeField] private Transform pageParent;
    [SerializeField] private InputReader inputReader;


    private List<GameObject> itemPages = new List<GameObject>();
    private int currentPageIndex = 0;

    private void Awake()
    {
        ResetCollection();
    }
    private void Start()
    {
        GenerateAllPages();
        ShowPage(0);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        NextPage();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        PreviousPage();
    //    }
    //}
    private void OnEnable()
    {
        inputReader.NextPageEvent += NextPage;
        inputReader.PreviousPageEvent += PreviousPage;
    }

    private void OnDisable()
    {
        inputReader.NextPageEvent -= NextPage;
        inputReader.PreviousPageEvent -= PreviousPage;
    }

    private void GenerateAllPages()
    {
        foreach (var item in allItems)
        {
            GameObject page = Instantiate(pagePrefab, pageParent);

            // Update content
            UpdatePageUI(page, item);
            page.SetActive(false);
            itemPages.Add(page);
        }
    }

    private void UpdatePageUI(GameObject page, ItemSOBase item)
    {
        Image image = page.transform.Find("ItemImage").GetComponent<Image>();
        TMPro.TextMeshProUGUI nameText = page.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI descText = page.transform.Find("ItemDescription").GetComponent<TMPro.TextMeshProUGUI>();

        if (item.IsItemCollected)
        {
            image.sprite = item.ItemImage;
            nameText.text = item.ItemName;
            descText.text = item.ItemDescription;
        }
        else
        {
            image.sprite = unknownItem.ItemImage;
            nameText.text = unknownItem.ItemName;
            descText.text = unknownItem.ItemDescription;
        }
    }

    public void ShowPage(int index)
    {
        if (index < 0 || index >= itemPages.Count) return;

        foreach (var page in itemPages)
        {
            page.SetActive(false);
        }

        itemPages[index].SetActive(true);
        currentPageIndex = index;
    }

    private void NextPage()
    {
        if (currentPageIndex < itemPages.Count - 1)
        {
            ShowPage(currentPageIndex + 1);
        }
    }

    private void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            ShowPage(currentPageIndex - 1);
        }
    }

    public void RefreshAllPages()
    {
        for (int i = 0; i < itemPages.Count; i++)
        {
            UpdatePageUI(itemPages[i], allItems[i]);
        }
    }

    public void ResetCollection()
    {
        foreach (var item in allItems)
        {
            item.IsItemCollected = false;
        }

        RefreshAllPages(); // Optional: refresh the book UI if it's open
    }
}

