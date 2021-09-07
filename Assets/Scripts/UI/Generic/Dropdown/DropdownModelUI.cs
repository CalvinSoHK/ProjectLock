using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;

namespace UI.Dropdown
{
    public class DropdownModelUI : SelectorModelUI
    {
        private bool dropdownUpdate = false;
        /// <summary>
        /// Whether or not the dropdown was updated
        /// </summary>
        public bool DropdownUpdate
        {
            get
            {
                return dropdownUpdate;
            }
        }

        public void SetDropdownUpdate(bool _state)
        {
            dropdownUpdate = _state;
        }

        private DropdownDTO dropdownDTO;
        public DropdownDTO DropdownDTO
        {
            get
            {
                return dropdownDTO;
            }
        }

        public void SetDropdownDTO(DropdownDTO _dto)
        {
            dropdownDTO = _dto;
        }

        public delegate void DropdownModel(string key, DropdownModelUI model);
        public static new DropdownModel ModelUpdate;

        public override void InvokeModel(string _key)
        {
            ModelUpdate?.Invoke(_key, this);
        }
    }
}
