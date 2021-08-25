using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Enums;

namespace Inventory.Items
{
    [CreateAssetMenu(fileName = "BaseInventoryItem",
        menuName = "Items/BaseInventoryItem", order = 7)]
    public class BaseInventoryItem : InventoryItem
    {
        /// <summary>
        /// Constructor for BaseInventoryItem
        /// Used only for testing.
        /// </summary>
        /// <param name="_itemName"></param>
        /// <param name="_itemMask"></param>
        /// <param name="_itemCategory"></param>
        /// <param name="_onUseString"></param>
        public BaseInventoryItem(string _itemName, ItemMask _itemMask, ItemCategory _itemCategory, string _onUseString) 
            : base (_itemName, _itemMask, _itemCategory, _onUseString)
        {

        }

        public override void OnUse()
        {
            Core.CoreManager.Instance.dialogueManager.FireDialogue(new Core.Dialogue.DialogueObject()
            {
                dialogueText = OnUseString,
                requestConfirm = false,
                hasNext = false,
                sceneName = "",
                isNotScene = true
            });
        }
    }
}
