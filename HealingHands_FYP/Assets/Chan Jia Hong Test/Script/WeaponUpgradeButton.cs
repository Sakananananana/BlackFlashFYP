using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUpgradeButton : MonoBehaviour
{
    public ShopManager shopManager;
    [SerializeField] private WeaponUpgradeManager upgradeManager;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int pricePerUpgrade = 1000;
    [SerializeField] private int maxUpgradeLevel = 5;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        UpdateUI();
    }
    public void Update()
    {
        UpdateUI();
    }

    public void OnUpgradeClicked()
    {
        int currentLevel = PlayerPrefs.GetInt("WeaponUpgradeLevel", 0);

        if (currentLevel < maxUpgradeLevel && ShopManager.Instance.totalCoins >= pricePerUpgrade)
        {
            // Deduct coins
            ShopManager.Instance.totalCoins -= pricePerUpgrade;

            // Save coins
            PlayerPrefs.SetInt("TotalCoins", ShopManager.Instance.totalCoins);
            PlayerPrefs.SetInt("WeaponUpgradeLevel", currentLevel + 1);
            PlayerPrefs.Save();

            // Upgrade weapon
            upgradeManager.UpgradeWeapon();

            // Update UI
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough coins or max level reached.");
        }
    }


    private void UpdateUI()
    {
        int currentLevel = PlayerPrefs.GetInt("WeaponUpgradeLevel", 0);
        shopManager.UpdateCoinUI();

        if (currentLevel >= maxUpgradeLevel)
        {
            priceText.text = "Max Level";
            upgradeButton.interactable = false;
        }
        else
        {
            priceText.text = $"Upgrade ({pricePerUpgrade} coins)";
            upgradeButton.interactable = ShopManager.Instance.totalCoins >= pricePerUpgrade;
        }
    }
}

