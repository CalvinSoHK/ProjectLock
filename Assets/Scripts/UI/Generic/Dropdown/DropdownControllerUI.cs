using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI.Selector;
using UI.Base;
using CustomInput;
using Core.MessageQueue;

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

        /// <summary>
        /// Making a new dropdown model UI and assigning it to the models
        /// </summary>
        protected override void InitFresh()
        {
            model = new DropdownModelUI();
            selectorModel = (SelectorModelUI)model;
            dropdownModel = (DropdownModelUI)model;
        }

        protected override void InitSet(string _JSONmodel)
        {
            dropdownModel = JsonUtility.FromJson<DropdownModelUI>(_JSONmodel);
            selectorModel = dropdownModel;
            model = dropdownModel;
        }

        /// <summary>
        /// Empties the dropdown options and hides the menu
        /// </summary>
        public void EmptyAndHideDropdown()
        {
            selectorModel.SetSelect(false);
            selectorModel.SetLocked(false);
            dropdownModel.SetDropdownDTO(new DropdownDTO(new List<DropdownElementDTO>()));
            ChangeState(UIState.Hiding);
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
                Refresh();
                TryEnableState();
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
            int counter = 0;
            foreach (string option_key in options)
            {
                dropdownList.Add(new DropdownElementDTO(
                    option_key,
                    new UnityAction(() => Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
                        MessageQueueManager.UI_KEY, 
                        key, 
                        JsonUtility.ToJson(new DropdownMessageObject(counter, option_key)))
                    )));
                counter++;
            }
            DropdownDTO dto = new DropdownDTO(dropdownList);
            return dto;
        }

        public override void HandleDisplayState()
        {
            if (!model.Locked)
            {
                IndexControl();
            }
        }
    }
}
