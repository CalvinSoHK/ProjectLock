using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton base class.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton : MonoBehaviour
{
    private static Singleton _instance;

    public static Singleton Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }
}