using Core.AddressableSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Inventory.Items;
using System.Threading.Tasks;

namespace Inventory
{
    /// <summary>
    /// Loads an item from scriptable objects
    /// </summary>
    public class InventoryItemLoader : MonoBehaviour
    {
        private const string ITEM_PATH = "InventoryItem/";

        /// <summary>
        /// Loaded item paths
        /// </summary>
        private List<string> loadedPaths = new List<string>();

        AddressablesManager addressablesManager
        {
            get
            {
                return CoreManager.Instance.addressablesManager;
            }
        }

        /// <summary>
        /// Returns a list of all items given tag "Item"
        /// </summary>
        /// <returns></returns>
        public async Task<List<InventoryItem>> LoadAllItems()
        {
            IList<InventoryItem> itemIList = await addressablesManager.LoadAddressablesByTag<InventoryItem>("ItemData");
            List<InventoryItem> itemList = new List<InventoryItem>();
            //Iterate through all loaded moves
            foreach (InventoryItem item in itemIList)
            {
                itemList.Add(item);
            }

            //Set as ready
            return itemList;
        }
        
        /// <summary>
        /// Loads an item from addressables
        /// Returns null if wasn't a valid item name
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public async Task<InventoryItem> LoadItem(string itemName)
        {
            string path = ConstructPath(itemName);
            if(await addressablesManager.TryLoadAddressable<InventoryItem>(path))
            {
                loadedPaths.Add(path);
                return await addressablesManager.LoadAddressable<InventoryItem>(path);
            }
            return null;
        }

        /// <summary>
        /// Unloads an item from addressables
        /// Returns true if successful
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool UnloadItem(string itemName)
        {
            string path = ConstructPath(itemName);
            if (addressablesManager.ReleaseAddressable(path))
            {
                loadedPaths.Remove(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Unloads all currently loaded items.
        /// Calls AddressablesManager and releases all those assets
        /// </summary>
        public void UnloadAllItems()
        {
            foreach(string path in loadedPaths)
            {
                addressablesManager.ReleaseAddressable(path);
            }

            loadedPaths.Clear();

            if(loadedPaths.Count != 0)
            {
                throw new System.Exception("InventoryItemLoader : Attempted to UnloadAllItems but some remained.");
            }
        }

        private string ConstructPath(string itemName)
        {
            return ITEM_PATH + itemName;
        }
    }
}
