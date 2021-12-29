using System.Collections;
using System.Collections.Generic;
using UI.Selector;
using UnityEngine;
using Inventory.Items;
using Inventory;

namespace UI.Inventory.Item
{
    public class ItemModelUI : SelectorModelUI
    {
        private bool update = false;
        /// <summary>
        /// Whether or not the dropdown was updated
        /// </summary>
        public bool Update
        {
            get
            {
                return update;
            }
        }

        public void SetUpdate(bool _state)
        {
            update = _state;
        }

        private List<ItemStack> displayItems;

        /// <summary>
        /// List of InventoryItems that need to be displayed
        /// </summary>
        public List<ItemStack> DisplayItems
        {
            get
            {
                return displayItems;
            }
        }

        /// <summary>
        /// Sets the display items in the model
        /// </summary>
        /// <param name="_displayItems"></param>
        public void SetDisplayItems(List<ItemStack> _displayItems)
        {
            displayItems = _displayItems;
        }

        public delegate void ItemModel(string key, ItemModelUI model);
        public new static ItemModel ModelUpdate;

        protected override void InvokeSpecificModel(string _key)
        {
            ModelUpdate?.Invoke(_key, this);
        }
    }
}
