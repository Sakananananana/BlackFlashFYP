using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfigSO", menuName = "Scriptable Objects /Entity Configs /AttackConfigSO")]
public class AttackConfigSO : ScriptableObject
{
    [SerializeField] private int _attackDamage;

    public int AttackDamage => _attackDamage;
}
