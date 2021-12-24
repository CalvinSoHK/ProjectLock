using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class EncounterArea : MonoBehaviour
{
    [SerializeField]
    private string id = "";

    public string ID
    {
        get
        {
            return id;
        }
    }

    [SerializeField]
    List<EncounterData> encounterData = new List<EncounterData>();

    public EncounterData PickEncounter()
    {
        Deck<EncounterData> encounterDeck = new Deck<EncounterData>(Utility.Random.RandomType.Inconsistent, "EncounterDeck", encounterData);
        encounterDeck.ShuffleDeck();
        return encounterDeck.DestructiveDraw();
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
