using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IControllerUI
    {
        /// <summary>
        /// Initializes the controller with the given key
        /// </summary>
        /// <param name="_key"></param>
        public abstract void SetupController(string _key);
        /// <summary>
        /// Destroys the controllers and cleans anything that needs to be cleaned up.
        /// (Mainly for unsubscribing delegates in the event of a Destroy being called)
        /// </summary>
        public abstract void DestroyController();
        
        /// <summary>
        /// Handles the states in the controller.
        /// Run in the update loop separately.
        /// </summary>
        public abstract void HandleState();
    }
}
