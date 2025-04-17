using UnityEngine;

public class DebugReset : MonoBehaviour
{
    [SerializeField] private AttackConfigSO attackConfigSO;
    [SerializeField] private int defaultDamage = 5;
    private const string UpgradeKey = "WeaponUpgradeLevel";

    public void ResetWeaponUpgrade()
    {
        PlayerPrefs.DeleteKey("WeaponUpgradeLevel");
        attackConfigSO.OverrideRuntimeDamage(defaultDamage);
        PlayerPrefs.GetInt(UpgradeKey, 0);
        PlayerPrefs.Save();
        Debug.Log("Weapon damage has been reset to default.");
    }
}
