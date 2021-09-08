using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI.Selector;

namespace UI.Dropdown
{
    [RequireComponent(typeof(GUILayout))]
    /// <summary>
    /// Strictor selector UI which populates the drop down on spawn
    /// </summary>
    public class DropdownControllerUI : SelectorControllerUI
    {
        [Header("Dropdown Options")]
        [Tooltip("Dropdown buttons.")]
        [SerializeField]
        private List<string> buttonList = new List<string>();

        private DropdownModelUI dropdownModel;

        public delegate void DropdownOptionEvent(string selectorKey, string selectableKey);
        public static DropdownOptionEvent DropdownOptionFire;

        public override void Init()
        {
            base.Init();
            model = new DropdownModelUI();
            dropdownModel = (DropdownModelUI)model;
        }

        /// <summary>
        /// Attempts to replace the dropdown menu with a list of buttons.
        /// If the list of buttons is already the one that is showing (order matters),
        /// then it will not do anything.
        /// </summary>
        /// <param name="_buttonList"></param>
        public void MakeOrReplaceDropdown(List<string> _buttonList)
        {
            if(buttonList.Count != _buttonList.Count)
            {
                dropdownModel.SetDropdownDTO(CreateDefaultOptions(_buttonList));
                dropdownModel.SetDropdownUpdate(true);
                dropdownModel.InvokeModel(key);
            }
        }

        /// <summary>
        /// Makes a list of dropdown element DTOs from a list of options.
        /// These options will invoke default callbacks.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected DropdownDTO CreateDefaultOptions(List<string> options)
        {           
            List<DropdownElementDTO> dropdownList = new List<DropdownElementDTO>();
            foreach (string option_key in options)
            {
                dropdownList.Add(new DropdownElementDTO(
                    option_key,
                    new UnityAction(() => DropdownOptionFire?.Invoke(key, option_key))
                    ));
            }
            DropdownDTO dto = new DropdownDTO(dropdownList);
            return dto;
        }
    }
}
