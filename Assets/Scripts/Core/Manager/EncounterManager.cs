using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Tile;

namespace Core.Player
{
    public class EncounterManager : MonoBehaviour
    {
        [Header("Encounter Detection")]
        [SerializeField]
        LayerMask encounterLayers;
        float checkDistance = 2f;
        RaycastHit hitInfo = new RaycastHit();
        EncounterArea encounterArea = null;
        EncounterData curEncounter = null;

        //Encountered mon
        [SerializeField]
        private MonIndObj encounteredMon = null;

        [Header("Encounter Chance")]
        float curChance = 0;
        [SerializeField]
        float encounterChance = 0.25f;
        [SerializeField]
        float chanceGain = 0.05f;

        private void OnEnable()
        {
            PlayerController.OnPlayerMove += CheckEncounter;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerMove -= CheckEncounter;
        }

        private void CheckEncounter()
        {
            if (CheckEncounterableState())
            {
                if (curChance == 0)
                {
                    curChance = encounterChance;
                }

                if (Random.Range(0f, 1f) <= curChance)
                {
                    curEncounter = encounterArea.PickEncounter();
                    FireEncounter();
                }
                else
                {
                    curChance += chanceGain;
                }
            }
            else
            {
                encounterArea = null;
                curEncounter = null;
            }
        }

        private bool CheckEncounterableState()
        {
            if (Physics.Raycast(CoreManager.Instance.player.transform.position, Vector3.down, out hitInfo, checkDistance, encounterLayers))
            {
                EncounterTile tile = hitInfo.collider.transform.GetComponent<EncounterTile>();
                if (tile)
                {
                    encounterArea = tile.EncounterArea;
                }
                else
                {
                    Debug.LogError("No encounter tile on object that should have one: " + hitInfo.collider.gameObject);
                }
                return true;
            }
            return false;
        }

        private void FireEncounter()
        {
            curChance = 0f;
            Debug.Log("CurEncounterData: " + curEncounter.dexID);
            MonIndObj indObj = new MonIndObj(CoreManager.Instance.dexManager.GetMonByID(curEncounter.dexID), curEncounter.level);
            encounteredMon = indObj;
            Debug.Log("Firing encounter against: " + indObj.Nickname);
        }
    }
}