using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    /// <summary>
    /// Data Transfer Object that represents info needed to make a individual dropdown selectable.
    /// </summary>
    public class DropdownElementDTO
    {
        private string elementText;

        /// <summary>
        /// Text to display on this element
        /// </summary>
        public string ElementText
        {
            get
            {
                return elementText;
            }
        }

        private UnityAction onElementSelect;

        /// <summary>
        /// UnityEvent to fire when this element is selected.
        /// </summary>
        public UnityAction OnElementSelect
        {
            get
            {
                return onElementSelect;
            }
        }

        private string key;

        /// <summary>
        /// The key that corresponds to this element being clicked
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
        }

        /// <summary>
        /// Creates a DTO for an individual selectable dropdown element
        /// </summary>
        /// <param name="_elementText"> Text to display for element </param>
        /// <param name="_onElementSelect"> Event to fire off when element is selected</param>
        public DropdownElementDTO(string _elementText, UnityAction _onElementSelect, string _key)
        {
            elementText = _elementText;
            onElementSelect = _onElementSelect;
            key = _key;
        }
    }
}