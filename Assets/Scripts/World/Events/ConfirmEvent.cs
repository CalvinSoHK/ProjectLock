using UI.Dialogue;
using UnityEngine;
using UnityEngine.Events;

namespace World.Event
{
    public class ConfirmEvent : BaseEvent
    {
        [SerializeField]
        UnityEvent OnConfirm;

        [SerializeField]
        UnityEvent OnDeny;

        private void OnEnable()
        {
            DialogueElementUI.OnConfirmRequest += FireConfirm;
        }

        private void OnDisable()
        {
            DialogueElementUI.OnConfirmRequest -= FireConfirm;
        }

        /// <summary>
        /// Fires BeforeEvent
        /// Fires Confirm
        /// Subscribes our Confirm and Deny events to the UI
        /// </summary>
        public void FireConfirm()
        {
            OnBeforeEventFire?.Invoke();
            FireOnConfirm();
            ConfirmElementUI.Confirm += Confirm;
            ConfirmElementUI.Deny += Deny;
        }

        /// <summary>
        /// Fires the OnConfirm UI event.
        /// Disables interaction.
        /// Unsubscribes itself
        /// </summary>
        private void FireOnConfirm()
        {
            Core.CoreManager.Instance.interact.DisableInteract();
            OnEventFire?.Invoke();
            DialogueElementUI.OnConfirmRequest -= FireOnConfirm;
        }

        /// <summary>
        /// Re-enables interaction
        /// Fires AfterEvent
        /// </summary>
        private void FireOnConfirmAfter()
        {
            Core.CoreManager.Instance.interact.EnableInteract();
            OnAfterEventFire?.Invoke();
        }

        /// <summary>
        /// Fires the OnConfirm event and calls OnAnyInput to clean up
        /// </summary>
        private void Confirm()
        {
            OnConfirm?.Invoke();
            OnAnyInput();
        }

        /// <summary>
        /// Fires the OnDeny event and callsk OnAnyInput to clean up
        /// </summary>
        private void Deny()
        {
            OnDeny?.Invoke();
            OnAnyInput();
        }

        /// <summary>
        /// Does clean up when either confirm or deny is fired
        /// </summary>
        private void OnAnyInput()
        {
            FireOnConfirmAfter();
            ConfirmElementUI.Confirm -= Confirm;
            ConfirmElementUI.Deny -= Deny;
        }
    }
}
