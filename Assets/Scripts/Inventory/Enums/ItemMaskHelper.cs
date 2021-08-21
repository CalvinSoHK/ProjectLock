using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Enums
{
    /// <summary>
    /// Helps check mask values
    /// </summary>
    public class ItemMaskHelper
    {
        /// <summary>
        /// Checks that the given mask contains value
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool MaskContains(ItemMask mask, ItemMask value)
        {
            if ((mask & value) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
