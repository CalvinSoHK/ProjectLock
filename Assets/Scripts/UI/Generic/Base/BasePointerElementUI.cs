using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Base
{
    public class BasePointerElementUI : BaseElementUI, IPointerClickHandler,
            IPointerEnterHandler, IPointerExitHandler,
            IPointerDownHandler, IPointerUpHandler
    {
        [Header("Pointer Events")]
        [Tooltip("Fired when pointer is used to click this UI element")]
        [SerializeField]
        public UnityEvent OnPointerClickEvent;

        [SerializeField]
        [Tooltip("Fired on pointer entering this UI element")]
        public UnityEvent OnPointerEnterEvent;

        [SerializeField]
        [Tooltip("Fired on pointer exiting this UI element")]
        public UnityEvent OnPointerExitEvent;

        [SerializeField]
        [Tooltip("Fired on click up on this UI element")]
        public UnityEvent OnPointerUpEvent;

        [SerializeField]
        [Tooltip("Fired on click down on this UI element")]
        public UnityEvent OnPointerDownEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }
        
        /// <summary>
        /// Enables all elements under this element. (Changes to printing state)
        /// </summary>
        public override void EnableElement(string _key)
        {
            base.EnableElement(_key);
            SetObjectsActive(true);
            ChangeState(UIState.Printing);
        }

        /// <summary>
        /// Disables all elements under this element. (Changes to hiding state)
        /// </summary>
        public override void DisableElement()
        {
            SetObjectsActive(false);
            ChangeState(UIState.Hiding);
        }

        public override void RefreshElement()
        {
            base.RefreshElement();
        }
    }
}
