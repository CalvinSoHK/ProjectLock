using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dropdown;
using Core.MessageQueue;

namespace UI.Nav
{
    public class NavViewUI : DropdownViewUI
    {

        protected override void OnEnable()
        {
            base.OnEnable();
            MessageQueue.MessageEvent += HandleMessage;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MessageQueue.MessageEvent -= HandleMessage;
        }

        private void HandleMessage(string _id, string _msg)
        {
            if (_id.Equals("UI"))
            {
                //Hide Dropdown Needs change. Button still appears and works despite DisableState()
                //dropdownController.DisableState();
                if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
                {
                    if (_msg.Equals("Swap"))
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

        //One delegate from partyViewUI --> populates dropdown in NavViewUI

        /// <summary>
        /// temporary
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_optionKey"></param>
        private void OnDropdownPress(string _key, string _optionKey)
        {
           
        }
    }
}
