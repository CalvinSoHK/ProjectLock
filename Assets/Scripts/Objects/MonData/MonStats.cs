using Mon.MonGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;

namespace Mon.MonData
{
    [System.Serializable]
    public class MonStats
    {
        /// <summary>
        /// Current stat level of each stat
        /// </summary>
        public int level, hp, def, spDef, atk, spAtk, speed;

        /// <summary>
        /// The theoretical max of each stat for this mon
        /// </summary>
        private int maxHp, maxDef, maxSpDef, maxAtk, maxSpAtk, maxSpeed;

        public MonStats(MonBaseStats baseStats)
        {
            CalculateMax(baseStats);
        }

        /// <summary>
        /// Levels the mon up by one
        /// Recalculates stats based on levels.
        /// Prohibits levelling past 100
        /// </summary>
        public void LevelUp()
        {
            if(level < 100)
            {
                SetToLevel(level + 1);
            }
            else
            {
                throw new System.Exception("Mon Stats Error: Calling level up when Mon is already level 100.");
            }
        }

        /// <summary>
        /// Sets mon to given level
        /// </summary>
        /// <param name="level"></param>
        public void SetToLevel(int _level)
        {
            level = _level;
            CalculateStats();
        }

        /// <summary>
        /// Calculates stats based on level
        /// </summary>
        private void CalculateStats()
        {
            hp = CalculateHP();
            def = CalculateDEF();
            spDef = CalculateSPDEF();
            atk = CalculateATK();
            spAtk = CalculateSPATK();
            speed = CalculateSPEED();
        }

        /// <summary>
        /// Calculates the current HP of this mon based off of our maximum stat and level.
        /// </summary>
        /// <param name="maxStat"></param>
        /// <returns></returns>
        private int CalculateHP()
        {
            return Mathf.RoundToInt(level / 100f * maxHp) + StaticBaseStats.baseHP;           
        }

        /// <summary>
        /// Calculates the current DEF of this mon based off our maximum stat and level.
        /// </summary>
        /// <param name="maxStat"></param>
        /// <returns></returns>
        private int CalculateDEF()
        {
            return Mathf.RoundToInt(level / 100f * maxDef) + StaticBaseStats.baseDEF;
        }

        /// <summary>
        /// Calculates the current SPDEF of this mon based off our maximum stat and level.
        /// </summary>
        /// <param name="maxStat"></param>
        /// <returns></returns>
        private int CalculateSPDEF()
        {
            return Mathf.RoundToInt(level / 100f * maxSpDef) + StaticBaseStats.baseSPDEF;
        }

        /// <summary>
        /// Calculates the current ATK of this mon based off our maximum stat and level.
        /// </summary>
        /// <param name="maxStat"></param>
        /// <returns></returns>
        private int CalculateATK()
        {
            return Mathf.RoundToInt(level / 100f * maxAtk) + StaticBaseStats.baseATK;
        }

        /// <summary>
        /// Calculates the current SPATK of this mon based off our maximum stat and level.
        /// </summary>
        /// <param name="maxStat"></param>
        /// <returns></returns>
        private int CalculateSPATK()
        {
            return Mathf.RoundToInt(level / 100f * maxSpAtk) + StaticBaseStats.baseSPATK;
        }

        /// <summary>
        /// Calculates the current SPEED of this mon based off our maximum stat and level.
        /// </summary>
        /// <param name="maxStat"></param>
        /// <returns></returns>
        private int CalculateSPEED()
        {
            return Mathf.RoundToInt(level / 100f * maxSpeed) + StaticBaseStats.baseSPEED;
        }

        /// <summary>
        /// Calculates all the max stat values of this mon.
        /// </summary>
        private void CalculateMax(MonBaseStats baseStats)
        {
            maxHp = CalculateMaxHP(baseStats.GetStat(MonStatType.HP));
            maxDef = CalculateMaxDEF(baseStats.GetStat(MonStatType.DEF));
            maxSpDef = CalculateMaxSPDEF(baseStats.GetStat(MonStatType.SPDEF));
            maxAtk = CalculateMaxATK(baseStats.GetStat(MonStatType.ATK));
            maxSpAtk = CalculateMaxSPATK(baseStats.GetStat(MonStatType.SPATK));
            maxSpeed = CalculateMaxSPEED(baseStats.GetStat(MonStatType.SPEED));
        }

        /// <summary>
        /// Calculates the MaxHP of this mon.
        /// </summary>
        /// <param name="baseMax"></param>
        /// <returns></returns>
        private int CalculateMaxHP(int baseMax)
        {
            return baseMax;
        }

        /// <summary>
        /// Calculates the MaxDEF of this mon.
        /// </summary>
        /// <param name="baseMax"></param>
        /// <returns></returns>
        private int CalculateMaxDEF(int baseMax)
        {
            return baseMax;
        }

        /// <summary>
        /// Calculates the MaxSPDEF of this mon.
        /// </summary>
        /// <param name="baseMax"></param>
        /// <returns></returns>
        private int CalculateMaxSPDEF(int baseMax)
        {
            return baseMax;
        }


        /// <summary>
        /// Calculates the MaxATK of this mon.
        /// </summary>
        /// <param name="baseMax"></param>
        /// <returns></returns>
        private int CalculateMaxATK(int baseMax)
        {
            return baseMax;
        }

        /// <summary>
        /// Calculates the MaxSPATK of this mon.
        /// </summary>
        /// <param name="baseMax"></param>
        /// <returns></returns>
        private int CalculateMaxSPATK(int baseMax)
        {
            return baseMax;
        }

        /// <summary>
        /// Calculates the MaxSPEED of this mon.
        /// </summary>
        /// <param name="baseMax"></param>
        /// <returns></returns>
        private int CalculateMaxSPEED(int baseMax)
        {
            return baseMax;
        }
    }
}