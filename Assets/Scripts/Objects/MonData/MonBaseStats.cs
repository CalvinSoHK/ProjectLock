using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;

namespace Mon.MonData
{
    [System.Serializable]
    public class MonBaseStats
    {
        [SerializeField]
        int hp, def, spDef, atk, spAtk, speed;

        [SerializeField]
        int statTotal;

        public MonBaseStats(int _hp, int _def, int _sp_def, int _atk, int _sp_atk, int _speed)
        {
            if(_hp <= 0 || _def <= 0 || _sp_def <= 0 || _atk <= 0 || _sp_atk <= 0 || _speed <= 0)
            {
                Debug.LogError("MonGeneration Error: Invalid stat value. One of the stats is 0 or lower.");
            }

            hp = _hp;
            def = _def;
            spDef = _sp_def;
            atk = _atk;
            spAtk = _sp_atk;
            speed = _speed;

            statTotal = hp + def + spDef + atk + spAtk + speed;
        }

        public void PrintStats()
        {
            Debug.Log("HP : " + hp +
                "\n DEF : " + def +
                "\n SP. DEF: " + spDef +
                "\n ATK : " + atk +
                "\n SP. ATK : " + spAtk +
                "\n SPEED : " + speed);
        }
    
        public int GetStat(MonStatType requestedStat)
        {
            switch (requestedStat)
            {
                case MonStatType.HP:
                    return hp;
                case MonStatType.DEF:
                    return def;
                case MonStatType.SPDEF:
                    return spDef;
                case MonStatType.ATK:
                    return atk;
                case MonStatType.SPATK:
                    return spAtk;
                case MonStatType.SPEED:
                    return speed;
                default:
                    Debug.LogError("Requested invalid stat: " + requestedStat);
                    return hp;
            }
        }
    }
}
