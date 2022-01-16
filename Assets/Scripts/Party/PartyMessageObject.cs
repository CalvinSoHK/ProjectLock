using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UI.Selector;
using UnityEngine;

namespace UI.Party
{
    public class PartyMessageObject : SelectorMessageObject
    {
        public MonIndObj mon;
        public PartyMessageObject(int _index, MonIndObj _mon) : base(_index)
        {
            mon = _mon;
        }
    }
}
