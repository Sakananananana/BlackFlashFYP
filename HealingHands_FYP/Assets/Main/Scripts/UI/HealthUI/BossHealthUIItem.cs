using UnityEngine;

[CreateAssetMenu(fileName = "BossHealthUIItem", menuName = "Scriptable Objects/BossHealthUIItem")]
public class BossHealthUIItem : ScriptableObject
{
    public string BossTitle;
    public VoidEventChannelSO _voidEvent;
    public HealthSO _healthSO;
}
