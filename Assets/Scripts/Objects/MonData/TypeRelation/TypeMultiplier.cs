using Mon.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonData
{
    /// <summary>
    /// Type and multiplier in one struct
    /// </summary>
    public struct TypeMultiplier : IComparable
    {
        public MonType type;
        public float multiplier;

        public TypeMultiplier(MonType _type, float _multiplier)
        {
            type = _type;
            multiplier = _multiplier;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            TypeMultiplier _typeMultiplier = (TypeMultiplier)obj;
            return multiplier.CompareTo(_typeMultiplier.multiplier);
        }
    }
}