using Mon.Moves;
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
        private MoveData[] moveSet = new MoveData[4];

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
        /// Sets move1 to index1, and move2 to index2.
        /// </summary>
        /// <param name="move1"></param>
        /// <param name="index1"></param>
        /// <param name="move2"></param>
        /// <param name="index2"></param>
        public void SwitchMoveIndex(MoveData move1, int index1, MoveData move2, int index2)
        {
            moveSet[index1] = move1;
            moveSet[index2] = move2;
        }

    }
}