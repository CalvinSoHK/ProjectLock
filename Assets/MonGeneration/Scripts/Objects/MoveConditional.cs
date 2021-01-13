using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Individual;

namespace Mon.Moves
{
    /// <summary>
    /// Base MoveConditional. Create new ones with this as a base.
    /// </summary>
    public class MoveConditional
    {
        /// <summary>
        /// Condition function.
        /// Returns a bool, if true, the move will fire off.
        /// If false, the move will fail.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool OnMoveCondition(MonObject user, MonObject target)
        {
            return false;
        }
    }
}
