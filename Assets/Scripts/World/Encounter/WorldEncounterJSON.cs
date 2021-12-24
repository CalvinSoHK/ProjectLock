using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

[System.Serializable]
public class WorldEncounterJSON
{
    public List<EncounterJSON> encounterList = new List<EncounterJSON>();

    /// <summary>
    /// Saves the data as a JSON
    /// </summary>
    public void SaveData()
    {
        JsonUtility<WorldEncounterJSON> jsonUtility = new JsonUtility<WorldEncounterJSON>();
        string path = StaticPaths.SaveToGeneratedEncountersPaths;
        if (!Directory.Exists(path))
        {
            Debug.Log("Path not available, will now generate path: " + path);
            Directory.CreateDirectory(path);
        }
        path += "/" + Core.CoreManager.Instance.randomManager.BaseSeed + ".txt";
#if DEBUG_ENABLED
        Debug.Log("Writing to path: " + path);
#endif
        jsonUtility.WriteJSON(this, path);
    }
}

[System.Serializable]
public class EncounterJSON
{
    public string id;
    public List<EncounterData> encounterData;

    public EncounterJSON(string _id, List<EncounterData> _encounterData)
    {
        id = _id;
        encounterData = new List<EncounterData>();
        foreach(EncounterData data in _encounterData)
        {
            encounterData.Add(data);
        }
    }
}
