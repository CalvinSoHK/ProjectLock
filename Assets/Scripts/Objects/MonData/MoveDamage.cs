using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.Moves
{
    /// <summary>
    /// Class that holds the power the move should do
    /// And the move in question
    /// </summary>
    public class MoveDamage : IComparable
    {
        /// <summary>
        /// The full move data
        /// </summary>
        public MoveData moveData;

        /// <summary>
        /// The theoretical power of the mon
        /// </summary>
        public int power;

        /// <summary>
        /// The multiplier from typing
        /// </summary>
        public float typeMultiplier;

        /// <summary>
        /// Index of the move on the mon
        /// </summary>
        public int index;

        public MoveDamage(MoveData _moveData, int _power, int _index, float _typeMultiplier)
        {
            moveData = _moveData;
            power = _power;
            index = _index;
            typeMultiplier = _typeMultiplier;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            MoveDamage _moveDamage = (MoveDamage)obj;
            return power.CompareTo(_moveDamage.power);
        }
    }
}