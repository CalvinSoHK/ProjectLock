using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Dropdown;
using UI.Base;

namespace UI.Nav
{
    /// <summary>
    /// NavUI. Repopulates on every call. In the future options may change dynamically so it is required.
    /// </summary>
    public class NavControllerUI : DropdownControllerUI
    {
        /// <summary>
        /// Returns the list of the Overworld UI options
        /// </summary>
        /// <returns></returns>
        private void PopulateOverworldDropdown()
        {
            List<string> optionKeys = new List<string>();
            optionKeys.Add("Party");
            optionKeys.Add("Inventory");
            optionKeys.Add("Options");
            MakeOrReplaceDropdown(optionKeys);
        }

        public override void HandleOffState()
        {
            base.HandleOffState();
            //Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld && 
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Navigation, InputEnums.InputAction.Down))
            {
                PopulateOverworldDropdown();
                ChangeState(UIState.Printing);
            }
        }
    }
}
