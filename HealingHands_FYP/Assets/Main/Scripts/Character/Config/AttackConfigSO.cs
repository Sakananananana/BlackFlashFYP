using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfigSO", menuName = "Scriptable Objects /Entity Configs /AttackConfigSO")]
public class AttackConfigSO : ScriptableObject
{
    [SerializeField] private int _attackDamage;

    public int AttackDamage => _attackDamage;

#if UNITY_EDITOR
    public void SetAttackDamage(int newDamage)
    {
        _attackDamage = newDamage;
        UnityEditor.EditorUtility.SetDirty(this); // Save change to asset
    }
#endif

    // For runtime override only (not saved to file)
    public void OverrideRuntimeDamage(int newDamage)
    {
        _attackDamage = newDamage;
    }
}
