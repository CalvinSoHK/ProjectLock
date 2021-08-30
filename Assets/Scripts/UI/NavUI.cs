using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;

namespace UI
{
    /// <summary>
    /// NavUI. Repopulates on every call. In the future options may change dynamically so it is required.
    /// </summary>
    public class NavUI : DropdownUI
    {
        /// <summary>
        /// Returns the list of the Overworld UI options
        /// </summary>
        /// <returns></returns>
        private List<DropdownElementDTO> GetOverworldList()
        {
            List<string> optionKeys = new List<string>();
            optionKeys.Add("Party");
            optionKeys.Add("Inventory");
            //optionKeys.Add("Options");
            return CreateDefaultOptions(optionKeys);
        }

        protected override void HandleOffState()
        {
            base.HandleOffState();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld && Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Navigation, InputEnums.InputAction.Down))
            {
                PopulateDropdown(new DropdownDTO(groupKey, GetOverworldList()));
                ChangeState(UIState.Printing);
            }
        }
    }
}
