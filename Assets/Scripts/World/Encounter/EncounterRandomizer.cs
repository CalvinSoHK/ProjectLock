using Core;
using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        encounterArea.EmptyEncounterList();

        dex = CoreManager.Instance.dexManager.Dex;

        //Clamps numEncounters to dex length
        numEncounters = Mathf.Clamp(numEncounters, 0, dex.dexLength);

        for(int i = 0; i < numEncounters; i++)
        {
            
            int randID = CoreManager.Instance.randomManager.Range(0, dex.dexLength, "EncounterRandomizer1");
            if (dex.CheckValidID(randID))
            {
                EncounterData data = new EncounterData(
                    randID,
                    CoreManager.Instance.randomManager.Range(minLevel, maxLevel, "EncounterRandomizer2"),
                    CoreManager.Instance.randomManager.Range(0f, 1f, "EncounterRandomizer3"));
                encounterArea.AddEncounter(data);
                Debug.Log("Encounter added: " + data.dexID);
            }
        }

        //Destroy(this);
    }
}
