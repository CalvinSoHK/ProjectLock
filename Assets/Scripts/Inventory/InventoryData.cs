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
        /// Inventory item loader. Uses Addressables to laod items
        /// </summary>
        private InventoryItemLoader loader = new InventoryItemLoader();

        /// <summary>
        /// Dictionary of item count
        /// </summary>
        private ConcurrentDictionary<string, int> itemDict = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// List of items that we have
        /// </summary>
        [SerializeField]
        private List<string> itemList = new List<string>();

        /// <summary>
        /// List of items that the inventory contains.
        /// Does not allow changing
        /// </summary>
        public List<string> ItemList
        {
            get
            {
                return itemList;
            }
        }

        /// <summary>
        /// Preloads items into inventory based on serialized list in inspector
        /// </summary>
        public void Preload()
        {
            foreach(string itemName in ItemList)
            {
                AddItem(itemName, 1, true);
            }
        }

        /// <summary>
        /// Checks that the given name is a valid item.
        /// Item is not valid if there is no scriptable object in addressable system with it's item name
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public async Task<bool> ValidItem(string itemName, bool releaseAfter = true)
        {
            if(await loader.LoadItem(itemName) != null)
            {
                if (releaseAfter)
                {
                    loader.UnloadItem(itemName);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if an given item name is in the inventory
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool HasItem(string itemName)
        {
            return itemList.Contains(itemName);
        }

        /// <summary>
        /// Returns the item count of a given item
        /// Returns -1 if not valid, though we should use HasItem before this call
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public int GetItemCount(string itemName)
        {
            int count = -1;
            itemDict.TryGetValue(itemName, out count);
            return count;
        }

        /// <summary>
        /// Adds an item to inventory.
        /// Defaults to adding 1 but can specify
        /// Will not do anything with itemCount equal to or below 0
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCount"></param>
        public void AddItem(string itemName, int itemCount = 1, bool preload = false)
        {
            if(itemCount > 0)
            {
                itemDict.AddOrUpdate(itemName, itemCount, (key, oldValue) => oldValue + itemCount);

                if (!itemList.Contains(itemName) && !preload)
                {
                    itemList.Add(itemName);
                }
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
        public bool RemoveItem(string itemName, int itemCount = 1)
        {
            int curCount = -1;
            if(itemDict.TryGetValue(itemName, out curCount))
            {
                //If the cur count is greater than item count, update value
                if (curCount > itemCount)
                {
                    return itemDict.TryUpdate(itemName, curCount - itemCount, curCount);
                }
                else //If the cur count is equal to or less than item count, remove it
                {
                    itemList.Remove(itemName);
                    return itemDict.TryRemove(itemName, out curCount);
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
        public async Task<bool> UseItem(string itemName)
        {
            if (HasItem(itemName))
            {
                RemoveItem(itemName, 1);
                InventoryItem item = await loader.LoadItem(itemName);
                item.OnUse();
                loader.UnloadItem(itemName);
                return true;
            }
            return false;          
        }
    }
}
