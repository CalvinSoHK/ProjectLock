using Inventory;
using Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class ItemMaster : MonoBehaviour
    {
        /// <summary>
        /// Master list of all items in the game.
        /// Useful if you need to find something not based on ID.
        /// </summary>
        [SerializeField]
        private List<InventoryItem> masterList = new List<InventoryItem>();

        /// <summary>
        /// Master dict of all items in the game.
        /// Useful for look up by item ID.
        /// </summary>
        [SerializeField]
        private Dictionary<int, InventoryItem> masterDict = new Dictionary<int,InventoryItem>();

        /// <summary>
        /// Loads items from addressables
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            InventoryItemLoader loader = new InventoryItemLoader();
            masterList = await loader.LoadAllItems();

            foreach(InventoryItem item in masterList)
            {
                masterDict.Add(item.ItemID, item);
            }
        }

        /// <summary>
        /// Checks a given item is valid (part of master list)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ValidItem(InventoryItem item)
        {
            if (masterList.Contains(item))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a given itemID is valid
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public bool ValidItem(int itemID)
        {
            foreach(InventoryItem item in masterList)
            {
                if(item.ItemID == itemID)
                {
                    return true;
                }
            }
            Debug.LogWarning("ItemID not valid: " + itemID);
            return false;
        }

        /// <summary>
        /// Checks if a given item name is a valid item
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool ValidItem(string itemName)
        {
            foreach (InventoryItem item in masterList)
            {
                if (item.ItemName.Equals(itemName))
                {
                    return true;
                }
            }
            Debug.LogWarning("ItemID not valid: " + itemName);
            return false;
        }
    
        /// <summary>
        /// Grabs item based on itemID
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public InventoryItem GetItem(int itemID)
        {
            InventoryItem value;
            if(masterDict.TryGetValue(itemID, out value))
            {
                return value;
            }
            throw new System.Exception("ItemID is not valid: " + itemID);
        }

        /// <summary>
        /// Grabs item based on item name.
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public InventoryItem GetItem(string itemName)
        {
            Debug.LogWarning("Warning: Getting items by name is possible but not performant. Will be deprecated in the future.");
            foreach(InventoryItem item in masterList)
            {
                if (item.ItemName.Equals(itemName))
                {
                    return item;
                }
            }
            throw new System.Exception("ItemName is not valid: " + itemName);
        }

        /// <summary>
        /// Returns a list of items based on a list of InventoryItemInfo
        /// List of InventoryItemInfo is returned when grabbing items from an inventory
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public List<InventoryItem> GetItemList(List<ItemStack> itemList)
        {
            List<InventoryItem> validList = new List<InventoryItem>();
            InventoryItem item;
            foreach(ItemStack info in itemList)
            {
                if(masterDict.TryGetValue(info.ItemID, out item))
                {
                    validList.Add(item);
                }
                else
                {
                    throw new System.Exception("Attempted to grab an invalid item ID: " + info.ItemID);
                }
            }
            return validList;          
        }
    }
}
