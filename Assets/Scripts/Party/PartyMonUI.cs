using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UnityEngine.UI;

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
    }
}
