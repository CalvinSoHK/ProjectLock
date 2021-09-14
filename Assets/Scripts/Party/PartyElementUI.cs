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

        public override void EnableElement()
        {
            base.EnableElement();
        }

        public override void DisableElement()
        {
            base.DisableElement();
        }

    }
}
