using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Tile
{
    public class EncounterTile : MonoBehaviour
    {
        [SerializeField]
        private EncounterArea encounterArea;

        /// <summary>
        /// The encountere area data for this tile.
        /// </summary>
        public EncounterArea EncounterArea
        {
            get
            {
                return encounterArea;
            }
        }
    }
}
