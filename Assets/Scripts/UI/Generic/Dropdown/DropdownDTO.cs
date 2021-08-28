using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Data Transfer Object that represents info needed ot make a dropdown selector menu
    /// </summary>
    public class DropdownDTO
    {
        private string groupKey;

        /// <summary>
        /// The group key all elements will have assigned to them
        /// </summary>
        public string GroupKey
        {
            get
            {
                return groupKey;
            }
        }

        private List<DropdownElementDTO> elementList = new List<DropdownElementDTO>();

        /// <summary>
        /// Gives an element list that contains all the info for each dropdown selectable
        /// </summary>
        public List<DropdownElementDTO> ElementList
        {
            get
            {
                return elementList;
            }
        }

        /// <summary>
        /// Creates a drop down DTO.
        /// </summary>
        /// <param name="_groupKey"> Group key for whole dropdown </param>
        /// <param name="_selectableType"> SelectableUI script to use</param>
        /// <param name="_elementList"> List of dropdown element DTOs to read from</param>
        public DropdownDTO(string _groupKey, List<DropdownElementDTO> _elementList)
        {
            groupKey = _groupKey;
            elementList = _elementList;
        }
    }
}