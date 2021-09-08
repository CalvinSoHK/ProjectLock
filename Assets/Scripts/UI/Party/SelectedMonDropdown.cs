using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Dropdown;
using UI.Base;

namespace UI.Party
{
    public class SelectedMonDropdown : DropdownElementUI
    {
        bool isPrinted = false;

        protected override void SubscribeListeners()
        {
            base.SubscribeListeners();
            //PartyMonUI.OnMonSelectFire += Handler;
        }

        protected override void UnsubscribeListeners()
        {
            base.UnsubscribeListeners();
            //PartyMonUI.OnMonSelectFire -= Handler;
        }

        public override void HandlePrintingState()
        {
            base.HandlePrintingState();
            isPrinted = true;
        }

        public override void HandleDisplayState()
        {
            base.HandleDisplayState();
            if (Input.GetKeyDown(KeyCode.K)) 
            {
                ExitMenuPressed();
            }
        }
        public override void HandleOffState()
        {
            base.HandleOffState();
            isPrinted = false;
        }

        /// <summary>
        /// Turns off state
        /// Only if List has been DroppedDown
        /// </summary>
        private void ExitMenuPressed()
        {
            if (isPrinted)
            {
                ChangeState(UIState.Off);
            }
        }

        /// <summary>
        /// Selects DropdownElementDTO based on the current worldState
        /// </summary>
        private void Handler(int _savedIndex)
        {
            if(!isPrinted)
            { 
                switch (Core.CoreManager.Instance.worldStateManager.State)
                {
                    case Core.WorldState.Overworld:
                        //PopulateDropdown(new DropdownDTO(groupKey, OverworldParty()));
                        break;
                    case Core.WorldState.Battle:
                        //BattleParty();
                        break;
                }
                ChangeState(UIState.Printing);
            }
        }

        /*
        /// <summary>
        /// Returns the list of the Overworld Party UI options
        /// </summary>
        private List<DropdownElementDTO> OverworldParty()
        {
            List<string> dropdownList = new List<string>();
            dropdownList.Add("Details");
            dropdownList.Add("Swap");
            dropdownList.Add("Item");

            
            //return CreateDefaultOptions(dropdownList);
        }*/

    }

}
