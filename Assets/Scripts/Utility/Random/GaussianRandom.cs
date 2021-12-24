using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

namespace Utility.Random
{
    /// <summary>
    /// Generates random number in a normal/gaussian curve.
    /// Help from: https://stackoverflow.com/questions/218060/random-gaussian-variables
    /// </summary>
    public class GaussianRandom
    {
        private RandomType type;
        private string IDString;

        public GaussianRandom(RandomType _type, string _IDString)
        {
            //Has to be generated or else we will get the same random in the same frame.
            type = _type;
            IDString = _IDString;
        }

        public double RandomGaussian(float average, float stdDev)
        {
            double u1 = Core.CoreManager.Instance.randomManager.NextDouble(RandomType.Generation, IDString);
            double u2 = Core.CoreManager.Instance.randomManager.NextDouble(RandomType.Generation, IDString);
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         average + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
    }
}
