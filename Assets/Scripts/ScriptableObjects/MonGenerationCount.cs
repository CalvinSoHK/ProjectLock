using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonGeneration
{
    [CreateAssetMenu(fileName = "MonGenerationCount",
            menuName = "MonGeneration/MonGenerationCount", order = 2)]
    public class MonGenerationCount : ScriptableObject
    {
        [SerializeField]
        private int count = 0;

        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Checks if a given int ID is valid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckValidGenID(int id)
        {
            if (id < count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns new ID
        /// </summary>
        /// <returns></returns>
        public int GetNewID()
        {
            count++;
            return count - 1;
        }
    }
}