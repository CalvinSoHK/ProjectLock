using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
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
        public void SetupController(string _key)
        {
            key = _key;
            MessageQueue.MessageEvent += HandleMessage;
            Init();
        }

        public virtual void DestroyController()
        {
            MessageQueue.MessageEvent -= HandleMessage;
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
            if(state == UIState.Displaying)
            {
                ChangeState(UIState.Hiding);
                return true;
            }
            return false;
        }

        public virtual void Init()
        {
            model.Init();
        }

        public virtual void Reset()
        {
            model.Reset();
            model.InvokeModel(key);
        }

        protected virtual void Refresh()
        {
            model.InvokeModel(key);
        }

        public virtual void HandleState()
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

        public virtual void HandleOffState()
        {

        }

        public virtual void HandlePrintingState()
        {
            model.SetActive(true);
            Refresh();
            ChangeState(UIState.Displaying);
        }

        public virtual void HandleDisplayState()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Return, CustomInput.InputEnums.InputAction.Down))
            {
                ChangeState(UIState.Hiding);
            }
        }

        public virtual void HandleHidingState()
        {
            model.SetActive(false);
            Refresh();
            ChangeState(UIState.Off);
        }

        protected virtual void HandleMessage(string id, FormattedMessage fMsg)
        {

        }
    }
}
