using Core;
using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

[RequireComponent(typeof(EncounterArea))]
public class EncounterRandomizer : MonoBehaviour
{
    EncounterArea encounterArea;
    MonDex dex;

    [SerializeField]
    int numEncounters = 0;

    [SerializeField]
    [Range(1, 100)]
    int minLevel = 1;

    [SerializeField]
    [Range(1, 100)]
    int maxLevel = 100;

    private void Awake()
    {
        encounterArea = GetComponent<EncounterArea>();

        List<EncounterData> encounterData = new List<EncounterData>();

        //If we can get data then use those
        if(CoreManager.Instance.encounterManager.encounterData.TryGetEncounters(encounterArea.ID, out encounterData))
        {
            foreach(EncounterData data in encounterData)
            {
                Debug.Log(data.dexID + " / " + data.level + " / " + data.chanceWeight);
                encounterArea.AddEncounter(data);
            }
        }
        else //Else generate new ones
        {
            GenerateRandomEncounters();
        }
    }

    /// <summary>
    /// Generates random encounters
    /// </summary>
    private void GenerateRandomEncounters()
    {
        encounterArea.EmptyEncounterList();

        dex = CoreManager.Instance.dexManager.Dex;

        //Clamps numEncounters to dex length
        numEncounters = Mathf.Clamp(numEncounters, 0, dex.dexLength);

        for (int i = 0; i < numEncounters; i++)
        {

            int randID = CoreManager.Instance.randomManager.Range(RandomType.Generation, 0, dex.dexLength, "EncounterRandomizer1");
            if (dex.CheckValidID(randID))
            {
                EncounterData data = new EncounterData(
                    randID,
                    CoreManager.Instance.randomManager.Range(RandomType.Generation, minLevel, maxLevel, "EncounterRandomizer2"),
                    CoreManager.Instance.randomManager.Range(RandomType.Generation, 0f, 1f, "EncounterRandomizer3"));
                encounterArea.AddEncounter(data);
                CoreManager.Instance.encounterManager.encounterData.AddEncounter(encounterArea.ID, data);
            }
        }
    }
}
