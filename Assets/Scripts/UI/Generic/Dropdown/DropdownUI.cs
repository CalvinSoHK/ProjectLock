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

        public delegate void DropdownOptionEvent(string groupKey, string key);
        public static DropdownOptionEvent DropdownOptionFire;

        /// <summary>
        /// Populates this DropdownUI based on the info passed to it
        /// </summary>
        /// <param name="dto"></param>
        public void PopulateDropdown(DropdownDTO dropdownDTO)
        {
            EmptyChildren();
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
            Init();
        }

        /// <summary>
        /// Empties children so we can repopulate the menu
        /// </summary>
        protected virtual void EmptyChildren()
        {
            for(int i = transform.childCount - 1; i > -1 ; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Makes a list of dropdown element DTOs from a list of options.
        /// These options will invoke default callbacks.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected List<DropdownElementDTO> CreateDefaultOptions(List<string> options)
        {
            List<DropdownElementDTO> dropdownList = new List<DropdownElementDTO>();
            foreach (string key in options)
            {
                dropdownList.Add(new DropdownElementDTO(
                    key,
                    new UnityAction(() => DropdownOptionFire?.Invoke(groupKey, key)),
                    key));
            }
            return dropdownList;
        }
    }
}
