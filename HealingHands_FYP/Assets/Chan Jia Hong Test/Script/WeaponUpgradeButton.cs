using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;


public class WeaponUpgradeButton : MonoBehaviour
{
    public ShopManager shopManager;
    [SerializeField] private AttackConfigSO attackConfig;
    [SerializeField] private WeaponUpgradeManager upgradeManager;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int pricePerUpgrade = 1000;
    [SerializeField] private int maxUpgradeLevel = 5;

    [SerializeField] GameObject _firstButton;
    [SerializeField] private GameObject weaponPanel;
    private bool isProcessing = false;

    private void Start()
    {
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        UpdateUI();
    }
    public void Update()
    {
        UpdateUI();
    }

    public void OpenWeapon()
    {
        if (weaponPanel != null)
        {
            EventSystem.current.SetSelectedGameObject(_firstButton);
            weaponPanel.SetActive(true);
        }
    }

    public void CloseWeapon()
    {
        if (weaponPanel != null)
        {
            weaponPanel.SetActive(false);
        }
    }

    public void OnUpgradeClicked()
    {
        if (isProcessing) return; // Prevent multiple clicks
        isProcessing = true;

        int currentLevel = PlayerPrefs.GetInt("WeaponUpgradeLevel", 0);

        if (currentLevel < maxUpgradeLevel && ShopManager.Instance.totalCoins >= pricePerUpgrade)
        {
            // Deduct coins
            ShopManager.Instance.totalCoins -= pricePerUpgrade;

            // Save coins
            PlayerPrefs.SetInt("TotalCoins", ShopManager.Instance.totalCoins);
            //PlayerPrefs.SetInt("WeaponUpgradeLevel", currentLevel += 1);
            PlayerPrefs.Save();

            // Upgrade weapon
            upgradeManager.UpgradeWeapon();

            // Update UI
            UpdateUI();
            ShopManager.Instance.IsCoinLessThanThresold.RaiseEvent(ShopManager.Instance.totalCoins < 400);
        }
        else
        {
            Debug.Log("Not enough coins or max level reached.");
        }

        // Release lock after short delay in case of animations or visual feedback
        StartCoroutine(UnlockAfterDelay(0.1f));
    }

    private IEnumerator UnlockAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Realtime to ignore Time.timeScale = 0
        isProcessing = false;
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
//    private void ApplySavedUpgrade()
//    {
//        int savedLevel = PlayerPrefs.GetInt("WeaponUpgradeLevel", 0);
//        ApplyUpgrade(savedLevel);
//    }

//    private void ApplyUpgrade(int level)
//    {
//        int newDamage = baseDamage + (level * damagePerLevel);
//        attackConfig.OverrideRuntimeDamage(newDamage);

//#if UNITY_EDITOR
//        attackConfig.SetAttackDamage(newDamage); // Optional: only updates asset in editor
//#endif

//        Debug.Log($"Weapon upgraded to level {level} | Damage: {newDamage}");
//        UpdateUI();
    //}

  
}

