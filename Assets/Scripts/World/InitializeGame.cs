using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Event
{
    /// <summary>
    /// Initializes game
    /// </summary>
    public class InitializeGame : BaseEvent
    {
        /// <summary>
        /// Async initialize of the game
        /// </summary>
        public async void InitTask()
        {
            OnEventFire?.Invoke();
            await Core.CoreManager.Instance.Initialize();
            OnAfterEventFire?.Invoke();
        }
    }
}
