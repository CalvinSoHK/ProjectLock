using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    [SerializeField]
    private int seed = 0;

    public int Seed
    {
        get
        {
            return seed;
        }
    }

    [SerializeField]
    private RandomState state;

    public RandomState State
    {
        get
        {
            return state;
        }
    }

    [SerializeField]
    private bool debug = false;

    private string chosennumbers = "";

    private int idNumber = 0;

    public void InitializeSeed()
    {
        if(state == RandomState.NotInitialized)
        {
            Random.InitState(seed);
            state = RandomState.Initialized;
        }
        else
        {
            Debug.LogWarning("Random state already initialized.");
        }
        
    }

    public int Range(int minInclusive, int maxInclusive, string IDString)
    {
        if(state == RandomState.Initialized)
        {
            int value = Random.Range(minInclusive, maxInclusive);
            if (debug)
            {
                chosennumbers += "\"" + idNumber + "/" + IDString + "\" :  \"" + value + "\",";
                idNumber++;
            }
            return value;
        }
        else
        {
            throw new System.Exception("Random seed not initialized yet");
        }
        
    }

    public float Range(float minInclusive, float maxInclusive, string IDString)
    {
        if (state == RandomState.Initialized)
        {
            float value = Random.Range(minInclusive, maxInclusive);
            if (debug)
            {
                chosennumbers += "\"" + idNumber + "/" + IDString + "\" :  \"" + value + "\",";
                idNumber++;
            }
            return value;
        }
        else
        {
            throw new System.Exception("Random seed not initialized yet");
        }
    }

    private void Update()
    {
        if(debug && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("{" + chosennumbers + "}");
        }
    }
}

[System.Serializable]
public enum RandomState
{
    NotInitialized = 0,
    Initialized = 1
}
