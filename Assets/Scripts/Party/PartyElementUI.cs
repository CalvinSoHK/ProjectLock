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
            Debug.Log("Selected", this);
            PartySelectFire?.Invoke("Party",selectableIndex);
        }
    }
}
