using UnityEngine;
using System.Collections.Generic;


namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "CraftableItemSO", menuName = "Scriptable Objects /Inventory /ItemSO /CraftableItemSO")]
    public class CraftableItemSO : ItemSOBase
    {
        [SerializeField] public List<ItemSOBase> Ingredients = new List<ItemSOBase>();
    }
}

