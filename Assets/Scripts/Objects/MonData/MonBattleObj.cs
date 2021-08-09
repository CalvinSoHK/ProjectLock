using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonGeneration;

namespace Mon.MonData
{
    [System.Serializable]
    public class MonBattleObj
    {
        /// <summary>
        /// Battle data that can be passed to this Mon.
        /// Useful for passing on information needed for different conditionals.
        /// Is cleared at the end of battles.
        /// </summary>
        Dictionary<string, string> monBattleData;

        /// <summary>
        /// mon stats 
        /// </summary>
        public MonStats monStats;


        /// <summary>
        /// Constructor that takes in curStats and battle data to set it
        /// </summary>
        /// <param name="baseStats"></param>
        /// <param name="battleData"></param>
        public MonBattleObj(MonStats curStats, Dictionary<string, string> battleData)
        {
            monStats = curStats;
            monBattleData = battleData;
        }

        /// <summary>
        /// Appends to the monData dictionary
        /// If the key is already in the dictionary, it will fail.
        /// Returns true if it succeeds, false otherwise.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public bool AppendData(string key, string data)
        {
            string value;
            if (monBattleData.TryGetValue(key, out value))
            {
                return false;
            }
            monBattleData.Add(key, data);
            return true;
        }

        /// <summary>
        /// Appends to the monData dictionary
        /// If the key is already in the dictionary, it will overwrite the value.
        /// NOTE: Since it overwrites, this will never fail.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void ForceAppendData(string key, string data)
        {
            string value;
            if (monBattleData.TryGetValue(key, out value))
            {
                monBattleData.Remove(key);
            }
            monBattleData.Add(key, data);
        }

        /// <summary>
        /// Checks for the given key's data on the mon.
        /// If valid, returns a string, else the string will be null.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string CheckData(string key)
        {
            string value = null;
            monBattleData.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Resets the battle obj, removing conditions
        /// </summary>
        public void ResetBattleObj()
        {
            monBattleData.Clear();
        }
    }
}
