using Mon.Individual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mon.Moves
{
    /// <summary>
    /// Data for Move data.
    /// </summary>
    [CreateAssetMenu(fileName = "MoveData",
    menuName = "MonGeneration/MoveData", order = 4)]
    public class MoveData : ScriptableObject
    {
        public string moveName;
        public MonType moveType;
        public int power;
        public int accuracy;

        /// <summary>
        /// Used when the move is about to be used.
        /// Examples: Dream Eater only works if they're sleeping. This MoveConditional can check that.
        /// </summary>
        public MoveConditional OnMoveBegin;

        /// <summary>
        /// Used when the move is complete.
        /// Examples: Confusion has a chance of proccing the confusion status.
        /// </summary>
        public MoveConditional OnMoveComplete;

        //Suggested level range for learning this skill
        public int minLevelRange, maxLevelRange;

        //Mon MUST have all strict tags to be viable to learn this move
        public string[] strictTags;

        //Mon MUST have at least ONE of the optional tags to learn it, AND strict tag requirement.
        public string[] optionalTags;
    }
}
