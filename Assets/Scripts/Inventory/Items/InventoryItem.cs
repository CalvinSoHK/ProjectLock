using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Enums;

namespace Inventory.Items
{
    /// <summary>
    /// Basic inventory item
    /// </summary>
    public abstract class InventoryItem : ScriptableObject
    {
        [SerializeField]
        private string itemName;

        public string ItemName { get { return itemName; } }

        [SerializeField]
        private ItemMask itemMask;

        public ItemMask ItemMask { get { return itemMask; } }

        [SerializeField]
        private ItemCategory itemCategory;

        public ItemCategory ItemCategory { get { return itemCategory; } }

        [SerializeField]
        private string onUseString;
        public string OnUseString { get { return onUseString; } }

        /// <summary>
        /// Constructs an InventoryItem.
        /// Only used in testing.
        /// </summary>
        /// <param name="_itemName"></param>
        /// <param name="_itemMask"></param>
        /// <param name="_itemCategory"></param>
        /// <param name="_onUseString"></param>
        public InventoryItem(string _itemName, ItemMask _itemMask, ItemCategory _itemCategory, string _onUseString)
        {
            itemName = _itemName;
            itemMask = _itemMask;
            itemCategory = _itemCategory;
            onUseString = _onUseString;
        }

        public abstract void OnUse();
    }
}
