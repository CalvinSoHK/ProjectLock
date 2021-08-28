using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class DropdownElementUI : SelectableUI
    {
        [Header("Dropdown Element Options")]
        [Tooltip("String to display in display text object.")]
        [SerializeField]
        public string displayText;

        [SerializeField]
        [Tooltip("Where to put display text.")]
        private TextMeshProUGUI displayTextObj;

        protected override void Init()
        {
            base.Init();
            displayTextObj.text = displayText;
        }
    }
}
