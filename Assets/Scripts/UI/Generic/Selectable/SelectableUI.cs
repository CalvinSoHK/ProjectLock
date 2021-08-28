using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SelectableUI : BasePointerUI
    {
        [Header("Selectable Options")]
        [Tooltip("Index of this selectable component.")]
        [SerializeField]
        protected int index = -1;

        [Tooltip("Key of the group of selectable components.")]
        [SerializeField]
        protected string groupKey = "";

        [Tooltip("Select marker is enabled and disabled when this is selected")]
        [SerializeField]
        private GameObject selectMarker;

        [Tooltip("Unity event that fires when the element is selected.")]
        [SerializeField]
        public UnityEvent OnSelect;

        [Tooltip("Element key that is used to fire being selected")]
        [SerializeField]
        public string key;


        public delegate void SelectElement(string groupKey, int index);

        /// <summary>
        /// Fired when an element is clicked on.
        /// Used for propagating information around.
        /// Is not the delegate to subscribe to if looking for when his is selected.
        /// </summary>
        public static SelectElement SelectableSelected;

        /// <summary>
        /// Fired to count this element in the selector UI.
        /// </summary>
        public static SelectElement SelectableCount;

        public delegate void FireSelect(string key);
        /// <summary>
        /// Fires the selectable select.
        /// </summary>
        public static FireSelect SelectableSelectFire;

        /// <summary>
        /// Ignore list used for setuiactive
        /// </summary>
        private List<GameObject> ignoreList = new List<GameObject>();

        private void OnEnable()
        {
            SelectorUI.SelectorSelect += SelectEvent;
            SelectorUI.SelectorCount += CountEvent;
            SelectorUI.SelectorHover += HoverEvent;
            SelectorUI.SelectorUIActive += ControlState;
            SelectableSelected += DeselectClick;
        }

        private void OnDisable()
        {
            SelectorUI.SelectorSelect -= SelectEvent;
            SelectorUI.SelectorCount -= CountEvent;
            SelectorUI.SelectorHover -= HoverEvent;
            SelectorUI.SelectorUIActive -= ControlState;
            SelectableSelected -= DeselectClick;
        }

        /// <summary>
        /// Sets the index of this selectable UI.
        /// </summary>
        /// <param name="_index"></param>
        public void SetIndex(int _index)
        {
            index = _index;
        }

        /// <summary>
        /// Sets the groupkey of this selectable UI.
        /// </summary>
        /// <param name="_groupKey"></param>
        public void SetGroupKey(string _groupKey)
        {
            groupKey = _groupKey;
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
            OnPointerEnterEvent?.Invoke();
        }

        /// <summary>
        /// Dehovers this element.
        /// </summary>
        public virtual void Dehover()
        {
            selectMarker.SetActive(false);
            OnPointerExitEvent?.Invoke();
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
                if (index == _index)
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
        /// Select by clicking
        /// </summary>
        public virtual void SelectClick()
        {
            SelectableSelected?.Invoke(groupKey, index);
        }

        /// <summary>
        /// Function that listens for SelectedElement and Deselects itself
        /// </summary>
        /// <param name="_groupKey"></param>
        /// <param name="_index"></param>
        protected virtual void DeselectClick(string _groupKey, int _index)
        {
            if(groupKey.Equals(_groupKey) && index != _index)
            {
                Deselect();
            }
        }

        /// <summary>
        /// Function that fires when this element is selected
        /// </summary>
        protected virtual void Select()
        {
            Hover();
            SelectableSelectFire?.Invoke(key);
            Debug.Log("Clicked on : " + key);
        }

        /// <summary>
        /// Funcino that fires when this element is deselected.
        /// </summary>
        protected virtual void Deselect()
        {
            Dehover();
        }

        /// <summary>
        /// Controls state of selectable
        /// </summary>
        /// <param name="_groupKey"></param>
        /// <param name="_active"></param>
        private void ControlState(string _groupKey, bool _active)
        {
            if (_groupKey.Equals(groupKey))
            {
                if(state == UIState.Off && _active)
                {
                    ChangeState(UIState.Printing);
                }
                else if(state == UIState.Displaying && !_active)
                {
                    ChangeState(UIState.Off);
                }
            }
        }

        /// <summary>
        /// Override to add select marker to the ignore list
        /// </summary>
        protected override void Init()
        {
            base.Init();
            if (!ignoreList.Contains(selectMarker))
            {
                ignoreList.Add(selectMarker);
            }           
        }

        /// <summary>
        /// OVerride it to ignore the select marker
        /// </summary>
        protected override void HandlePrintingState()
        {
            SetUIActive(true, ignoreList);
        }

        /// <summary>
        /// Propagates count event if group key matches
        /// </summary>
        /// <param name="_groupKey"></param>
        public void CountEvent(string _groupKey)
        {
            if (groupKey.Equals(_groupKey))
            {
                SelectableCount?.Invoke(groupKey, index);
            }
        }
    }
}
