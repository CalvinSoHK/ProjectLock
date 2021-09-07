using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UI.Selector;

namespace UI.Dropdown
{
    public class DropdownElementUI : SelectorElementUI
    {
        [Header("Dropdown Element Options")]
        [Tooltip("String to display in display text object.")]
        [SerializeField]
        public string displayText;

        [SerializeField]
        [Tooltip("Where to put display text.")]
        private TextMeshProUGUI displayTextObj;

        public override void Init()
        {
            base.Init();
            displayTextObj.text = displayText;
        }
    }
}
