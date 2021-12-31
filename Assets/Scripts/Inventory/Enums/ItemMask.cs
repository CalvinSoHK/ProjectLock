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
        None = 0,
        Sellable = 1,
        Buyable = 2,
        Unique = 4,
        UsableInCombat = 8,
        UsableInWorld = 16,
        UsableInWild = 32,
        ConsumedOnUse = 64
    }
}
