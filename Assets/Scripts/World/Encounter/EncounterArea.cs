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

    public void AddEncounter(EncounterData data)
    {
        encounterData.Add(data);
    }

    public void EmptyEncounterList()
    {
        encounterData.Clear();
    }
}

[System.Serializable]
public class EncounterData
{
    public int dexID;
    public int level;
    public float chanceWeight;

    public EncounterData(int _id, int _level, float _chanceWeight)
    {
        dexID = _id;
        level = _level;
        chanceWeight = _chanceWeight;
    }
}
