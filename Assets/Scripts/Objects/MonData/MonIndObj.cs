using Core;
using Mon.MonGeneration;
using Mon.Moves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Random;

namespace Mon.MonData
{
    [System.Serializable]
    public class MonIndObj
    {
        /// <summary>
        /// Nickname of the individual
        /// </summary>
        private string nickname = "";

        /// <summary>
        /// Nickname of the individual
        /// </summary>
        public string Nickname
        {
            get
            {
                if (nickname.Equals(""))
                {
                    return baseMon.name;
                }
                else
                {
                    return nickname;
                }
            }
        }

        /// <summary>
        /// Base mon of this individual
        /// </summary>
        public GeneratedMon baseMon;

        /// <summary>
        /// Stats of this individual
        /// </summary>
        public MonStats stats;

        /// </summary>
        /// Current move set of the mon
        /// </summary>
        public MoveSet moveSet;

        /// <summary>
        /// Battle object of this individual.
        /// Contains a way to store statuses or special moves as strings with a string value.
        /// </summary>
        public MonBattleObj battleObj;

        /// <summary>
        /// Constructs an individual mon based off the GeneratedMon as base and a given level.
        /// </summary>
        /// <param name="mon"></param>
        /// <param name="level"></param>
        public MonIndObj(GeneratedMon mon, int level)
        {
            baseMon = mon;
            stats = new MonStats(baseMon.baseStats);
            stats.SetToLevel(level);
            moveSet = new MoveSet();
            SetMoveset(level);
            battleObj = new MonBattleObj(new MonStats(baseMon.baseStats), new Dictionary<string, string>());
            battleObj.monStats.MatchStatsTo(stats);
        }

        /// <summary>
        /// Sets random moveset for the mon based on level
        /// </summary>
        private void SetMoveset(int level)
        {
            Deck<LearnMoveData> possibleMoves = new Deck<LearnMoveData>(RandomType.Generation, "PickLearnMoveDeck" + baseMon.ID, baseMon.GetLearnableMoves(level));
            possibleMoves.ShuffleDeck();
            while(moveSet.MoveCount < 5)
            {
                //If there are no more possible moves just break out of the while loop
                if(possibleMoves.Count == 0)
                {
                    break;
                }
                else //Keep drawing from the possible moves list and keep overwriting till done
                {
                    //Draw a possible move
                    LearnMoveData learnMove = possibleMoves.DestructiveDraw();

                    //Grabs empty index. IF there is one just assign a move to it
                    if (moveSet.GetEmptyIndex() != -1)
                    {
                        moveSet.LearnMove(learnMove.move, moveSet.GetEmptyIndex());
                    }
                    else //If there is no empty index
                    {
                        //Pick a random index
                        int index = CoreManager.Instance.randomManager.Range(RandomType.Generation, 0, MoveSet.MoveMaxCount, "MonIndObj1");
                        moveSet.LearnMove(learnMove.move, index);
                    }
                }
            }
        }

        /// <summary>
        /// Sets name of the individual
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            nickname = name;
        }

        /// <summary>
        /// Clears nickname
        /// </summary>
        public void ClearName()
        {
            nickname = "";
        }

        /// <summary>
        /// Resets mon stats to base values
        /// </summary>
        public void ResetStats()
        {
            battleObj.monStats.atk = stats.atk;
            battleObj.monStats.spAtk = stats.spAtk;
            battleObj.monStats.def = stats.def;
            battleObj.monStats.spDef = stats.spDef;
            battleObj.monStats.speed = stats.speed;
        }

        /// <summary>
        /// Resets mon health to full health
        /// </summary>
        public void ResetHealth()
        {
            battleObj.monStats.hp = stats.hp;
        }

        /// <summary>
        /// Resets all status and conditions
        /// </summary>
        public void ResetStatus()
        {
            battleObj.ResetBattleObj();
        }

        /// <summary>
        /// Resets health, stats, and statuses
        /// </summary>
        public void FullReset()
        {
            ResetStats();
            ResetHealth();
            ResetStatus();
        }
    }
}
