using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UI.Base;
using Mon.MonData;
using UI.Enums;
using UI.Nav;
using Core.MessageQueue;

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
        }

        protected override void OnDisable()
        {
            PartyModelUI.ModelUpdate -= UpdateModel;
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
