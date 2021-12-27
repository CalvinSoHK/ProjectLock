using Inventory.Enums;
using Inventory.Items;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Inventory system
    /// </summary>
    [System.Serializable]
    public class InventoryData
    {
        /// <summary>
        /// Dictionary of item count
        /// Key : ItemID
        /// Value: ItemCount
        /// </summary>
        private ConcurrentDictionary<int, int> itemDict = new ConcurrentDictionary<int, int>();

        /// <summary>
        /// Initial inventory. Is preloaded at start and then destroyed.
        /// </summary>
        [SerializeField]
        private List<ItemStack> initInventory = new List<ItemStack>();

        /// <summary>
        /// Preloads the inputted data
        /// </summary>
        public void Preload()
        {
            foreach(ItemStack stack in initInventory)
            {
                AddItem(stack.ItemID, stack.ItemCount);
            }
        }

        /// <summary>
        /// Checks to see if a given item name is in inventory
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool HasItem(int itemID)
        {
            return itemDict.ContainsKey(itemID);
        }

        /// <summary>
        /// Returns the item count of a given item
        /// Returns -1 if not valid, though we should use HasItem before this call
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetItemCount(int itemID)
        {
            int count = -0;
            try
            {
                itemDict.TryGetValue(itemID, out count);
                return count;
            }
            catch
            {
                //If we fail count is 0
                return 0;
            }     
        }

        /// <summary>
        /// Adds an item to inventory.
        /// Defaults to adding 1 but can specify
        /// Will not do anything with itemCount equal to or below 0
        /// </summary>
        /// <param name="item"></param>
        /// <param name="itemCount"></param>
        public void AddItem(int itemID, int itemCount = 1)
        {
            if(itemCount > 0)
            {
                itemDict.AddOrUpdate(itemID, itemCount, (key, oldValue) => oldValue + itemCount);
            }         
        }

        /// <summary>
        /// Removes an item from inventory.
        /// Defaults to removing 1 but can specify
        /// Will not do anything if item is not in inventory
        /// Will not do anything with itemCount equal to or above 0
        /// Returns ture if successful, else false
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCount"></param>
        public bool RemoveItem(int itemID, int itemCount = 1)
        {
            int curCount = 0;
            if(itemDict.TryGetValue(itemID, out curCount))
            {
                //If the cur count is greater than item count, update value
                if (curCount > itemCount)
                {
                    return itemDict.TryUpdate(itemID, curCount - itemCount, curCount);
                }
                else //If the cur count is equal to or less than item count, remove it
                {
                    return itemDict.TryRemove(itemID, out curCount);
                }
            }

            //Fails if it didn't return anything by this point
            return false;
        }
    
        /// <summary>
        /// Uses an item from inventory
        /// Returns true if operation successful
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool UseItem(int itemID)
        {
            if (HasItem(itemID))
            {
                RemoveItem(itemID, 1);
                InventoryItem item = Core.CoreManager.Instance.itemMaster.GetItem(itemID);
                item.OnUse();
                return true;
            }
            return false;          
        }

        /// <summary>
        /// Grabs all items that fall under this mask
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public List<ItemStack> GetItems(ItemMask mask)
        {
            List<ItemStack> validItems = new List<ItemStack>();
            ItemMaskHelper maskHelper = new ItemMaskHelper();

            foreach (int key in itemDict.Keys)
            {
                InventoryItem item = Core.CoreManager.Instance.itemMaster.GetItem(key);
                if (maskHelper.MaskContains(item.ItemMask, mask))
                {
                    int count = 0;
                    itemDict.TryGetValue(key, out count);
                    validItems.Add(new ItemStack(key, count));
                }
            }
            return validItems;
        }

        /// <summary>
        /// Grabs all items that fall under a given category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<ItemStack> GetItems(ItemCategory category)
        {
            List<ItemStack> validItems = new List<ItemStack>();
            ItemMaskHelper maskHelper = new ItemMaskHelper();

            foreach (int key in itemDict.Keys)
            {
                InventoryItem item = Core.CoreManager.Instance.itemMaster.GetItem(key);
                if (item.ItemCategory == category)
                {
                    int count = 0;
                    itemDict.TryGetValue(key, out count);
                    validItems.Add(new ItemStack(key, count));
                }
            }
            return validItems;
        }

        /// <summary>
        /// Grabs all items that match the given category and mask
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<ItemStack> GetItems(ItemMask mask, ItemCategory category)
        {
            List<ItemStack> validItems = new List<ItemStack>();
            ItemMaskHelper maskHelper = new ItemMaskHelper();

            foreach(int key in itemDict.Keys)
            {
                InventoryItem item = Core.CoreManager.Instance.itemMaster.GetItem(key);
                if (maskHelper.MaskContains(item.ItemMask, mask) && item.ItemCategory == category)
                {
                    int count = 0;
                    itemDict.TryGetValue(key, out count);
                    validItems.Add(new ItemStack(key, count));
                }            
            }
            return validItems;
        }
    }

    /// <summary>
    /// Item stack includes an itemID and how many of that item are in the stack
    /// </summary>
    [System.Serializable]
    public class ItemStack
    {
        [SerializeField]
        private int itemID, itemCount;
        public int ItemID
        {
            get
            {
                return itemID;
            }
        }

        public int ItemCount
        {
            get
            {
                return itemCount;
            }
        }

        public ItemStack(int _id, int _count)
        {
            itemID = _id;
            itemCount = _count;
        }
    }
}
