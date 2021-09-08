using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UnityEngine.UI;
using Core.PartyUI;
using CustomInput;
using UI.Base;
using UI.Selector;

namespace UI.Party
{
    public class PartyElementUI : SelectorElementUI
    {
        [Header("Party Slot Elements")]
        public Text monName;
        public Text monLevel;
        public Text monHealth;
        public Image monHealthBar;
        public GameObject monSprite;

        public delegate void MonSelectEvent();
        public static MonSelectEvent OnMonSelectFire;

        public delegate void MonRecountEvent(string groupKey);
        public static MonRecountEvent MonRecount;
        
        public override void HandlePrintingState()
        {

            if (CheckValidMon(selectableIndex))
            {
                base.HandlePrintingState();
                MonInfo(Core.CoreManager.Instance.playerParty.party.GetPartyMember(selectableIndex));
                ChangeState(UIState.Displaying);
            }
        }

        /// <summary>
        /// Checks for valid mon
        /// If valid continues to printing state
        /// </summary>
        /// <param name="monIndex"></param>
        private bool CheckValidMon(int monIndex)
        {
            if (Core.CoreManager.Instance.playerParty.party.GetPartyMember(monIndex) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets variable based on MonIndObj
        /// </summary>
        /// <param name="monster"></param>
        private void MonInfo(MonIndObj monster)
        {
            monName.text = monster.baseMon.name;
            monHealth.text = $"{monster.battleObj.monStats.hp} / {monster.stats.hp}";
            monLevel.text = monster.stats.level.ToString();
            monHealthBar.fillAmount = (float)monster.battleObj.monStats.hp / monster.stats.hp;
        }
    }
}
