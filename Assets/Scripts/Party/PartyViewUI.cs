using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UI.Base;
using Mon.MonData;
using UI.Enums;
using UI.Nav;

namespace UI.Party
{
    public class PartyViewUI : SelectorViewUI
    {
        protected PartyModelUI partyModel;

        [SerializeField]
        List<PartyElementUI> playerParty = new List<PartyElementUI>();

        protected override void OnEnable()
        {
            PartyModelUI.ModelUpdate += UpdateModel;
            PartyElementUI.PartySelectFire += OnUISelect; 
        }

        protected override void OnDisable()
        {
            PartyModelUI.ModelUpdate -= UpdateModel;
            PartyElementUI.PartySelectFire -= OnUISelect;
        }


        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            partyModel = (PartyModelUI)_model;
        }

        public override void HandlePrintingState()
        {
            base.HandlePrintingState();
            SetElements();
        }

        public override void HandleHidingState()
        {
            base.HandleHidingState();
            for (int i = 0; i < playerParty.Count; i++)
            {
                playerParty[i].DisableElement();
                selectorElementList[i].Dehover();
            }
        }
        private bool CheckValidMon(int monIndex)
        {
            if (partyModel.playerMon[monIndex] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public delegate void OnSelectEvent(List<int> selectedMons);
        public static OnSelectEvent OnSelectFire;

        /// <summary>
        /// When a UI element is selected
        /// </summary>
        private void OnUISelect(string key, int selectableKey)
        {
            if (key.Equals("Party"))
            {
                if (partyModel.selectedMonsList.Contains(selectableKey))
                {
                    Debug.Log("Cant swap... already exists: " + selectableKey);
                    //Should unlock everything
                    selectorModel.SetSelect(false);
                    partyModel.SetLocked(false);
                    return;
                }
                if (partyModel.selectedMonsList.Count == 0)
                {
                    Core.CoreManager.Instance.uiManager.navController.PopulateOverworldDropdown(DropdownTypes.Party);
                }

                partyModel.selectedMonsList.Add(selectableKey);
                //Call delegate
                //Another script to check whether to populate or swap etc.. 

                //Different types of select? How to differentiate
                //1.Swap Select
                //2.Choosing multiple mons for a battle
                //3.


                //Only if Swapping
                if (Core.CoreManager.Instance.uiManager.partyController.isSwapping)
                {
                    //OnSelectFire?.Invoke(partyModel.selectedMonsList);
                    Core.CoreManager.Instance.uiManager.partyController.SwapMonOverworld(partyModel.selectedMonsList[0], partyModel.selectedMonsList[1]);
                    Core.CoreManager.Instance.uiManager.partyController.isSwapping = false;
                    partyModel.selectedMonsList.Clear();
                }
            }
        }

        [ContextMenu("CheckLocked")]
        private void CheckLocked()
        {
            Debug.Log("Active: " + partyModel.Active);
            Debug.Log("Locked: " + partyModel.Locked);
        }

        /// <summary>
        /// Sets all element values
        /// </summary>
        private void SetElements()
        {
            selectorBoundMax = 0;
            for (int i = 0; i < playerParty.Count; i++)
            {
                if (CheckValidMon(i))
                {
                    playerParty[i].DisplayInfo(partyModel.playerMon[i]);
                    EnableElement(playerParty[i]);
                    selectorBoundMax++;
                }
            }
        }

        protected override void RefreshUI()
        {
            base.RefreshUI();
            SetElements();
        }
    }
}
