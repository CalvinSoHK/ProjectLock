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
            selectorBoundMax = 0;
            for (int i = 0; i < playerParty.Count; i++)
            {                
                if (CheckValidMon(i))
                {
                    SetElements(i);
                    selectorBoundMax++;
                }
            }
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
        /// Sets the element depending on the index
        /// </summary>
        /// <param name="monNumber"></param>
        void SetElements(int monNumber)
        {
            DisplayInfo(playerParty[monNumber], partyModel.playerMon[monNumber]);
            playerParty[monNumber].EnableElement();
        }

        /// <summary>
        /// Sets variable based on MonIndObj
        /// </summary>
        /// <param name="monster"></param>
        private void DisplayInfo(PartyElementUI monNumber, MonIndObj monster)
        {
            monNumber.monName.text = monster.baseMon.name;
            monNumber.monHealth.text = $"{monster.battleObj.monStats.hp} / {monster.stats.hp}";
            monNumber.monLevel.text = monster.stats.level.ToString();
            monNumber.monHealthBar.fillAmount = (float)monster.battleObj.monStats.hp / monster.stats.hp;
        }

        [ContextMenu("CheckLocked")]
        private void CheckLocked()
        {
            Debug.Log("Active: " + partyModel.Active);
            Debug.Log("Locked: " + partyModel.Locked);
        }
    }
}
