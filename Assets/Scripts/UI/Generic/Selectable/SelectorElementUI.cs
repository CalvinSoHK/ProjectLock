using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI.Base;

namespace UI.Selector
{
    public class SelectorElementUI : BasePointerElementUI
    {
        [Header("Selectable Options")]
        [Tooltip("Index of this selectable component.")]
        [SerializeField]
        protected int selectableIndex = -1;

        /// <summary>
        /// Index as a selectable of this element
        /// </summary>
        public int SelectableIndex
        {
            get
            {
                return selectableIndex;
            }
        }

        [Tooltip("Select marker is enabled and disabled when this is selected")]
        [SerializeField]
        private GameObject selectMarker;

        [Tooltip("Unity event that fires when the element is selected.")]
        [SerializeField]
        public UnityEvent OnSelect;

        /// <summary>
        /// Sets the index of this selectable UI.
        /// </summary>
        /// <param name="_index"></param>
        public void SetIndex(int _index)
        {
            selectableIndex = _index;
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
        /// Select by clicking
        /// </summary>
        public virtual void Select()
        {
            Hover();
            Debug.Log("Selected: " + gameObject.name);
            OnSelect?.Invoke();
        }

        /// <summary>
        /// OVerride it to ignore the select marker
        /// </summary>
        public override void HandlePrintingState()
        {
            base.HandlePrintingState();
            ChangeState(UIState.Displaying);
        }
    }
}
