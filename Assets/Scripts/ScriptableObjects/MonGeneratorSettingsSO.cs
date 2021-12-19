using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonGeneration
{
    /// <summary>
    /// Class that holds constants that we can switch around on the scriptable object
    /// </summary>
    [CreateAssetMenu(fileName = "MonGeneratorSettingsSO",
        menuName = "MonGeneration/MonGeneratorSettings", order = 1)]
    public class MonGeneratorSettingsSO : ScriptableObject
    {
        /// <summary>
        /// Chance it will be monoTyped. Should be moved to a scriptable object.
        /// </summary>
        public float monoTypingChance = 0.25f;

        /// <summary>
        /// List of possible mon generation profiles.
        /// </summary>
        public List<MonGenFamilyProfileSO> familyProfiles = new List<MonGenFamilyProfileSO>();

        /// <summary>
        /// Grab a random profile that fulfill the given criteria.
        /// </summary>
        /// <param name="familySize"></param>
        /// <returns></returns>
        public MonGenFamilyProfileSO PickRandomFamilyProfile(int familySize)
        {
            List<MonGenFamilyProfileSO> pickedProfiles = new List<MonGenFamilyProfileSO>();
            foreach(MonGenFamilyProfileSO profile in familyProfiles)
            {
                if(profile.profiles.Count == familySize)
                {
                    pickedProfiles.Add(profile);
                }
            }

            return pickedProfiles[CoreManager.Instance.randomManager.Range(0, pickedProfiles.Count - 1, "FamilyProfile1")];
        }
    }
}
