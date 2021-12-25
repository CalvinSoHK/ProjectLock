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


        public delegate void PartySelectEvent(string key, int selectableKey);
        public static PartySelectEvent PartySelectFire;

        public override void Select()
        {
            base.Select();
            PartySelectFire?.Invoke("Party",selectableIndex);
        }

        /// <summary>
        /// Sets variable based on MonIndObj
        /// </summary>
        /// <param name="monster"></param>
        public void DisplayInfo(MonIndObj monster)
        {
            monName.text = monster.baseMon.name;
            monHealth.text = $"{monster.battleObj.monStats.hp} / {monster.stats.hp}";
            monLevel.text = monster.stats.level.ToString();
            monHealthBar.fillAmount = (float)monster.battleObj.monStats.hp / monster.stats.hp;
        }
    }
}
