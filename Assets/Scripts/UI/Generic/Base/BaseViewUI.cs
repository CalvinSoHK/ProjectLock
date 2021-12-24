using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public class BaseViewUI : MonoBehaviour, IUIBase
    {
        [Header("View Options")]
        [Tooltip("BaseController key to be listening out for")]
        [SerializeField]
        protected string controllerKey;

        [Tooltip("List of Elements that are managed by this view.")]
        [SerializeField]
        public List<BaseElementUI> managedList = new List<BaseElementUI>();

        protected UIState state = UIState.Off;

        /// <summary>
        /// The model we are using
        /// </summary>
        protected Model model;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            HandleState();
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
        /// Called when the view needs to be updated
        /// </summary>
        protected virtual void UpdateView(Model _model)
        {
            UpdateModelState(_model);
        }

        /// <summary>
        /// Updates the view based on new model
        /// By default it will adjust to the active state on the model and change states
        /// </summary>
        private void UpdateModelState(Model _model)
        {
            SetModel(_model);
            if(model.Active && state == UIState.Displaying && model.CheckRefresh())
            {
                RefreshUI();
            }
            else if (!model.Active && state == UIState.Displaying)
            {
                ChangeState(UIState.Hiding);
            }
            else if(model.Active && state == UIState.Off)
            {            
                ChangeState(UIState.Printing);
            }
        }

        /// <summary>
        /// Enables or disables managed elements.
        /// If an element is marked IsExplicitlyManaged, it will not be enabled by default here.
        /// </summary>
        /// <param name="active"></param>
        protected void SetUIActive(bool active)
        {
            foreach(BaseElementUI element in managedList)
            {
                if (!element.IsExplictlyManaged)
                {
                    if (active)
                    {
                        element.EnableElement();
                    }
                    else
                    {
                        element.DisableElement();
                    }
                }
            }
        }
    
        protected virtual void RefreshUI()
        {
            foreach (BaseElementUI element in managedList)
            {
                element.RefreshElement();
            }
        }

        public virtual void Init()
        {
            
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

        /// <summary>
        /// Now calls SetUiActive(true) on print
        /// </summary>
        public virtual void HandlePrintingState()
        {
            SetUIActive(true);
            ChangeState(UIState.Displaying);
        }

        public virtual void HandleDisplayState()
        {
            
        }

        /// <summary>
        /// Now calls SetUIActive(false) on print
        /// </summary>
        public virtual void HandleHidingState()
        {
            SetUIActive(false);
            ChangeState(UIState.Off);
        }

        /// <summary>
        /// Sets the model variables.
        /// </summary>
        /// <param name=""></param>
        protected virtual void SetModel(Model _model)
        {
            model = _model;
        }
    }
}
