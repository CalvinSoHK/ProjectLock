using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterArea : MonoBehaviour
{
    [SerializeField]
    List<EncounterData> encounterData = new List<EncounterData>();

    public EncounterData PickEncounter()
    {
        return encounterData[0];
    }
}

[System.Serializable]
public class EncounterData
{
    public int dexID;
    public int level;
    public float chanceWeight;
}
