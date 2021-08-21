using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Condition
{
    /// <summary>
    /// BaseCondition class. Helps hook up events through inspector by checking various states
    /// </summary>
    public class BaseCondition : MonoBehaviour
    {
        [SerializeField]
        private string conditionID;

        /// <summary>
        /// ID for this condition. Used to make sure we are getting the right one
        /// </summary>
        public string ConditionID
        {
            get
            {
                return conditionID;
            }
        }

        public delegate void ConditionEvent(bool condition, string conditionID);

        /// <summary>
        /// Delegate to invoke when condition is valid
        /// </summary>
        public static ConditionEvent OnCondition;

        /// <summary>
        /// Function that checks the condition.
        /// Needs to be overriden per implementation
        /// </summary>
        public virtual void CheckCondition()
        {
            throw new System.Exception("Condition not implemented: " + gameObject + " with ID: " + ConditionID);
        }
    }
}
