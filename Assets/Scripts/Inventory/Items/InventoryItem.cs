using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Enums;
using UnityEngine.UI;

namespace Inventory.Items
{
    /// <summary>
    /// Basic inventory item
    /// </summary>
    public abstract class InventoryItem : ScriptableObject
    {
        [SerializeField]
        private int itemID;

        /// <summary>
        /// ItemID for this type of item. 
        /// Should be unique per type of item
        /// </summary>
        public int ItemID
        {
            get
            {
                return itemID;
            }
        }

        [SerializeField]
        private string itemName;

        public string ItemName { get { return itemName; } }

        [SerializeField]
        private string itemDescription;

        public string ItemDescription { get { return itemDescription; } }

        [SerializeField]
        private Sprite thumbnail;
        public Sprite ItemThumbnail { get { return thumbnail; } }

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
