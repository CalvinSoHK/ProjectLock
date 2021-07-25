using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Generates random number in a normal/gaussian curve.
    /// Help from: https://stackoverflow.com/questions/218060/random-gaussian-variables
    /// </summary>
    public class GaussianRandom
    {
        System.Random rand;

        private static GaussianRandom instance = null;
        public static GaussianRandom Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GaussianRandom();
                }
                return instance;
            }
        }

        public GaussianRandom()
        {
            //Has to be generated or else we will get the same random in the same frame.
            rand = new System.Random();
        }

        public double RandomGaussian(float average, float stdDev)
        {
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         average + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
    }
}
