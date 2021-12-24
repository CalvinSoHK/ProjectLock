using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

public class RandomManager : MonoBehaviour
{
    [SerializeField]
    private int baseSeed = 0;

    /// <summary>
    /// Base seed of current setting
    /// </summary>
    public int BaseSeed
    {
        get
        {
            return baseSeed;
        }
    }

    private string chosennumbers = "";

    private int idNumber = 0;

    /// <summary>
    /// Dict of random generators we are running
    /// </summary>
    private Dictionary<RandomType, RandomGenerator> generatorDict = new Dictionary<RandomType, RandomGenerator>();

    public void Initialize()
    {
        RandomGenerator baseGenerator = new RandomGenerator(baseSeed);

        foreach (int i in Enum.GetValues(typeof(RandomType)))
        {
            int seed = 0;
            RandomGenerator generator;
            if ((RandomType)i == (RandomType.Inconsistent))
            {
                seed = DateTime.Now.Millisecond;
                generator = new RandomGenerator(seed);
            }
            else
            {
                seed = baseGenerator.Range(int.MinValue, int.MaxValue);
                generator = new RandomGenerator(seed);
            }
#if DEBUG_ENABLED
            Debug.Log("RandomManager: " + (RandomType)i + " Seed: " + seed);
#endif
            generatorDict.Add((RandomType)i, generator);
        }
    }

    /// <summary>
    /// Generates a random number from generator of type
    /// Value from MinInclusive to MaxExclusive
    /// IDString is used for debugging
    /// </summary>
    /// <param name="type"></param>
    /// <param name="minInclusive"></param>
    /// <param name="maxExclusive"></param>
    /// <param name="IDString"></param>
    /// <returns></returns>
    public int Range(RandomType type, int minInclusive, int maxExclusive, string IDString)
    {
        RandomGenerator generator;

        if(generatorDict.TryGetValue(type, out generator))
        {
            int value = generator.Range(minInclusive, maxExclusive);
#if DEBUG_ENABLED
            chosennumbers += "\"" + idNumber + "/" + IDString + "\" :  \"" + value + "\",";
            idNumber++;
#endif
            return value;
        }
        else
        {
            throw new System.Exception("RandomType: " + type + " not initialized yet.");
        }
    }

    /// <summary>
    /// Generates a random number from generator of type
    /// Value from MinInclusive to MaxExclusive
    /// IDString is used for debugging
    /// </summary>
    /// <param name="type"></param>
    /// <param name="minInclusive"></param>
    /// <param name="maxExclusive"></param>
    /// <param name="IDString"></param>
    /// <returns></returns>
    public float Range(RandomType type, float minInclusive, float maxExclusive, string IDString)
    {
        RandomGenerator generator;

        if (generatorDict.TryGetValue(type, out generator))
        {
            float value = generator.Range(minInclusive, maxExclusive);
#if DEBUG_ENABLED
            chosennumbers += "\"" + idNumber + "/" + IDString + "\" :  \"" + value + "\",";
            idNumber++;
#endif
            return value;
        }
        else
        {
            throw new System.Exception("RandomType: " + type + " not initialized yet.");
        }
    }

    /// <summary>
    /// Gives next double from this generator
    /// </summary>
    /// <param name="type"></param>
    /// <param name="IDString"></param>
    /// <returns></returns>
    public double NextDouble(RandomType type, string IDString)
    {
        RandomGenerator generator;

        if (generatorDict.TryGetValue(type, out generator))
        {
            double value = generator.NextDouble();
#if DEBUG_ENABLED
            chosennumbers += "\"" + idNumber + "/" + IDString + "\" :  \"" + value + "\",";
            idNumber++;
#endif
            return value;
        }
        else
        {
            throw new System.Exception("RandomType: " + type + " not initialized yet.");
        }
    }

    private void Update()
    {
#if DEBUG_ENABLED
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("{" + chosennumbers + "}");
        }
#endif
    }
}

