using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;

namespace World.Trigger
{
    [RequireComponent(typeof(EntityInfo))]
    [RequireComponent(typeof(PartyManager))]
    public class BattleTrigger : MonoBehaviour
    {
        public void TriggerBattle()
        {
            EncounterType type;
            EntityInfo entity = GetComponent<EntityInfo>();
            Debug.Log("Triggering battle");
            switch (entity.Type)
            {
                case EntityType.Trainer:
                    type = EncounterType.Trainer;
                    break;
                case EntityType.Gym:
                    type = EncounterType.Gym;
                    break;
                default:
                    return;
            }

            Core.CoreManager.Instance.encounterManager.FireEncounter(new Encounter(type, GetComponent<PartyManager>().party));
        }
    }
}
