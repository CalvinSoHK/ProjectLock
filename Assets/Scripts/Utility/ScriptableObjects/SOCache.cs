using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// SOCache is a class that helps us manage retrieving scriptable objects that we need
/// </summary>
public static class SOCache
{
    public static T GetScriptableObject<T>(string path) where T:ScriptableObject
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        return request.asset as T;
    }
}

/// <summary>
/// SO contains all the paths we need to use SOCache
/// </summary>
public static class SO
{
    public const string MonGeneratorSettingsSO = "ScriptableObjects/MonGeneratorSettingsSO";

}