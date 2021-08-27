using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseUI : MonoBehaviour
    {
        protected enum UIState
        {
            Off,
            Printing,
            Displaying
        }

        protected UIState state = UIState.Off;

        /// <summary>
        /// Updates UI. Currently calls all the HandleStates.
        /// Remember to call base.Update if overriding.
        /// </summary>
        protected virtual void Update()
        {
            HandleState();
        }

        /// <summary>
        /// Handles states. Calls all the overrideable functions
        /// </summary>
        private void HandleState()
        {
            switch (state)
            {
                case UIState.Off:
                    HandleOffState();
                    break;
                case UIState.Printing:
                    HandlePrintingState();
                    break;
                case UIState.Displaying:
                    HandleDisplayState();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles Off UI state
        /// </summary>
        protected virtual void HandleOffState()
        {

        }

        /// <summary>
        /// Handles Printing UI state
        /// </summary>
        protected virtual void HandlePrintingState()
        {

        }

        /// <summary>
        /// Handles Display UI state
        /// By default calls HandleDisable
        /// </summary>
        protected virtual void HandleDisplayState()
        {
            HandleDisable();
        }

        /// <summary>
        /// Handles disabling the UI state
        /// </summary>
        protected virtual void HandleDisable()
        {

        }

        /// <summary>
        /// Changes internal UI state
        /// </summary>
        /// <param name="_state"></param>
        protected void ChangeState(UIState _state)
        {
            state = _state;
        }

        /// <summary>
        /// Sets all children to active state
        /// </summary>
        /// <param name="active"></param>
        protected void SetUIActive(bool active)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }
    }
}
