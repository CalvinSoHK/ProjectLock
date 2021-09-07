using Core.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;

namespace UI.Dialogue
{
    /// <summary>
    /// Confirm UI script
    /// </summary>
    public class ConfirmElementUI : BaseElementUI
    {
        public delegate void OnConfirmEvent();
        public static OnConfirmEvent Confirm;
        public static OnConfirmEvent Deny;

        private void OnEnable()
        {
            DialogueElementUI.OnConfirmRequest += EnableUI;
        }

        private void OnDisable()
        {
            DialogueElementUI.OnConfirmRequest -= EnableUI;
        }

        /// <summary>
        /// Enables UI
        /// </summary>
        private void EnableUI()
        {
            SetObjectsActive(true);
        }

        public void ConfirmInvoke()
        {
            Confirm?.Invoke();
            SetObjectsActive(false);
        }

        public void DenyInvoke()
        {
            Deny?.Invoke();
            SetObjectsActive(false);
        }

        public override void EnableElement()
        {
            throw new System.NotImplementedException();
        }

        public override void DisableElement()
        {
            throw new System.NotImplementedException();
        }

        public override void RefreshElement()
        {
            throw new System.NotImplementedException();
        }
    }
}
