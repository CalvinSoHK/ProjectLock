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
        private string realGroupKey;

        public delegate void MonSelectEvent();
        public static MonSelectEvent OnMonSelectFire;
        public static MonSelectEvent MonRecount;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            PartyUI.OnPartyReadyFire += OnPartyUIFire;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PartyUI.OnPartyReadyFire -= OnPartyUIFire;
        }

        protected override void HandlePrintingState()
        {
            if (CheckValidMon(index))
            {
                ActivateKey();
                base.HandlePrintingState();
                MonInfo(Core.CoreManager.Instance.playerParty.party.GetPartyMember(index));
                ChangeState(UIState.Displaying);
            }
            else
            {
                DeactivateKey();
            }
        }

        protected override void Init()
        {
            base.Init();
            realGroupKey = groupKey;
        }

        protected override void Select()
        {
            base.Select();
            Debug.Log(index);
        }

        private void OnPartyUIFire()
        {
            //CheckValidMon(index);
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
        }
        
        /// <summary>
        /// Sets groupKey to newKey
        /// </summary>
        /// <param name="newKey"></param>
        /// <returns></returns>
        private bool ChangeGroupKey(string newKey)
        {
            if (!groupKey.Equals(newKey))
            {
                groupKey = newKey;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets groupKey to realGroupKey and Invokes a recount
        /// </summary>
        private void ActivateKey()
        {
            if (ChangeGroupKey(realGroupKey))
            {
                MonRecount?.Invoke();
            }
        }

        /// <summary>
        /// Sets groupKey to empty and Invokes a recount
        /// </summary>
        private void DeactivateKey()
        {
            if (ChangeGroupKey(""))
            {
                MonRecount?.Invoke();
            }
        }


    }
}
