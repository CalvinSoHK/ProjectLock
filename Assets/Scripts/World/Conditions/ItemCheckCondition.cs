using Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Condition
{
    /// <summary>
    /// Checks if the player has the inputted item with the given itemCount
    /// </summary>
    public class ItemCheckCondition : BaseCondition
    {
        [SerializeField]
        private InventoryItem item;

        [SerializeField]
        [Range(1, int.MaxValue)]
        private int itemCount;
        public override void CheckCondition()
        {
            bool hasItem = Core.CoreManager.Instance.playerInventory.Inventory.HasItem(item.ItemID);
            if(itemCount == 1)
            {
                OnCondition?.Invoke(hasItem, ConditionID);
            }
            else if (hasItem && itemCount <= Core.CoreManager.Instance.playerInventory.Inventory.GetItemCount(item.ItemID))
            {
                OnCondition?.Invoke(true, ConditionID);
            }
            else
            {
                OnCondition?.Invoke(false, ConditionID);
            }
        }
    }
}
