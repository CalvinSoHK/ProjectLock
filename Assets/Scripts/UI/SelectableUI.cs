using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectableUI : BaseUI
    {
        [Tooltip("Index of this selectable component.")]
        [SerializeField]
        private int index = -1;

        [Tooltip("Key of the group of selectable components.")]
        [SerializeField]
        private string groupKey = "";

        [Tooltip("Select marker is enabled and disabled when this is selected")]
        [SerializeField]
        private GameObject selectMarker;

        public delegate void SelectElement(string groupKey, int index);

        /// <summary>
        /// Fired when an element is clicked on or selected.
        /// </summary>
        public static SelectElement SelectedElement;

        public delegate void CountElementEvent(int index);
        public static CountElementEvent CountElement;

        /// <summary>
        /// Sets the index of a selectableUI component.
        /// Only use for dynamic UIs
        /// </summary>
        /// <param name="_index"></param>
        public void SetIndex(int _index)
        {
            index = _index;
        }

        private void OnEnable()
        {
            SelectorUI.SelectElement += SelectEvent;
            SelectorUI.CountElement += CountEvent;
            SelectorUI.HoverElement += HoverEvent;
        }

        private void OnDisable()
        {
            SelectorUI.SelectElement -= SelectEvent;
            SelectorUI.CountElement -= CountEvent;
            SelectorUI.HoverElement -= HoverEvent;
        }

        /// <summary>
        /// Checks to see if this element's group is having hover state changes.
        /// Then either hovers or dehovers the element.
        /// </summary>
        /// <param name="_groupKey"></param>
        /// <param name="_index"></param>
        public void HoverEvent(string _groupKey, int _index)
        {
            //Only process if the group key is the same
            if (groupKey.Equals(_groupKey))
            {
                if (index == _index)
                {
                    Hover();
                }
                else
                {
                    Dehover();
                }
            }
        }

        /// <summary>
        /// Hovers this element.
        /// </summary>
        public virtual void Hover()
        {
            selectMarker.SetActive(true);
        }

        /// <summary>
        /// Dehovers this element.
        /// </summary>
        public virtual void Dehover()
        {
            selectMarker.SetActive(false);
        }

        /// <summary>
        /// Checks to see if this element is in the relevant group
        /// If it is, check if it was selected or deselected.
        /// </summary>
        /// <param name="_groupKey"></param>
        /// <param name="_index"></param>
        public void SelectEvent(string _groupKey, int _index)
        {
            //Only process if the group key is the same
            if (groupKey.Equals(_groupKey))
            {
                if(index == _index)
                {
                    Select();
                }
                else
                {
                    Deselect();
                }
            }
        }

        /// <summary>
        /// Function that fires when this element is selected
        /// </summary>
        public virtual void Select()
        {
            
        }

        /// <summary>
        /// Funcino that fires when this element is deselected.
        /// </summary>
        public virtual void Deselect()
        {
            
        }
    
        /// <summary>
        /// Propagates count event if group key matches
        /// </summary>
        /// <param name="_groupKey"></param>
        public void CountEvent(string _groupKey)
        {
            if (groupKey.Equals(_groupKey))
            {
                CountElement?.Invoke(index);
            }
        }
    }
}
