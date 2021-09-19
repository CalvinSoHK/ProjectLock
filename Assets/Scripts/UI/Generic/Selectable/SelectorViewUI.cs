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
        protected List<SelectorElementUI> selectorElementList = new List<SelectorElementUI>();

        protected SelectorModelUI selectorModel = new SelectorModelUI();

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
                    selectedIndex = selectorElementList.Count - 1;
                }
                else if (selectedIndex > selectorElementList.Count - 1)
                {
                    selectedIndex = 0;
                }
                UpdateHover();
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
                        selectorModel.SetLocked(true);
                        element.Select();
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

        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            selectorModel = (SelectorModelUI)_model;
        }
    }
}