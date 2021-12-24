using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Random
{
    /// <summary>
    /// Class that generates random numbers
    /// </summary>
    public class RandomGenerator
    {
        System.Random random;

        public RandomGenerator(int seed)
        {
            random = new System.Random(seed);
        }

        /// <summary>
        /// Return random number min inclusive max exclusive
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Range(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// Return random number float min inclusive max exclusive
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public float Range(float min, float max)
        {
            double randValue = random.NextDouble();
            double range = (double)max - (double)min;
            return (float)((randValue * range) + min);
        }
    }
}