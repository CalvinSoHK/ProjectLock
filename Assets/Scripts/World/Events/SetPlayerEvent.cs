using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Event
{
    public class SetPlayerEvent : BaseEvent
    {
        /// <summary>
        /// Sets the player active through core manager
        /// </summary>
        /// <param name="state"></param>
        public void SetPlayer(bool state)
        {
            OnBeforeEventFire?.Invoke();
            OnEventFire?.Invoke();
            Core.CoreManager.Instance.SetPlayerActive(state);
            OnAfterEventFire?.Invoke();
        }
    }
}
