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

            //Grab all selector elements and put in list
            foreach (BaseElementUI element in managedList)
            {
                SelectorElementUI selectorElement = element.GetComponent<SelectorElementUI>();
                if (selectorElement != null)
                {
                    selectorElementList.Add(selectorElement);
                }               
            }
            selectorBoundMax = selectorElementList.Count;
        }

        /// <summary>
        /// Written per view since the second input is differently typed per view.
        /// Calls UpdateView which we override
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_model"></param>
        private void UpdateModel(string _key, SelectorModelUI _model)
        {
            if (_key.Equals(controllerKey))
            {
                UpdateView(_model);
            }
        }

        protected override void UpdateView(Model _model)
        {
            base.UpdateView(_model);
            UpdateIndex();
            UpdateSelect();
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
                RefreshUI();
            }            
        }

        private void UpdateSelect()
        {
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

        protected override void RefreshUI()
        {
            base.RefreshUI();
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