using Mon.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonGeneration
{
    /// <summary>
    /// Defines profiles for types of mons that can be generated and the averages for that profile.
    /// </summary>
    [CreateAssetMenu(fileName = "MonGenFamilyProfileSO",
        menuName = "MonGeneration/MonGenFamilyProfile", order = 3)]
    public class MonGenFamilyProfileSO : ScriptableObject
    {
        /// <summary>
        /// Name of this profile
        /// </summary>
        public string familyProfileName;

        //List of profiles
        public List<MonGenProfileSO> profiles;

        //Minimum and max stats for this family of mon
        //Represents MAX value possible in our subsequent profiles
        public int statTotalAverage = 500;
        public int hpAverage = 100;
        public int defAverage = 80;
        public int spDefAverage = 80;
        public int atkAverage = 80;
        public int spAtkAverage = 80;
        public int speedAverage = 70;

        //Standard deviation of the distribution of stat totals.
        //Higher values equate to a more spread out number of stats.
        //NOTE: 1SD is will be the range of 66% of values. 2SD will be 95%. 3SD will be 99.5%.
        //For example: statTotalAverage = 500. If statTotal_stddev = 100, then:
        // 66% of values are between 400-600.
        // 95% of values are between 300-700.
        // 99.5% of values are between 200-800.
        //0.5% will be out of that range, so to be safe when we multiply we should clamp.
        public float statTotal_stddev = 100f;
        public float hpWeight_stddev = 20f;
        public float defWeight_stddev = 15f;
        public float spDefWeight_stddev = 15f;
        public float atkWeight_stddev = 15f;
        public float spAtkWeight_stddev = 15f;
        public float speedWeight_stddev = 12.5f;

        /// <summary>
        /// Grabs the MonGenProfile of the given stage in this collection
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public MonGenProfileSO GrabProfile(MonStage stage)
        {
            MonGenProfileSO chosen = null;
            foreach(MonGenProfileSO profile in profiles)
            {
                if(profile.monStage == stage)
                {
                    if(chosen == null)
                    {
                        chosen = profile;
                    }
                    else
                    {
                        Debug.LogError("Family profile: " + familyProfileName + " has multiple profiles for the stage: " + stage);
                    }                  
                }
            }
            if(chosen == null)
            {
                Debug.LogError("Family profile: " + familyProfileName + " did not have a profile for stage: " + stage);
            }

            return chosen;
        }
    }
}
