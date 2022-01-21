using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UnityEngine.UI;
using CustomInput;
using UI.Base;
using UI.Selector;
using Core;
using Core.MessageQueue;

namespace UI.Party
{
    public class PartyElementUI : SelectorElementUI
    {

        [Header("Party Slot Elements")]
        [SerializeField]
        private Text monName;
        [SerializeField]
        private Text monLevel;
        [SerializeField]
        private Text monHealth;
        [SerializeField]
        private Image monHealthBar;
        [SerializeField]
        private GameObject monSprite;
        public MonIndObj setMon;

        public override void Select()
        {
            if (!selected)
            {
                CoreManager.Instance.messageQueueManager.TryQueueMessage(MessageQueueManager.UI_KEY, key, JsonUtility.ToJson(new PartyMessageObject(SelectableIndex, setMon)));
            }
            base.Select();      
        }

        public override void Deselect()
        {
            base.Deselect();
        }

        /// <summary>
        /// Sets variable based on MonIndObj
        /// </summary>
        /// <param name="monster"></param>
        public void DisplayInfo(MonIndObj monster)
        {
            setMon = monster;
            monName.text = setMon.baseMon.name;
            monHealth.text = $"{setMon.battleObj.monStats.hp} / {setMon.stats.hp}";
            monLevel.text = setMon.stats.level.ToString();
            monHealthBar.fillAmount = (float)setMon.battleObj.monStats.hp / setMon.stats.hp;
        }
    }
}
