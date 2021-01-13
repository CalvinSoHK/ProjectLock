using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mon.MonGeneration
{
    /// <summary>
    /// Reads in a data json file on a base ungenerated Mon.
    /// </summary>
    public class DataReader
    {
        public BaseMon ParseData(string fileName)
        {
            string filePath = "MonData/" + fileName.Replace(".json", "");

            Utility.JsonReader<BaseMonJSON> jsonReader = new Utility.JsonReader<BaseMonJSON>();

            BaseMonJSON baseMonJSON = jsonReader.LoadJSON(filePath);

            BaseMon baseMon = new BaseMon(
                baseMonJSON.name,
                baseMonJSON.key,
                (MonStage)baseMonJSON.stage,
                baseMonJSON.family,
                ConvertToType(baseMonJSON.requiredType),
                baseMonJSON.tags
                );

            return baseMon;
        }

        /// <summary>
        /// Converts a written typing to the enum
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private MonType ConvertToType(string type)
        {
            string searchString = System.Text.RegularExpressions.Regex.Replace(type, @"[^a-zA-Z0-9_\\]", "").ToUpperInvariant();

            switch (searchString)
            {
                case "NORMAL":
                    return MonType.Normal;
                case "FIRE":
                    return MonType.Fire;
                case "WATER":
                    return MonType.Water;
                case "GRASS":
                    return MonType.Grass;
                case "ELECTRIC":
                    return MonType.Electric;
                case "FLYING":
                    return MonType.Flying;
                case "GROUND":
                    return MonType.Ground;
                case "ROCK":
                    return MonType.Rock;
                case "BUG":
                    return MonType.Bug;
                case "POISON":
                    return MonType.Poison;
                case "FIGHTING":
                    return MonType.Fighting;
                case "PSYCHIC":
                    return MonType.Psychic;
                case "DRAGON":
                    return MonType.Dragon;
                case "ICE":
                    return MonType.Ice;
                case "GHOST":
                    return MonType.Ghost;
                case "FAIRY":
                    return MonType.Fairy;
                case "DARK":
                    return MonType.Dark;
                case "STEEL":
                    return MonType.Steel;
                default:
                    Debug.LogError("Invalid type was given: " + searchString);
                    return MonType.None;
            }
        }
    }
}
