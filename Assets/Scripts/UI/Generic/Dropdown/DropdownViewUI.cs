using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UI.Base;

namespace UI.Dropdown
{
    public class DropdownViewUI : SelectorViewUI
    {
        [Header("Dropdown Options")]
        [Tooltip("Prefab for selectable in this dropdown")]
        [SerializeField]
        protected DropdownElementUI elementPrefab;

        protected DropdownModelUI dropdownModel;

        protected override void OnEnable()
        {
            DropdownModelUI.ModelUpdate += UpdateModel;
        }

        protected override void OnDisable()
        {
            DropdownModelUI.ModelUpdate -= UpdateModel;
        }

        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            dropdownModel = (DropdownModelUI)_model;
        }

        /// <summary>
        /// Empties children so we can repopulate the menu
        /// </summary>
        protected virtual void EmptyChildren()
        {
            for (int i = transform.childCount - 1; i > -1; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            selectorElementList.Clear();
            managedList.Clear();
        }

        private void UpdateModel(string _key, DropdownModelUI _model)
        {
            if (_key.Equals(controllerKey))
            {
                UpdateView(_model);
            }
        }

        protected override void UpdateView(Model _model)
        {
            base.UpdateView(_model);
            if (dropdownModel.Active)
            {
                Debug.Log(dropdownModel.Active);
                PopulateDropdown(dropdownModel);
            }
            else
            {
                EmptyChildren();
            }
            
        }

        /// <summary>
        /// Populates this DropdownUI based on the info passed to it
        /// </summary>
        /// <param name="dto"></param>
        public void PopulateDropdown(DropdownModelUI _model)
        {
            EmptyChildren();
            int index = 0;
            foreach (DropdownElementDTO elementDTO in _model.DropdownDTO.ElementList)
            {
                DropdownElementUI element = Instantiate(elementPrefab, transform);
                element.SetIndex(index);
                element.displayText = elementDTO.ElementText;
                element.OnSelect.AddListener(elementDTO.OnElementSelect);
                index++;
                selectorElementList.Add(element);
                element.EnableElement(controllerKey);
                managedList.Add(element);
            }
            Init();
        }
    }
}
