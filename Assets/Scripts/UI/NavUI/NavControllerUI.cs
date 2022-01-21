using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Dropdown;
using UI.Base;
using UI.Enums;
using Core.MessageQueue;
using UI.Handler;
using UI.Party;
using System.Threading.Tasks;

namespace UI.Nav
{
    /// <summary>
    /// NavUI. Repopulates on every call. In the future options may change dynamically so it is required.
    /// </summary>
    public class NavControllerUI : DropdownControllerUI
    {
        /// <summary>
        /// When set to true, we are currently operating.
        /// If off we are not waiting on selection handler
        /// </summary>
        private bool navLocked = false;
        public override void HandlePrintingState()
        {
            base.HandlePrintingState();
        }

        public override void HandleHidingState()
        {
            base.HandleHidingState();
        }

        /// <summary>
        /// Returns the list of the Overworld UI options
        /// </summary>
        /// <returns></returns>
        public void PopulateOverworldDropdown(DropdownTypes type)
        {
            switch (type)
            {
                case DropdownTypes.Empty:
                    EmptyAndHideDropdown();
                    break;
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

        protected override void HandleMessage(string id, FormattedMessage fMsg)
        {
            base.HandleMessage(id, fMsg);
            if (id.Equals(MessageQueueManager.UI_KEY))
            {
                if (fMsg.key.Equals("Party" + PartyControllerUI.OUTPUTKEY))
                {
                    PartyControllerMessageObject partyControllerMessage = JsonUtility.FromJson<PartyControllerMessageObject>(fMsg.message);
                    PopulateOverworldDropdown(partyControllerMessage.dropdownRequest);
                    navLocked = true;
                }              
                else if(fMsg.key.Equals("Party" + SelectionHandler.HANDLERKEY))
                {
                    SelectionHandlerMessageObject message = JsonUtility.FromJson<SelectionHandlerMessageObject>(fMsg.message);
                    if(navLocked && message.state == SelectionState.AllSelected)
                    {
                        EmptyAndHideDropdown();
                        navLocked = false;
                    }
                }
            }
        }
    }
}
