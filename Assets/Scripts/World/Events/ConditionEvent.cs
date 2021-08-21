using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using World.Condition;

namespace World.Event
{
    [RequireComponent(typeof(BaseCondition))]
    /// <summary>
    /// Waits for a condition to be invoked
    /// </summary>
    public class ConditionEvent : BaseEvent
    {
        [SerializeField]
        private string conditionID;

        [SerializeField]
        private UnityEvent OnConditionTrue;

        [SerializeField]
        private UnityEvent OnConditionFalse;

        private void OnEnable()
        {
            BaseCondition.OnCondition += CheckCondition;
        }

        private void OnDisable()
        {
            BaseCondition.OnCondition -= CheckCondition;
        }

        private void CheckCondition(bool _condition, string _conditionID)
        {
            if (conditionID.Equals(_conditionID))
            {
                OnBeforeEventFire?.Invoke();
                if (_condition)
                {
                    OnConditionTrue?.Invoke();
                }
                else
                {
                    OnConditionFalse?.Invoke();
                }
                OnEventFire?.Invoke();
                OnAfterEventFire?.Invoke();
            }
        }
    }
}
