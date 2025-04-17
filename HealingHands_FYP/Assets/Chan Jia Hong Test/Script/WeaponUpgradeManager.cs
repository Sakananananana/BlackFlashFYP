using UnityEngine;

public class WeaponUpgradeManager : MonoBehaviour
{
    [SerializeField] private AttackConfigSO attackConfig;
    [SerializeField] private int baseDamage = 5;
    [SerializeField] private int damagePerLevel = 5;
    [SerializeField] private int maxUpgradeLevel = 5;

    private const string UpgradeKey = "WeaponUpgradeLevel";

    private void Awake()
    {
        ApplySavedUpgrade();
    }

    public void UpgradeWeapon()
    {
        int currentLevel = PlayerPrefs.GetInt(UpgradeKey, 0);

        if (currentLevel < maxUpgradeLevel)
        {
            currentLevel++;
            PlayerPrefs.SetInt(UpgradeKey, currentLevel);
            PlayerPrefs.Save();

            ApplyUpgrade(currentLevel);
            Debug.Log($"Upgraded weapon to level {currentLevel}");
        }
        else
        {
            Debug.Log("Weapon is already max level.");
        }
    }

    public void ApplySavedUpgrade()
    {
        int savedLevel = PlayerPrefs.GetInt(UpgradeKey, 0);
        ApplyUpgrade(savedLevel);
    }

    private void ApplyUpgrade(int level)
    {
        int newDamage = baseDamage + (level * damagePerLevel);
        attackConfig.OverrideRuntimeDamage(newDamage); // Pass data into SO at runtime

#if UNITY_EDITOR
        attackConfig.SetAttackDamage(newDamage); // Optional: save to asset in editor
#endif
    }
}
