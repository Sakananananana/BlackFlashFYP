using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfigSO", menuName = "Scriptable Objects /Entity Configs /HealthConfigSO")]
public class HealthConfigSO : ScriptableObject
{
    [SerializeField] private int _initialHealth;

    public int InitialHealth => _initialHealth;
}
