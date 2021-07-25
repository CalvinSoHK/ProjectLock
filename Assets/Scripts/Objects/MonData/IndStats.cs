using Mon.MonGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonData
{
    /// <summary>
    /// Individual Mon's stats.
    /// Includes genetics and current status
    /// </summary>
    public class IndStats
    {
        /// <summary>
        /// Current name of the individual
        /// </summary>
        public string name = null;

        /// <summary>
        /// Current health of the individual.
        /// </summary>
        public int health = 0;

        /// <summary>
        /// Current level of the individual.
        /// </summary>
        public int level = 0;

        /// <summary>
        /// Current exp of the individual.
        /// </summary>
        public int exp = 0;


        public void InitStats(GeneratedMon mon, int level)
        {

        }
    }
}
