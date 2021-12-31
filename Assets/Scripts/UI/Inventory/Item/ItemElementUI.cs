using System.Collections;
using System.Collections.Generic;
using UI.Selector;
using UnityEngine;
using Inventory.Items;
using TMPro;
using UnityEngine.UI;
using Utility;

namespace UI.Inventory.Item
{
    public class ItemElementUI : SelectorElementUI
    {
        private InventoryItem displayItem = null;

        /// <summary>
        /// The item this item element is displaying
        /// </summary>
        public InventoryItem DisplayItem
        {
            get
            {
                return displayItem;
            }
        }

        private int count = -1;

        [SerializeField]
        TextMeshProUGUI itemName, itemCount;

        [SerializeField]
        Image itemThumbnail;

        /// <summary>
        /// Sets the display item for this element
        /// </summary>
        /// <param name="item"></param>
        public void SetItemStack(InventoryItem _item, int _count)
        {
            count = _count;
            displayItem = _item;
        }

        public override void HandlePrintingState()
        {
            SetInfo();
            base.HandlePrintingState();           
        }

        private void SetInfo()
        {
            if (displayItem != null)
            {
                Prettify prettify = new Prettify();
                itemName.text = prettify.Pretty(displayItem.ItemName, false);
                itemCount.text = "" + count;
                if (displayItem.ItemThumbnail != null)
                {
                    itemThumbnail.sprite = displayItem.ItemThumbnail;
                }
            }
        }

        public override void HandleHidingState()
        {
            base.HandleHidingState();
            itemName.text = "";
            itemThumbnail.sprite = null;
        }
    }
}
