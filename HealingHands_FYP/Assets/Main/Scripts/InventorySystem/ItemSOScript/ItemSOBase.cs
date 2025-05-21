using System.IO;
using UnityEditor;
using UnityEngine;

namespace Inventory.Model
{
    public class ItemSOBase : ScriptableObject
    {
        public int ID => GetInstanceID();

        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public string ItemName { get; set; }

        [field: SerializeField]
        [field: TextArea] public string ItemDescription { get; set; }

        [field: SerializeField]
        public bool IsItemCollected { get; set; }

        [Header("Effect")]
        [SerializeField] private bool isHealingItem = false;
        [SerializeField] private int healingAmount = 0;

        public bool IsHealingItem => isHealingItem;
        public int HealingAmount => healingAmount;
    }
}