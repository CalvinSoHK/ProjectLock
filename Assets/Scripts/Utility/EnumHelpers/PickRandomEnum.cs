using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Helps to pick random enum from enum type
    /// </summary>
    public class PickRandomEnum<T> where T : struct, IConvertible
    {
        /// <summary>
        /// Picks a random enum value of type T
        /// </summary>
        /// <returns></returns>
        public T PickRandom()
        {
            T[] enums = (T[])Enum.GetValues(typeof(T));
            return enums[CoreManager.Instance.randomManager.Range(0, enums.Length, "PickRandomEnumL21")];
        }
    }
}
