using System;
using UnityEngine;
using System.Collections.Generic;


namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "CraftableItemSO", menuName = "Scriptable Objects /Inventory /ItemSO /CraftableItemSO")]
    public class CraftableItemSO : ItemSOBase
    {
        [SerializeField] public List<ItemSOBase> Ingredients = new List<ItemSOBase>();
        [NonSerialized] private bool _isCraftedBefore;

        public bool IsCraftedBefore
        {
            get => _isCraftedBefore;
            set => _isCraftedBefore = value;
        }
    }
}

