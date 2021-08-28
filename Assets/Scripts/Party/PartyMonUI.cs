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

        protected void OnEnable()
        {
            PartyUIManager.OnPartyFire += MonInfo;
        }

        protected void OnDisable()
        {
            PartyUIManager.OnPartyFire -= MonInfo;
        }

        public override void Select()
        {
            base.Select();
        }
        private void MonInfo()
        {
            MonIndObj mon = Core.CoreManager.Instance.playerParty.party.GetPartyMember(0);
        }
    }
}
