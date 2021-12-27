using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dropdown;

namespace UI.Nav
{
    public class NavViewUI : DropdownViewUI
    {

        private void OnEnable()
        {
            DropdownControllerUI.DropdownOptionFire += OnDropdownPress;
        }

        private void OnDisable()
        {
            DropdownControllerUI.DropdownOptionFire -= OnDropdownPress;
        }

        //One delegate from partyViewUI --> populates dropdown in NavViewUI

        /// <summary>
        /// temporary
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_optionKey"></param>
        private void OnDropdownPress(string _key, string _optionKey)
        {
            //Hide Dropdown Needs change. Button still appears and works despite DisableState()
            //dropdownController.DisableState();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
            {
                if (_optionKey == "Swap")
                {
                    int selectedIndex = Core.CoreManager.Instance.uiManager.partyController.ReturnCurrentSelected();
                    //Currently Swap with 1
                    //new index 
                    int newSelectedIndex = 1;
                    Core.CoreManager.Instance.uiManager.partyController.SwapMonOverworld(selectedIndex, newSelectedIndex);
                }
            }
        }
    }
}
