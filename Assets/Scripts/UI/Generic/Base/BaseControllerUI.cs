using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Page;
using UnityEngine;

namespace UI.Base
{
    /// <summary>
    /// ModelUI scripts contain the logic for manipulating a whole panel.
    /// Controller scripts contain information for manipulating elements.
    /// View scripts contain logic to control themselves.
    /// </summary>
    public class BaseControllerUI : IUIBase, IControllerUI
    {
        [Header("Controller Options")]
        [SerializeField]
        [Tooltip("Key is used for calling to this UI")]
        public string key;

        public Model model;

        protected UIState state = UIState.Off;

        /// <summary>
        /// Sets up the controller.
        /// Custom setup can be included in the overrideable Init
        /// </summary>
        /// <param name="_key"></param>
        public void SetupController(string _key)
        {
            key = _key;
            MessageQueue.MessageEvent += HandleMessage;
            UIPagesManager.OnSaveCompleteEvent += OnSaveComplete;
            Init();
            model.Init();
        }

        /// <summary>
        /// Destroys the controller.
        /// Cleans up anything that needs to be cleaned up on destroying the object managing the controller.
        /// </summary>
        public virtual void DestroyController()
        {
            MessageQueue.MessageEvent -= HandleMessage;
            UIPagesManager.OnSaveCompleteEvent -= OnSaveComplete;
        }

        /// <summary>
        /// Changes internal UI state
        /// </summary>
        /// <param name="_state"></param>
        protected virtual void ChangeState(UIState _state)
        {
            state = _state;
        }

        /// <summary>
        /// Enables the UI.
        /// Only works if the UI is off.
        /// Will return true if successful
        /// </summary>
        /// <returns></returns>
        public bool TryEnableState()
        {
            if (state == UIState.Off)
            {
                ChangeState(UIState.Printing);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Disables the UI.
        /// Only works if already displaying. 
        /// Will return true if successful
        /// </summary>
        /// <returns></returns>
        public bool TryDisableState()
        {
            if (state == UIState.Displaying)
            {
                ChangeState(UIState.Hiding);
                return true;
            }
            return false;
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
        /// BaseController sets the model to _model. For specific typecasted types you should add them after the base call.
        /// </summary>
        /// <param name="_model"></param>
        protected virtual void InitSet(string _JSONmodel)
        {
            model = JsonUtility.FromJson<Model>(_JSONmodel);
        }

        /// <summary>
        /// Calls reset on the model and refreshes.
        /// </summary>
        public virtual void Reset()
        {
            model.Reset();
            Refresh();
        }

        /// <summary>
        /// Reinvokes the model and thus "Refreshes" the UI
        /// </summary>
        protected virtual void Refresh()
        {
            model.InvokeModel(key);
        }

        /// <summary>
        /// Handles all the UI states.
        /// Called by anything maintaining the life-cycle
        /// </summary>
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
        /// Handles the off state for this UI.
        /// Usually allows for checking user input.
        /// BaseController Off state does not do anything.
        /// </summary>
        public virtual void HandleOffState()
        {

        }

        /// <summary>
        /// Handles the printing state for this UI.
        /// BaseController Printing state does do something, make sure to call base unless you specifically need to override it.
        /// </summary>
        public virtual void HandlePrintingState()
        {
            model.SetActive(true);
            Refresh();
            ChangeState(UIState.Displaying);

            //Add this as an active controller
            Core.CoreManager.Instance.uiManager.pagesManager.AddController(this, model);
        }

        /// <summary>
        /// Handles the display state for this UI.
        /// </summary>
        public virtual void HandleDisplayState()
        {

        }

        /// <summary>
        /// Handles the hiding state for this UI.
        /// BaseController Hiding state turns the UI off. Make sure to use call base unless you specifically need to override it.
        /// </summary>
        public virtual void HandleHidingState()
        {
            model.SetActive(false);
            Refresh();
            ChangeState(UIState.Off);
            
            //Remove ourselves from the active controller list if we are hiding
            Core.CoreManager.Instance.uiManager.pagesManager.RemoveController(this);
        }

        /// <summary>
        /// Function that fires when the return key is fired.
        /// Call base.OnReturnKey when you want to hide the UI.
        /// </summary>
        protected virtual void OnReturnKey()
        {
            ChangeState(UIState.Hiding);
        }

        /// <summary>
        /// Handles incoming messages.
        /// Is set up as async but does not necessarily need to call anything with await
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fMsg"></param>
        protected virtual void HandleMessage(string id, FormattedMessage fMsg)
        {
            if (id.Equals(MessageQueueManager.UI_KEY))
            {
                if (fMsg.key.Equals(UIPagesManager.PAGESTACK_OUT_KEY))
                {
                    ProcessPagesOutKey(fMsg);
                }
            }
        }

        /// <summary>
        /// Sets this controller to a given model and enable this UI controller
        /// </summary>
        /// <param name="model"></param>
        public void SetModelAndEnable(string _JSONmodel)
        {
            //Initialize again with the given model
            Init(_JSONmodel);

            //If we were off, change to printing
            if (state == UIState.Off)
            {
                ChangeState(UIState.Printing);
            }
            //If we are displaying call a refresh
            else if (state == UIState.Displaying)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Processes a UIPagesManagerOutMessage
        /// </summary>
        /// <param name="fMsg"></param>
        private void ProcessPagesOutKey(FormattedMessage fMsg)
        {
            UIPagesManagerOutMessage message = JsonUtility.FromJson<UIPagesManagerOutMessage>(fMsg.message);
            if (message.returnKeyPressed)
            {
                if (state == UIState.Displaying)
                {
                    OnReturnKey();
                }
            }
        }
    
        /// <summary>
        /// Disables this UI if it is displaying and not in the ignore list
        /// Is subscribed to the UIPagesManager OnSaveComplete event
        /// </summary>
        protected virtual void OnSaveComplete(List<BaseControllerUI> ignoreList)
        {
            if(state == UIState.Displaying && !ignoreList.Contains(this))
            {
                ChangeState(UIState.Hiding);
            }
        }
    }
}
