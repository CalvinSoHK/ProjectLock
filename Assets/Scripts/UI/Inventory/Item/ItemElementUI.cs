using System.Collections;
using System.Collections.Generic;
using UI.Selector;
using UnityEngine;
using Inventory.Items;
using TMPro;
using UnityEngine.UI;

namespace UI.Inventory.Item
{
    public class ItemElementUI : SelectorElementUI
    {
        private InventoryItem displayItem = null;

        [SerializeField]
        TextMeshProUGUI itemName;

        [SerializeField]
        Image itemThumbnail;

        public override void HandlePrintingState()
        {
            base.HandlePrintingState();

            if(displayItem != null)
            {
                itemName.text = displayItem.ItemName;
                itemThumbnail.sprite = displayItem.ItemThumbnail;
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
