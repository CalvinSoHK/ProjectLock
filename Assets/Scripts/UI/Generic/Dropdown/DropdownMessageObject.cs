using System.Collections;
using System.Collections.Generic;
using UI.Selector;
using UnityEngine;

namespace UI.Dropdown
{
    public class DropdownMessageObject : SelectorMessageObject
    {
        public string dropdownKey;

        public DropdownMessageObject(int _index, string _dropdownKey) : base(_index)
        {
            dropdownKey = _dropdownKey;
        }
    }
}
