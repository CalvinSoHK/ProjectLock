using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Selector;
using UI.Base;
using UI.Dropdown;
using Mon.MonData;

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
            StartIndexTimer();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
            {
                for (int i = 0; i < partyModel.playerMon.Length; i++)
                {
                    MonInfoOverworld(i);
                }         
            } 
            base.HandlePrintingState();
        }

        public override void HandleDisplayState()
        {
            if (!model.Locked)
            {
                IndexControl();
                OnReturnKey();
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
        private void MonInfoOverworld(int monNumber)
        {
            partyModel.playerMon[monNumber] = Core.CoreManager.Instance.playerParty.party.GetPartyMember(monNumber);
        }

        public void MonInfoBattle(int monNumber, MonIndObj playerMon)
        {
            partyModel.playerMon[monNumber] = playerMon;
        }

        public void SaveIndex(int _selectedIndex)
        {
            if (savedSelectedIndex < 0)
            {
                savedSelectedIndex = _selectedIndex;
            }
        }

        public void SwapMonOverworld(int _selectedIndex, int newMonIndex)
        {
            if (_selectedIndex != newMonIndex && savedSelectedIndex != -1)
            {
                Debug.Log($"Swapping {_selectedIndex} with {newMonIndex}");
                Core.CoreManager.Instance.playerParty.party.SwapMembers(_selectedIndex, newMonIndex);
                savedSelectedIndex = -1;
                SelectorSetSelect(false);
                Refresh();
                partyModel.SetLocked(false);
                firstIteration = true;
            } else
            {
                Debug.Log($"Not swappable: {_selectedIndex} and {newMonIndex}");
                selectorModel.SetSelect(false);
                partyModel.SetLocked(false);
            }
        }

        public void SelectorSetSelect(bool setSelect)
        {
            selectorModel.SetSelect(setSelect);
        }
        
        protected override void Refresh()
        {
            ChangeState(UIState.Printing);
            partyModel.InvokeModel(key);
            
        }

        private void OnReturnKey()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down))
            {
                if (!firstIteration)
                {
                    //Reopen dropdown?
                    //is Not Locked (Swapped has been pressed already)
                    if (!model.Locked)
                    {
                        firstIteration = true;
                    }
                }
                else
                {
                    ChangeState(UIState.Hiding);
                }
            }
        }
    }
}
