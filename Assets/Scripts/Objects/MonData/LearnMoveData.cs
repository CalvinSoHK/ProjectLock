using Mon.Moves;

namespace Mon.Moves
{
    /// <summary>
    /// Describes when a given move is learned
    /// </summary>
    [System.Serializable]
    public struct LearnMoveData
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
    }
}