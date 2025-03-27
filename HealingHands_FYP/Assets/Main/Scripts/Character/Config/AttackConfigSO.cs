using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfigSO", menuName = "Scriptable Objects /Entity Configs /AttackConfigSO")]
public class AttackConfigSO : ScriptableObject
{
    [SerializeField] private float _attackDamage;

    public float AttackDamage => _attackDamage;
}
