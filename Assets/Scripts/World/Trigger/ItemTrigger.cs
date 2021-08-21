using Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Trigger
{
    /// <summary>
    /// Trigger for doing various item events
    /// </summary>
    public class ItemTrigger : MonoBehaviour
    {
        [SerializeField]
        private InventoryItem item;

        [SerializeField]
        private ItemTriggerType triggerType;

        [SerializeField]
        private int countChange;

        /// <summary>
        /// Handles ItemTrigger event.
        /// Returns true if successful.
        /// </summary>
        /// <returns></returns>
        public void FireTrigger()
        {
            switch (triggerType)
            {
                case ItemTriggerType.Add:
                    Core.CoreManager.Instance.playerInventory.Inventory.AddItem(item.ItemName, countChange);
                    break;
                case ItemTriggerType.Remove:
                    bool value = Core.CoreManager.Instance.playerInventory.Inventory.RemoveItem(item.ItemName, countChange);
                    if (!value)
                    {
                        throw new System.Exception("ItemTrigger Error : Attempted to remove an item that the player did not have or not have enough of: " + item.ItemName + " Count: " + countChange);
                    }
                    break;
            }
        }
    }

    public enum ItemTriggerType
    {
        Add,
        Remove
    }
}
