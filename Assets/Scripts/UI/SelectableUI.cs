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
        }

        private void OnDisable()
        {
            SelectorUI.SelectElement -= SelectEvent;
            SelectorUI.CountElement -= CountEvent;
        }

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

        public virtual void Select()
        {
            SelectedElement?.Invoke(groupKey, index);
        }

        public virtual void Deselect()
        {

        }
    
        public void CountEvent(string _groupKey)
        {
            if (groupKey.Equals(_groupKey))
            {
                CountElement?.Invoke(index);
            }
        }
    }
}
