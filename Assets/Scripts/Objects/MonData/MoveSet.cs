using Mon.Moves;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.MonData
{
    [System.Serializable]
    /// <summary>
    /// Move set of an individual mon
    /// </summary>
    public class MoveSet
    {
        public static int MoveMaxCount
        {
            get
            {
                return 4;
            }
        }

        [SerializeField]
        private MoveData[] moveSet = new MoveData[MoveMaxCount];

        /// <summary>
        /// Number of valid moves in this move set
        /// </summary>
        public int MoveCount
        {
            get
            {
                int total = 0;
                for (int i = 0; i < moveSet.Length; i++)
                {
                    if (moveSet[i] != null)
                    {
                        total++;
                    }
                }
                return total;
            }
        }

        /// <summary>
        /// Tries to grab an empty move slot, if not able returns -1.
        /// </summary>
        /// <returns></returns>
        public int GetEmptyIndex()
        {
            for(int i = 0; i < moveSet.Length; i++)
            {
                if(moveSet[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Learns a move in the given index slot.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="index"></param>
        public void LearnMove(MoveData move, int index)
        {
            moveSet[index] = move;
        }

        /// <summary>
        /// Unlearns a move and sets to null.
        /// </summary>
        /// <param name="index"></param>
        public void UnlearnMove(int index)
        {
            moveSet[index] = null;
        }

        /// <summary>
        /// Gets the move at given index.
        /// Can return null, which means it is an empty slot.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MoveData GetMove(int index)
        {
            return moveSet[index];
        }

        /// <summary>
        /// Swaps moves between two indexes
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void SwitchMoveIndex(int index1, int index2)
        {
            MoveData temp = GetMove(0);
            moveSet[index1] = GetMove(1);
            moveSet[index2] = temp;
        }

        /// <summary>
        /// Returns a list of MoveDamage which contains the preliminary power guess of each move
        /// </summary>
        /// <param name="typeMultiplierList"></param>
        /// <returns></returns>
        public List<MoveDamage> CalcMovePower(List<TypeMultiplier> typeMultiplierList)
        {
            List<MoveDamage> moveDamageList = new List<MoveDamage>();

            //Loop through all moves
            for (int i = 0; i < MoveMaxCount; i++)
            {
                MoveData move = GetMove(i);
                if (move != null)
                {
                    float moveMultiplier = typeMultiplierList.Find(x => x.type == move.moveTyping).multiplier;
                    int power = Mathf.RoundToInt(move.power * moveMultiplier);
                    moveDamageList.Add(new MoveDamage(move, power, i, moveMultiplier));
                }
            }

            return moveDamageList;
        }
    }
}