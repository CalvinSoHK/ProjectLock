using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IUIBase
    {
        /// <summary>
        /// Inits the UI. Not called anywhere by default
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Handles states. Calls all the other HandleStates
        /// </summary>
        public abstract void HandleState();

        /// <summary>
        /// Handles Off UI state
        /// </summary>
        public abstract void HandleOffState();

        /// <summary>
        /// Handles Printing UI state
        /// </summary>
        public abstract void HandlePrintingState();

        /// <summary>
        /// Handles Display UI state
        /// By default calls HandleDisable
        /// </summary>
        public abstract void HandleDisplayState();

        /// <summary>
        /// Handles transitioning to the Off state
        /// </summary>
        public abstract void HandleHidingState();
    }
}
