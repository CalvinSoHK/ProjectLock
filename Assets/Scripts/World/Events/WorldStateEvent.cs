using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Event
{
    public class WorldStateEvent : BaseEvent
    {
        [SerializeField]
        private WorldState targetState;
        public void ChangeWorldState()
        {
            OnBeforeEventFire?.Invoke();
            CoreManager.Instance.worldStateManager.SetWorldState(targetState);
            OnEventFire?.Invoke();
            OnAfterEventFire?.Invoke();
        }
    }
}
