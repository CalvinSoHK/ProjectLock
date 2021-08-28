using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseUI : MonoBehaviour
    {
        protected enum UIState
        {
            Off, //Turns UI off
            Printing, //Turns UI on, changes to displaying when initialized and on correctly
            Displaying //Handles UI inputs, and decides when to turn off
        }

        protected UIState state = UIState.Off;

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void Start()
        {
            Init();
        }

        /// <summary>
        /// Updates UI. Currently calls all the HandleStates.
        /// Remember to call base.Update if overriding.
        /// </summary>
        protected virtual void Update()
        {
            HandleState();
        }

        /// <summary>
        /// Inits the UI. Overrideable, default does nothing but called on Start.
        /// </summary>
        protected virtual void Init()
        {

        }

        /// <summary>
        /// Resets the UI. Overrideable, default does nothing.
        /// </summary>
        protected virtual void Reset()
        {

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
            SetUIActive(false);
        }

        /// <summary>
        /// Handles Printing UI state
        /// </summary>
        protected virtual void HandlePrintingState()
        {
            SetUIActive(true);
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

        /// <summary>
        /// Sets all children to active st ate.
        /// Ignores objects in the ignoreList
        /// </summary>
        /// <param name="active"></param>
        /// <param name="ignoreList"></param>
        protected void SetUIActive(bool active, List<GameObject> ignoreList)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject obj = transform.GetChild(i).gameObject;
                if (!ignoreList.Contains(obj))
                {
                    obj.SetActive(active);
                }
            }
        }
    }
}
