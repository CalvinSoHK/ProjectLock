﻿using Mon.MonGeneration;
using Mon.Moves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonData
{
    [System.Serializable]
    public class MonIndObj
    {
        /// <summary>
        /// Nickname of the individual
        /// </summary>
        private string nickname = "";

        /// <summary>
        /// Nickname of the individual
        /// </summary>
        public string Nickname
        {
            get
            {
                if (nickname.Equals(""))
                {
                    return baseMon.name;
                }
                else
                {
                    return nickname;
                }
            }
        }

        /// <summary>
        /// Base mon of this individual
        /// </summary>
        public GeneratedMon baseMon;

        /// <summary>
        /// Stats of this individual
        /// </summary>
        public MonStats stats;

        /// </summary>
        /// Current move set of the mon
        /// </summary>
        public MoveSet moveSet;

        /// <summary>
        /// Battle object of this individual.
        /// Contains a way to store statuses or special moves as strings with a string value.
        /// </summary>
        public MonBattleObj battleObj;

        /// <summary>
        /// Constructs an individual mon based off the GeneratedMon as base and a given level.
        /// </summary>
        /// <param name="mon"></param>
        /// <param name="level"></param>
        public MonIndObj(GeneratedMon mon, int level)
        {
            baseMon = mon;
            stats = new MonStats(baseMon.baseStats);
            stats.SetToLevel(level);
            battleObj = new MonBattleObj(stats, new Dictionary<string, string>());
        }

        /// <summary>
        /// Sets name of the individual
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            nickname = name;
        }

        /// <summary>
        /// Clears nickname
        /// </summary>
        public void ClearName()
        {
            nickname = "";
        }
    }
}
