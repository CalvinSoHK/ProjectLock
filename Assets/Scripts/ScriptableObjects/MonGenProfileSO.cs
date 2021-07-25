using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;

namespace Mon.MonGeneration
{
    /// <summary>
    /// Defines profiles for types of mons that can be generated and the averages for that profile.
    /// </summary>
    [CreateAssetMenu(fileName = "MonGenProfileSO",
        menuName = "MonGeneration/MonGenProfile", order = 2)]
    public class MonGenProfileSO : ScriptableObject
    {
        /// <summary>
        /// Stage this profile should be used for
        /// </summary>
        public MonStage monStage;

        //Percentage of stats this mon should have based on this profile.
        //Value between 0 and 1. 1 means max value possible in this family.    
        [Range(0f, 1f)]
        public float statMinPotential = 1;
        [Range(0f, 1f)]
        public float statMaxPotential = 1;

        [Range(0f, 1f)]
        public float hpMinPotential = 1f;
        [Range(0f, 1f)]
        public float hpMaxPotential = 1f;

        [Range(0f, 1f)]
        public float defMinPotential = 1f;
        [Range(0f, 1f)]
        public float defMaxPotential = 1f;

        [Range(0f, 1f)]
        public float spDefMinPotential = 1f;
        [Range(0f, 1f)]
        public float spDefMaxPotential = 1f;

        [Range(0f, 1f)]
        public float atkMinPotential = 1f;
        [Range(0f, 1f)]
        public float atkMaxPotential = 1f;

        [Range(0f, 1f)]
        public float spAtkMinPotential = 1f;
        [Range(0f, 1f)]
        public float spAtkMaxPotential = 1f;

        [Range(0f, 1f)]
        public float speedMinPotential = 1f;
        [Range(0f, 1f)]
        public float speedMaxPotential = 1f;

        public float GetRandomStatPotential(MonStatType requestedStat)
        {
            float min = 0, max = 1;
            switch (requestedStat)
            {
                case MonStatType.HP:
                    min = hpMinPotential;
                    max = hpMaxPotential;
                    break;
                case MonStatType.DEF:
                    min = defMinPotential;
                    max = defMaxPotential;
                    break;
                case MonStatType.SPDEF:
                    min = spDefMinPotential;
                    max = spDefMaxPotential;
                    break;
                case MonStatType.ATK:
                    min = atkMinPotential;
                    max = atkMaxPotential;
                    break;
                case MonStatType.SPATK:
                    min = spAtkMinPotential;
                    max = spAtkMaxPotential;
                    break;
                case MonStatType.SPEED:
                    min = speedMinPotential;
                    min = speedMaxPotential;
                    break;
                default:
                    Debug.LogError("Requested stat potentials for invalid stat: " + requestedStat);
                    break;
            }
            return Random.Range(min, max);
        }
    }
}
