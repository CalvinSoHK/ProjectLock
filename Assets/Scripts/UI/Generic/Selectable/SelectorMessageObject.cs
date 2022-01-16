using System.Collections;
using System.Collections.Generic;
using UI.Message;
using UnityEngine;

namespace UI.Selector
{
    public class SelectorMessageObject : MessageObject
    {
        public int index;

        public SelectorMessageObject(int _index) : base()
        {
            index = _index;
        }
    }
}
