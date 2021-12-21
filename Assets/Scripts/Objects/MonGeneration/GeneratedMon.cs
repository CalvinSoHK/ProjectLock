﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Moves;
using System.IO;
using Mon.Enums;
using Mon.MonData;

namespace Mon.MonGeneration
{
    [System.Serializable]
    public class GeneratedMon
    {
        public int ID;
        public int generationID;
        public string name;
        public MonStage stage;

        //Points to next evolution, if null there is no next evolution
        public int next_evoID;

        //Points to prev evolution, if null there is no prev evolution
        public int prev_evoID;

        //Determines typing, for STAB and for resistance/weaknesses
        public MonType primaryType;
        public MonType secondaryType;

        //Determines what the levelling curve is like for the mon
        public MonGrowthType growthType;

        //Stats for this mon at max level (100).
        //Stats per level is calculated by multiplying by a float.
        //Example: If HP = 150 at level 100, then level 1 is 0.01 * 150 = 1.5. 
        //This is then rounded up to 2. Level 2 is then 150 * 0.02 = 3, so from levelling up you gain 1 HP. 
        //These are only the base stats, which are then further manipulated.
        //For example, if you have the minimum HP of 1, it does not mean you literally have 1 HP at level 100.
        public MonBaseStats baseStats;

        // How easy it is to catch this mon. 
        // Float represents chance to catch out of a 100 throws of a normal capture device.
        public float catchRate;

        // How heavy the mon is. Affects some moves.
        public float weight;

        // How much exp defeating this mon gives. Multipled with level.
        public float exp_gain;

        /// <summary>
        /// Move set dictionary for generated mon. 
        /// Associates level with move. 
        /// Only one move per level allowed.
        /// </summary>
        public List<LearnMoveData> learnableMoves = new List<LearnMoveData>();

        /// <summary>
        /// List of moves that can be taught, but not learned by levels.
        /// </summary>
        public List<MoveData> teachableMoves = new List<MoveData>();

        /// <summary>
        /// List of tags this generated mon will be using this generation
        /// </summary>
        public List<string> assignedTags = new List<string>();

        /// <summary>
        /// Returns a list of all learnable moves at or below given level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<LearnMoveData> GetLearnableMoves(int level)
        {
            List<LearnMoveData> learnMoves = new List<LearnMoveData>();
            foreach(LearnMoveData data in learnableMoves)
            {
                if(data.level <= level)
                {
                    learnMoves.Add(data);
                }
            }
            return learnMoves;
        }

        /// <summary>
        /// Saves the data as a JSON
        /// </summary>
        public void SaveData()
        {
            Utility.JsonUtility<GeneratedMon> jsonUtility = new Utility.JsonUtility<GeneratedMon>();
            string path = StaticPaths.SaveToGeneratedMonsPaths + "/" + generationID;
            if (!Directory.Exists(path))
            {
                Debug.Log("Path not available, will now generate path: " + path);
                Directory.CreateDirectory(path);
            }
            path += "/" + ID + name + ".txt";
#if DEBUG_ENABLED
            Debug.Log("Writing to path: " + path);
#endif
            jsonUtility.WriteJSON(this, path);
        }
    }
}
