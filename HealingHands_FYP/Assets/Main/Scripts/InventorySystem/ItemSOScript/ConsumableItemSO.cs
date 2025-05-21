using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable[Name]", menuName = "Scriptable Objects /Inventory /ItemSO /Consumable ItemSO")]
public class ConsumableItemSO : ItemSOBase
{
    [Tooltip("How much health this item restores when used.")]
    [SerializeField] private int _healAmount = 10;
    public int HealAmount => _healAmount;
}
