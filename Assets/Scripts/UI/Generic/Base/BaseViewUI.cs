using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private void OnEnable()
        {
            MessageQueue.MessageEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueue.MessageEvent -= HandleMessage;
        }

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
            SetModel(_model);
            bool refresh = model.CheckRefresh();

            if (model.Active && state == UIState.Displaying && refresh)
            {
                RefreshUI();
            }
            else if (!model.Active && state == UIState.Displaying)
            {
                ChangeState(UIState.Hiding);
            }
            else if (model.Active && state == UIState.Off)
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
                if (!element.IsExplictlyManaged && active)
                {
                    EnableElement(element);
                }
                else
                {
                    DisableElement(element);
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
        /// </summary>
        /// <param name="_model"></param>
        protected virtual void InitSet(string _JSONmodel)
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

        /// <summary>
        /// Enables a given element. Turns object on
        /// </summary>
        /// <param name="element"></param>
        protected virtual void EnableElement<T>(T element) where T : BaseElementUI
        {
            if (!element.gameObject.activeSelf)
            {
                element.gameObject.SetActive(true);
            }
            element.EnableElement(controllerKey);
        }

        /// <summary>
        /// Disables element. Turns off gameobject.
        /// </summary>
        /// <param name="element"></param>
        protected virtual void DisableElement<T>(T element) where T : BaseElementUI
        {
            if (element.gameObject.activeSelf)
            {
                element.gameObject.SetActive(false);
            }
            element.DisableElement();
        }

        /// <summary>
        /// Updates the model. 
        /// Subscribe to a model's delegate to update to that model's changes
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_model"></param>
        protected void UpdateModel(string _key, Model _model)
        {
            if (_key.Equals(controllerKey))
            {
                UpdateView(_model);
            }
        }

        /// <summary>
        /// Handles messages that we hear
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fMsg"></param>
        protected virtual void HandleMessage(string id, FormattedMessage fMsg)
        {

        }
    }
}
