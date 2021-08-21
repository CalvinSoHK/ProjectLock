using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Enums
{
    /// <summary>
    /// Enum mask that represents how an item can be used
    /// </summary>
    [Flags]
    public enum ItemMask
    {
        Unique = 0,
        Sellable = 1,
        Buyable = 2,
        UsableInCombat = 4,
        UsableInWorld = 8,
        UsableInWild = 16,
        ConsumedOnUse = 32
    }
}
