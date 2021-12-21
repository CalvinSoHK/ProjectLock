using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

[System.Serializable]
public class WorldEncounterData
{
    //Key is a string ID (i.e. NorthwestPatch), value is list of EncounterData
    private Dictionary<string, List<EncounterData>> encounterDict = new Dictionary<string, List<EncounterData>>();

    //List for saving encounters as json
    private WorldEncounterJSON worldEncounterJSON = new WorldEncounterJSON();

    /// <summary>
    /// Adds encounterData with given ID to the encounterDict
    /// </summary>
    /// <param name="id"></param>
    /// <param name="encounter"></param>
    public void AddEncounter(string id, EncounterData encounter)
    {
        //If we don't already have it in the dict, add it to the dict
        if (!encounterDict.ContainsKey(id))
        {
            List<EncounterData> encounterData = new List<EncounterData>();
            encounterData.Add(encounter);
            encounterDict.Add(id, encounterData);

            //Since it is not in dict we can also add it to the list
            worldEncounterJSON.encounterList.Add(new EncounterJSON(id, encounterData));
        }
        else//We already have it in the dict, add our new encounter to that list
        {
            List<EncounterData> encounterData;
            if(encounterDict.TryGetValue(id, out encounterData))
            {
                encounterData.Add(encounter);

                //Since it is in dict we need to pull the list and add to it
                foreach(EncounterJSON data in worldEncounterJSON.encounterList)
                {
                    if (data.id.Equals(id))
                    {
                        data.encounterData.Add(encounter);
                        break;
                    }
                }
            }
            else
            {
                throw new System.Exception("Error: Trying to get list that does not exist.");
            }
        }
        SaveData();
    }


    /// <summary>
    /// Tries to get encounter list given string id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dataList"></param>
    /// <returns></returns>
    public bool TryGetEncounters(string id, out List<EncounterData> dataList)
    {
        return encounterDict.TryGetValue(id, out dataList);
    }

    public void SaveData()
    {
        worldEncounterJSON.SaveData();
    }

    public async Task LoadData()
    {
        JsonUtility<WorldEncounterJSON> jsonUtility = new JsonUtility<WorldEncounterJSON>();
        string loadPath = StaticPaths.LoadFromGeneratedEncountersPaths + "/" + Core.CoreManager.Instance.randomManager.Seed;
        WorldEncounterJSON jsonData = await jsonUtility.LoadJSON(loadPath, JsonUtility<WorldEncounterJSON>.LoadType.Resources);

        //Go through each entry
        foreach(EncounterJSON jsonEntry in jsonData.encounterList)
        {
            foreach(EncounterData encounter in jsonEntry.encounterData)
            {
                AddEncounter(jsonEntry.id, encounter);
            }
        }
    }
}
