using Mon.Moves;
using System;

namespace Mon.Moves
{
    /// <summary>
    /// Describes when a given move is learned
    /// </summary>
    [System.Serializable]
    public struct LearnMoveData : IComparable
    {
        /// <summary>
        /// The move in reference
        /// </summary>
        public MoveData move;

        /// <summary>
        /// Level when this move is set to be learned
        /// </summary>
        public int level;

        /// <summary>
        /// Constructs this struct
        /// </summary>
        /// <param name="_move"></param>
        /// <param name="_level"></param>
        public LearnMoveData(MoveData _move, int _level)
        {
            move = _move;
            level = _level;
        }

        /// <summary>
        /// Comparator for this struct
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            LearnMoveData _moveData = (LearnMoveData)obj;
            return level.CompareTo(_moveData.level);
        }
    }
}