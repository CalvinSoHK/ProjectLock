using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Dropdown;
using UI.Base;

namespace UI
{
    public class CategoryUI : DropdownControllerUI
    {
        [SerializeField]
        private string targetGroupKey, targetKey;

        /// <summary>
        /// Returns the list of the Overworld UI options
        /// </summary>
        /// <returns></returns>
        private void UseDefaultOverworldList()
        {
            List<string> optionKeys = new List<string>();
            optionKeys.Add("Key");
            optionKeys.Add("Items");
            optionKeys.Add("Capture");
            MakeOrReplaceDropdown(optionKeys);
        }

        private void EnableUI(string _selectorKey, string selectableKey)
        {
            if(targetGroupKey.Equals(_selectorKey) && targetKey.Equals(selectableKey))
            {
                if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
                {
                    UseDefaultOverworldList();
                    ChangeState(UIState.Printing);
                }
            }
        }
    }
}
