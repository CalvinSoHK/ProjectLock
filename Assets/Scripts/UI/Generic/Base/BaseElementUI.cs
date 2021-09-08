using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public enum UIState
    {
        Off, //UI is completely inactive and only listens for when to turn on
        Printing, //Turns UI on, changes to displaying when initialized and on correctly
        Displaying, //Handles UI inputs, and decides when to turn off
        Hiding //Turns UI off, changes to Off when finished
    }

    public class BaseElementUI : MonoBehaviour, IUIBase, IUIElement
    {
        protected UIState state = UIState.Off;

        [Header("Element Options")]
        [Tooltip("When set to true, this will not be turned on and off by the normal UI set active.")]
        [SerializeField]
        private bool isExplicitlyManaged = false;

        /// <summary>
        /// Ignore list used for setuiactive
        /// </summary>
        [SerializeField]
        protected List<GameObject> ignoreList = new List<GameObject>();

        /// <summary>
        /// When set to true, should not be turned on by default.
        /// </summary>
        public bool IsExplictlyManaged
        {
            get
            {
                return isExplicitlyManaged;
            }
        }


        private void Start()
        {
            Init();
        }

        /// <summary>
        /// Updates UI. Currently calls all the HandleStates.
        /// Should not be overridden, logic should go in the HandleState logic instead.
        /// </summary>
        private void Update()
        {
            HandleState();
        }

        /// <summary>
        /// Subscribes to listeners.
        /// Should not be overridden.
        /// </summary>
        private void OnEnable()
        {
            SubscribeListeners();
        }

        /// <summary>
        /// Unsubscribes delegates.
        /// </summary>
        private void OnDisable()
        {
            UnsubscribeListeners();
        }

        public void HandleState()
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
                case UIState.Hiding:
                    HandleHidingState();
                    break;
                default:
                    break;
            }
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
        /// Called on OnEnable
        /// Should be used to subscribe to all needed delegates
        /// </summary>
        protected virtual void SubscribeListeners()
        {

        }

        /// <summary>
        /// Called on OnDisable
        /// Should be used to unsubscribe to all needed delegates
        /// </summary>
        protected virtual void UnsubscribeListeners()
        {

        }

        public virtual void Init()
        {
            
        }

        public virtual void HandleOffState()
        {

        }

        public virtual void HandlePrintingState()
        {
            SetObjectsActive(true);
            ChangeState(UIState.Displaying);
        }

        public virtual void HandleDisplayState()
        {

        }

        public virtual void HandleHidingState()
        {
            SetObjectsActive(false);
            ChangeState(UIState.Off);
        }

        public virtual void EnableElement()
        {

        }

        public virtual void DisableElement()
        {

        }

        public virtual void RefreshElement()
        {

        }

        protected void SetObjectsActive(bool state)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                GameObject obj = transform.GetChild(i).gameObject;
                if (!ignoreList.Contains(obj))
                {
                    obj.SetActive(state);
                }
            }
        }
    }
}