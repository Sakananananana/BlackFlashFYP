using UnityEngine;
using UnityEngine.EventSystems;
using Inventory.Model;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public InventorySO inventory;
    public int totalCoins;
    [SerializeField] GameObject _firstButton;
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private AttackConfigSO attackConfigSO;
    //private WeaponStats currentWeaponStats;

    [SerializeField] private VoidEventChannelSO _onSceneReady;
    [SerializeField] private VoidEventChannelSO _onWarpPerformed;
    public BoolEventChannelSO IsCoinLessThanThresold;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI coinText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 可选：保留在场景切换时
        }
        else
        {
            Destroy(gameObject); // 如果有重复的，就销毁新的
        }
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // default 0 if no save
        UpdateCoinUI();
    }

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += CheckWarpAvailable;
        _onWarpPerformed.OnEventRaised += WarpRemoveCoins;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= CheckWarpAvailable;
        _onWarpPerformed.OnEventRaised -= WarpRemoveCoins;
    }

    private void CheckWarpAvailable()
    {
        Debug.Log("Event Raised");
        IsCoinLessThanThresold.RaiseEvent(totalCoins < 400);
    }

    private void Update()
    {
        SaveCoins();
    }

    public void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
            StartCoroutine(SelectFirstButtonNextFrame(0.5f));
        }
    }

    public void CloseShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }

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
        //int remaining = quantity;
        //foreach (var slotIndex in inventoryState.Keys)
        //{
        //    var slot = inventoryState[slotIndex];
        //    if (slot.Item == itemToSell)
        //    {
        //        int toRemove = Mathf.Min(remaining, slot.ItemQuantity);
        //        for (int i = 0; i < toRemove; i++)
        //        {
        //            inventory.RemoveItem(slotIndex);
        //        }

        //        remaining -= toRemove;
        //        if (remaining <= 0) break;
        //    }
        //}

        // Determine price
        int valuePerItem = 0;
        if (itemToSell is CraftableItemSO)
            valuePerItem = 500;
        else if (itemToSell is MaterialItemSO)
            valuePerItem = 200;

        int earnedCoins = valuePerItem * quantity;
        totalCoins += earnedCoins;

        UpdateCoinUI();
        IsCoinLessThanThresold.RaiseEvent(totalCoins < 400);

        //Debug.Log($"Sold {quantity} {itemToSell.ItemName} for {earnedCoins} coins!");
    }

    public void UpdateCoinUI()
    {
        coinText.text = $"Coins: {totalCoins}";
    }

    private void OnApplicationQuit()
    {
        SaveCoins();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save(); // optional, but makes it immediate
    }

    private void WarpRemoveCoins()
    {
        Debug.Log(totalCoins + "removed");
        totalCoins -= 400;
        UpdateCoinUI();

        IsCoinLessThanThresold.RaiseEvent(totalCoins < 400);
    }

    private IEnumerator SelectFirstButtonNextFrame(float delay)
    {
        //yield return null; // Wait one frame
        EventSystem.current.SetSelectedGameObject(null); // Clear selection first (optional but safer)
        yield return new WaitForSecondsRealtime(delay);
        EventSystem.current.SetSelectedGameObject(_firstButton);
    }
}

