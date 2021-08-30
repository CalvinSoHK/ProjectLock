using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CategoryUI : DropdownUI
    {
        [SerializeField]
        private string targetGroupKey, targetKey;

        protected override void OnEnable()
        {
            base.OnEnable();
            SelectableUI.SelectableSelectFire += EnableUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SelectableUI.SelectableSelectFire -= EnableUI;
        }

        /// <summary>
        /// Returns the list of the Overworld UI options
        /// </summary>
        /// <returns></returns>
        private List<DropdownElementDTO> GetOverworldList()
        {
            List<string> optionKeys = new List<string>();
            optionKeys.Add("Key");
            optionKeys.Add("Items");
            optionKeys.Add("Capture");
            return CreateDefaultOptions(optionKeys);
        }

        private void EnableUI(string _groupKey, string _key)
        {
            if(targetGroupKey.Equals(_groupKey) && targetKey.Equals(_key))
            {
                if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
                {
                    PopulateDropdown(new DropdownDTO(groupKey, GetOverworldList()));
                    ChangeState(UIState.Printing);
                }
            }
        }
    }
}
