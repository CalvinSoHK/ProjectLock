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
            Init();
        }

        /// <summary>
        /// Changes internal UI state
        /// </summary>
        /// <param name="_state"></param>
        protected virtual void ChangeState(UIState _state)
        {
            state = _state;
        }

        public void EnableState()
        {
            ChangeState(UIState.Printing);
        }

        public void DisableState()
        {
            ChangeState(UIState.Hiding);
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
            model.Refresh();
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
            model.InvokeModel(key);
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
            model.InvokeModel(key);
            ChangeState(UIState.Off);
        }
    }
}
