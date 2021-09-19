using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Selector;
using UI.Base;
using UI.Dropdown;

namespace UI.Party
{
    /// <summary>
    /// Controller for Party UI
    /// </summary>
    public class PartyControllerUI : SelectorControllerUI
    {

        private PartyModelUI partyModel;

        public int savedSelectedIndex = -1;
        public bool firstIteration = true;
        public override void HandleOffState()
        {
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld
                && Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Party, InputEnums.InputAction.Down)
                )
            {
                ChangeState(UIState.Printing);
            }
        }

        public override void HandlePrintingState()
        {
            //Setting data in Model
            for (int i = 0; i < partyModel.playerMon.Length; i++)
            {
                MonInfo(i);
            }
            base.HandlePrintingState();
        }

        public override void HandleDisplayState()
        {
            if (!model.Locked)
            {
                base.HandleDisplayState();
            }
        }

        public override void Init()
        {
            if (!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }

            model = new PartyModelUI();
            partyModel = (PartyModelUI)model;
            selectorModel = (SelectorModelUI)model;


            model.Init();
        }


        //Maybe move somewhere in future?
        /// <summary>
        /// Gives MonIndObj player party member from index
        /// </summary>
        /// <param name="monNumber"></param>
        private void MonInfo(int monNumber)
        {
            partyModel.playerMon[monNumber] = Core.CoreManager.Instance.playerParty.party.GetPartyMember(monNumber);
        }


        public void SaveIndex(int _selectedIndex)
        {
            if (savedSelectedIndex < 0)
            {
                savedSelectedIndex = _selectedIndex;
            }
        }

        public void SwapMon(int _selectedIndex, int newMonIndex)
        {
            if (_selectedIndex != newMonIndex && savedSelectedIndex != -1)
            {
                Debug.Log($"Swapping {_selectedIndex} with {newMonIndex}");
                Core.CoreManager.Instance.playerParty.party.SwapMembers(_selectedIndex, newMonIndex);
                savedSelectedIndex = -1;
                selectorModel.SetSelect(false);
                partyModel.InvokeModel(key);
                partyModel.SetLocked(false);
                //Update UI here? Refresh?
            } else
            {
                Debug.Log($"Not swappable: {_selectedIndex} and {newMonIndex}");
            }
        }

        public void SelectorSetSelect(bool setSelect)
        {
            selectorModel.SetSelect(setSelect);
        }
    }
}
