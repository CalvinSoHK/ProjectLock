using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI.Base;
using Core.MessageQueue;

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

        private PointerColorPicker colorPicker = null;

        protected bool selected = false;

        protected override void InitGeneral()
        {
            base.InitGeneral();
            colorPicker = GetComponent<PointerColorPicker>();
        }

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
        /// Selected by controller select
        /// </summary>
        public virtual void Select()
        {
            if (!selected)
            {
                OnSelect?.Invoke();
                if (colorPicker != null)
                {
                    colorPicker.ChangeColor(colorPicker.SelectedColor);
                    colorPicker.SetLock(true);
                }
                selected = true;
            }
        }

        /// <summary>
        /// Deselects this button.
        /// </summary>
        public virtual void Deselect()
        {
            if (selected)
            {
                if (colorPicker != null)
                {
                    colorPicker.SetLock(false);
                    colorPicker.ChangeColor(colorPicker.DefaultColor);
                }
                selected = false;
            }
        }

        /// <summary>
        /// Click selects. Does not invoke Select or else it would loop
        /// </summary>
        public virtual void ClickSelect()
        {
            Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(MessageQueueManager.UI_KEY, key, JsonUtility.ToJson(new SelectorMessageObject(selectableIndex)));
        }
    }
}
