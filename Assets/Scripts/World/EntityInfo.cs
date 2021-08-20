using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    /// <summary>
    /// Basic info for an entity on the overworld
    /// </summary>
    public class EntityInfo : MonoBehaviour
    {
        [SerializeField]
        public string entityName = "INVALID";

        [SerializeField]
        private EntityType type = EntityType.NonCombatant;

        [HideInInspector]
        public EntityType Type { get { return type; } }
    }

    /// <summary>
    /// Different entity types. Can be used to determine how to react to starting a battle.
    /// </summary>
    public enum EntityType
    {
        NonCombatant,
        Trainer,
        Gym,
        Player
    }
}
