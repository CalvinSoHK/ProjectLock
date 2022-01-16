using Core.MessageQueue;
using CustomInput;
using Mon.MonData;
using UI.Base;
using UI.Enums;
using UI.Selector;
using UI.Handler;
using UnityEngine;
using UI.Dropdown;

namespace UI.Party
{
    /// <summary>
    /// Controller for Party UI
    /// </summary>
    public class PartyControllerUI : SelectorControllerUI
    {
        private PartyModelUI partyModel;

        /// <summary>
        /// Handles selection events and lets us know when it is complete
        /// </summary>
        private SelectionHandler handler = null;

        /// <summary>
        /// The currently selected dropdown action.
        /// </summary>
        private string dropdownSelect = "";

        public static string OUTPUTKEY = "/PARTYCONTROLLER";

        public override void HandleOffState()
        {
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld
                && Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Party, InputEnums.InputAction.Down)
                )
            {
                Core.CoreManager.Instance.player.DisableInputMovement();
                ChangeState(UIState.Printing);
            }
        }

        public override void HandlePrintingState()
        {
            //Refreshes because it needs to grab mon info and display.
            //Without refresh it will not show.
            handler = new SelectionHandler("UI", "Party", 1);
            Refresh();
            base.HandlePrintingState();
        }

        public override void HandleHidingState()
        {
            base.HandleHidingState();
            Core.CoreManager.Instance.player.EnableInputMovement();
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

        public void SwapMonOverworld(int _selectedIndex, int newMonIndex)
        {
            if (_selectedIndex != newMonIndex)
            {
                //Debug.Log($"Swapping {_selectedIndex} with {newMonIndex}");
                Core.CoreManager.Instance.playerParty.party.SwapMembers(_selectedIndex, newMonIndex);
                partyModel.SetSelect(false);
                Refresh(); //IMPORTANT
                partyModel.SetLocked(false);
                //firstIteration = true;
            } 
            else
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
            for (int i = 0; i < partyModel.playerMon.Length; i++)
            {
                MonInfoOverworld(i);
            }
            base.Refresh();
        }

        private void OnReturnKey()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down))
            {
                //Base case: If we have no selected indexes
                if (handler.selectedIndexes.Count == 0)
                {
                    ChangeState(UIState.Hiding);
                }
                else
                {
                    //Remove the latest selected index (we know it is at least 1)
                    handler.RemoveLatest();
                    
                    //If the count is now 0, we are allowing a new selection in the party screen
                    //We also need to disable the dropdown
                    //TODO: Handle when we are backing out of a selection in party controller
                    if(handler.selectedIndexes.Count == 0)
                    {
                        //Reset dropdown select  
                        ResetSelectionHandler();
                    }
                    //If the count is now 1 or more, we are still picking for some option
                    else
                    {

                    }
                }
            }
        }
        
        /// <summary>
        /// Resets selection handler to initial settings
        /// </summary>
        private void ResetSelectionHandler()
        {
            dropdownSelect = "";
            handler.selectedIndexes.Clear();
            handler.SetRequired(1);
        }

        //Resets the selector
        private void ResetSelector()
        {
            model.SetLocked(false);
            partyModel.SetSelect(false);
            partyModel.UnselectAll();
            Refresh();
        }

        /// <summary>
        /// When a UI element is selected
        /// </summary>
        protected override void HandleMessage(string id, FormattedMessage fMsg)
        {
            base.HandleMessage(id, fMsg);  
            if (id == "UI")
            {
                if (fMsg.key.Equals("Navigation"))
                {
                    if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
                    {
                        Dropdown.DropdownMessageObject message = JsonUtility.FromJson<Dropdown.DropdownMessageObject>(fMsg.message);

                        //Record selected dropdown option
                        dropdownSelect = message.dropdownKey;
                        
                        //If it was swap
                        if (message.dropdownKey.Equals("Swap"))
                        {
                            //Change required amount to 2 and allow a new selection
                            handler.SetRequired(2);
                            ResetSelector();
                        }
                    }
                }
                else if (fMsg.key.Equals(key + SelectionHandler.HANDLERKEY))
                {
                    SelectionHandlerMessageObject message = JsonUtility.FromJson<SelectionHandlerMessageObject>(fMsg.message);

                    switch (message.state)
                    {
                        case SelectionState.SelectFail:
                            ResetSelector();
                            return;
                        case SelectionState.SelectSuccess:
                            partyModel.selectedMonsList.Add(message.selectedIndexes[message.selectedIndexes.Count - 1]);
                            return;
                        case SelectionState.AllSelected:
                            //If selection count is 1, we just did the initial select, make the dropdown
                            if (message.selectedIndexes.Count == 1)
                            {
                                Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
                                    "UI",
                                    key + OUTPUTKEY,
                                    JsonUtility.ToJson(new PartyControllerMessageObject(DropdownTypes.Party))
                                    );    
                            }
                            else if (dropdownSelect.Equals("Swap") && message.selectedIndexes.Count == 2)
                            {
                                SwapMonOverworld(message.selectedIndexes[0], message.selectedIndexes[1]);
                                partyModel.selectedMonsList.Clear();
                                partyModel.UnselectAll();
                                ResetSelectionHandler();
                            }
                            return;
                    }
                }
            }
        }
    }
}
