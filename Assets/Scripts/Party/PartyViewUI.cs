using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UI.Base;
using Mon.MonData;

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

        private void UpdateModel(string _key, Model _model)
        {
            if (_key.Equals(controllerKey))
            {
                UpdateView(_model);
            }
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
