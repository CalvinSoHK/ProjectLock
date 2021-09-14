using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using Core.Player;
using Mon.MonData;
using UnityEngine.UI;

namespace UI.Party
{
    /// <summary>
    /// Model for party UI
    /// </summary>
    public class PartyModelUI : SelectorModelUI
    {
        //Be able to reference mons

        //List determined by PartySize
        //Within each list

        MonIndObj[] _playerMon = new MonIndObj[Core.CoreManager.Instance.playerParty.party.PartySize];

        public MonIndObj[] playerMon
        {
            get
            {
                return _playerMon;
            }
            set
            {
                _playerMon = value;
            }
        }



        public delegate void PartyModel(string key, PartyModelUI model);
        public static new PartyModel ModelUpdate;

        public override void InvokeModel(string _key)
        {
            ModelUpdate?.Invoke(_key, this);
        }
    }
}
