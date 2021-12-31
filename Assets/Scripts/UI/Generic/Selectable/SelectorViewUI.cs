using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Selector
{
    public class SelectorViewUI : BaseViewUI
    {
        /// <summary>
        /// Currently selected index
        /// </summary>
        protected int selectedIndex = 0;

        /// <summary>
        /// List of selector elements
        /// </summary>
        [SerializeField]
        protected List<SelectorElementUI> selectorElementList = new List<SelectorElementUI>();

        protected SelectorModelUI selectorModel = new SelectorModelUI();

        protected int selectorBoundMax;

        [SerializeField]
        [Tooltip("When true after selecting it will lock")]
        protected bool lockOnSelect = true;

        [SerializeField]
        [Tooltip("When true, it will select the first option by default")]
        protected bool selectOnStart = false;

        [SerializeField]
        [Tooltip("When true, grabs all selector elements from the managed list. Use it if you aren't spawning at runtime.")]
        protected bool grabSelectorsOnStart = true;

        protected virtual void OnEnable()
        {
            SelectorModelUI.ModelUpdate += UpdateModel;          
        }

        protected virtual void OnDisable()
        {
            SelectorModelUI.ModelUpdate -= UpdateModel;
        }

        public override void Init()
        {
            base.Init();

            if (grabSelectorsOnStart)
            {
                //Grab all selector elements and put in list
                foreach (BaseElementUI element in managedList)
                {
                    SelectorElementUI selectorElement = element.GetComponent<SelectorElementUI>();
                    if (selectorElement != null)
                    {
                        selectorElementList.Add(selectorElement);
                    }
                }
            }
            selectorBoundMax = selectorElementList.Count;
        }

        public override void HandlePrintingState()
        {
            base.HandlePrintingState();
            selectedIndex = 0;
            if (selectOnStart)
            {
                selectorModel.SetSelect(true);
            }

            RefreshUI();
            SelectorElementUI.SelectorSelectFire += UpdateSelected;
        }
        public override void HandleHidingState()
        {
            base.HandleHidingState();
            SelectorElementUI.SelectorSelectFire -= UpdateSelected;
        }

        /// <summary>
        /// Selects the given index
        /// </summary>
        /// <param name="indexChange"></param>
        private void UpdateIndex()
        {           
            if (selectorModel.IndexChange != 0)
            {
                selectedIndex += selectorModel.IndexChange;
                if(selectedIndex < 0)
                {
                    selectedIndex = selectorBoundMax - 1;
                }
                else if (selectedIndex > selectorBoundMax - 1)
                {
                    selectedIndex = 0;
                }
            }            
        }

        

        /// <summary>
        /// Updates hover states of all selector elements
        /// Only calls if there was an index change.
        /// </summary>
        private void UpdateHover()
        {
            foreach(SelectorElementUI element in selectorElementList)
            {
                if(element.SelectableIndex == selectedIndex)
                {
                    element.Hover();
                }
                else
                {
                    element.Dehover();
                }
            }
        }

        /// <summary>
        /// If model is set to select, select the currently selected index
        /// </summary>
        private void UpdateSelect()
        {
            //Reset selected index if turned on
            if (selectorModel.CheckResetSelectIndex())
            {
                selectedIndex = 0;
            }

            if (selectorModel.Select)
            {
                foreach (SelectorElementUI element in selectorElementList)
                {
                    if (element.SelectableIndex == selectedIndex)
                    {
                        if (lockOnSelect)
                        {
                            selectorModel.SetLocked(true);
                        }
                        element.Select();
                    }
                    else
                    {
                        element.Deselect();
                    }
                }
            }
        }

        protected override void RefreshUI()
        {
            base.RefreshUI();
            UpdateIndex();
            UpdateHover();
            UpdateSelect();
        }

        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            selectorModel = (SelectorModelUI)_model;
        }

        private void UpdateSelected(string _key, int _selectedIndex)
        {
            if (controllerKey.Equals(_key))
            {
                selectedIndex = _selectedIndex;
                RefreshUI();
            }
        }
    }
}