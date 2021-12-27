using Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private InventoryData inventory = new InventoryData();

        /// <summary>
        /// Reference to the inventory of this entity
        /// </summary>     
        public InventoryData Inventory
        {
            get
            {
                return inventory;
            }
        }

        /// <summary>
        /// If set to true, will call preload on inventory
        /// </summary>
        [SerializeField]
        private bool preload = false;

        private void Start()
        {
            if (preload)
            {
                Inventory.Preload();
            }
        }
    }
}
