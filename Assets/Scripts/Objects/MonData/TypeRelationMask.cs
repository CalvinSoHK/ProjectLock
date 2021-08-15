using System;

namespace Mon.Enums
{
    /// <summary>
    /// Enum that is used for effectiveness relations
    /// NOTE: Make sure this has all the typings in MonType
    /// </summary>
    [Flags]
    public enum TypeRelationMask
    {
        None = 0,
        Normal = 1,
        Fire = 2,
        Water = 3,
        Grass = 4,
        Electric = 5,
        Flying = 6,
        Ground = 7,
        Rock = 8,
        Bug = 9,
        Poison = 10,
        Fighting = 11,
        Psychic = 12,
        Dragon = 13,
        Ice = 14,
        Ghost = 15,
        Fairy = 16,
        Dark = 17,
        Steel = 18
    }
}
