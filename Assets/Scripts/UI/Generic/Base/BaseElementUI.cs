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

        protected string key = "";

        protected bool initialized = false;

        private void Start()
        {
            if (!initialized)
            {
                Init();
            }
        }

        /// <summary>
        /// Initializes the controller, makes new models.
        /// If a model is passed in it will use that instead
        /// NOTE: No need to call model.Init() since it is called after the Init call by default in SetupController
        /// </summary>
        public void Init(string _JSONmodel = null)
        {
            InitGeneral();
            if (_JSONmodel == null)
            {
                InitFresh();
            }
            else
            {
                InitSet(_JSONmodel);
            }
        }

        /// <summary>
        /// Runs during init for every case, either if we are using new models or if we are setting it to an existing one
        /// </summary>
        protected virtual void InitGeneral()
        {
            initialized = true;
        }

        /// <summary>
        /// Inits the controller with fresh models.
        /// Do not use base for this function. Generally you should make a new model of the right type in each controller.
        /// </summary>
        protected virtual void InitFresh()
        {

        }

        /// <summary>
        /// Inits the controller with a set model.
        /// </summary>
        /// <param name="_model"></param>
        protected virtual void InitSet(string _JSONmodel)
        {
            
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

        public virtual void EnableElement(string _key)
        {
            key = _key;
        }

        public virtual void DisableElement()
        {

        }

        public virtual void RefreshElement()
        {
            if (!initialized)
            {
                Init();
            }
        }

        /// <summary>
        /// Sets all objects to on or off. If we are turning it off ignore the ignore list.
        /// </summary>
        /// <param name="state"></param>
        protected void SetObjectsActive(bool state)
        {
            for(int i = 0; i < transform.childCount; i++)
            {                
                GameObject obj = transform.GetChild(i).gameObject;
                if (state)
                {
                    if (!ignoreList.Contains(obj))
                    {
                        obj.SetActive(true);
                    }
                }
                else
                {
                    obj.SetActive(false);
                }
                
            }
        }
    }
}
