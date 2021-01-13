using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon
{
    /// <summary>
    /// Growth type, determines how much exp is needed to level.
    /// Normal: Normal curve. Less exp lower, more later.
    /// EarlyCurve: Early levels are harder to level, later is about normal.
    /// LateCurve: Early levels are normal to level, later levels are harder.
    /// </summary>
    public enum MonGrowthType
    {
        Normal = 0,
        EarlyCurve = 1,
        LateCurve = 2
    }
}
