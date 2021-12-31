using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Dropdown;
using UI.Base;
using UI.Enums;

namespace UI.Nav
{
    /// <summary>
    /// NavUI. Repopulates on every call. In the future options may change dynamically so it is required.
    /// </summary>
    public class NavControllerUI : DropdownControllerUI
    {
        public override void HandlePrintingState()
        {
            DropdownControllerUI.DropdownOptionFire += OnDropdownSelect;
            base.HandlePrintingState();
        }

        public override void HandleHidingState()
        {
            DropdownControllerUI.DropdownOptionFire -= OnDropdownSelect;
            base.HandleHidingState();
            Core.CoreManager.Instance.player.EnableInputMovement();
        }

        /// <summary>
        /// Returns the list of the Overworld UI options
        /// </summary>
        /// <returns></returns>
        public void PopulateOverworldDropdown(DropdownTypes type)
        {
            switch (type)
            {
                case DropdownTypes.Navigation:
                    PopulateNavigationDropdown();
                    break;
                case DropdownTypes.Party:
                    PopulatePartyDropdown();
                    break;
                default:
                    break;
            }
        }

        public override void HandleOffState()
        {
            base.HandleOffState();
            //Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld && 
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Navigation, InputEnums.InputAction.Down))
            {
                Core.CoreManager.Instance.player.DisableInputMovement();
                PopulateOverworldDropdown(DropdownTypes.Navigation);
                ChangeState(UIState.Printing);
            }
        }

        private void PopulateNavigationDropdown()
        {
            List<string> optionKeys = new List<string>();
            optionKeys.Add("Party");
            optionKeys.Add("Inventory");
            optionKeys.Add("Options");
            MakeOrReplaceDropdown(optionKeys);
        }
        private void PopulatePartyDropdown()
        {
            List<string> optionKeys = new List<string>();
            optionKeys.Add("Swap");
            optionKeys.Add("Details");
            MakeOrReplaceDropdown(optionKeys);
        }

        private void OnDropdownSelect(string key, string optionKey)
        {
            //Hide Dropdown Needs change. Button still appears and works despite DisableState()
            //dropdownController.DisableState();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
            {
                if (optionKey == "Swap")
                {
                    //Unlock everything
                    //Allow user to change index of party
                    Core.CoreManager.Instance.uiManager.partyController.model.SetLocked(false);
                    Core.CoreManager.Instance.uiManager.partyController.SelectorSetSelect(false);

                    Core.CoreManager.Instance.uiManager.partyController.isSwapping = true;
                }
            }
        }
    }
}
