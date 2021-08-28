using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [RequireComponent(typeof(GUILayout))]
    /// <summary>
    /// Strictor selector UI which populates the drop down on spawn
    /// </summary>
    public class DropdownUI : SelectorUI
    {
        [Header("Dropdown Options")]
        [Tooltip("Prefab for selectable in this dropdown")]
        [SerializeField]
        DropdownElementUI elementPrefab;

        /// <summary>
        /// Populates this DropdownUI based on the info passed to it
        /// </summary>
        /// <param name="dto"></param>
        public void PopulateDropdown(DropdownDTO dropdownDTO)
        {
            int index = 0;
            foreach(DropdownElementDTO elementDTO in dropdownDTO.ElementList)
            {
                DropdownElementUI element = Instantiate(elementPrefab, transform);
                element.SetIndex(index);
                element.SetGroupKey(groupKey);
                element.displayText = elementDTO.ElementText;
                element.key = elementDTO.Key;
                element.OnSelect.AddListener(elementDTO.OnElementSelect);
                index++;
            }
            CountSelectables();
            Reset();
        }
    }
}
