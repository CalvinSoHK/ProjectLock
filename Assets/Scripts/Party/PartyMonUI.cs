using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UnityEngine.UI;
using Core.PartyUI;

namespace UI
{
    public class PartyMonUI : SelectableUI
    {
        [Header("Party Slot Elements")]
        public Text monName;
        public Text monLevel;
        public Text monHealth;
        public GameObject monHealthBar;
        public GameObject monSprite;

        MonIndObj monster;
        protected void OnEnable()
        {
            PartyUIManager.OnPartyFire += OnPartyUIFire;
            //SelectableUI.FireSelect += MonInfo;
        }

        protected void OnDisable()
        {
            PartyUIManager.OnPartyFire -= OnPartyUIFire;
            //SelectableUI.FireSelect += M
        }

        protected override void HandlePrintingState()
        {
            base.HandlePrintingState();
            MonInfo(Core.CoreManager.Instance.playerParty.party.GetPartyMember(index));
            Debug.Log("Yep");
            ChangeState(UIState.Printing);
        }

        protected override void Select()
        {
            base.Select();
        }

        private void OnPartyUIFire()
        {
            GetMon(index);
        }

        private void GetMon(int monIndex)
        {
           if (Core.CoreManager.Instance.playerParty.party.GetPartyMember(monIndex) != null)
            {
                ChangeState(UIState.Printing);
            }
            else
            {

            }
        }
        private void MonInfo(MonIndObj monster)
        {
            monName.text = monster.baseMon.name;
            monHealth.text = $"{monster.battleObj.monStats.hp} / {monster.stats.hp}";
            monLevel.text = monster.stats.level.ToString();
        }

    }
}
