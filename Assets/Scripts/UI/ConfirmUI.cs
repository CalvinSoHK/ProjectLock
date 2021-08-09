using Core.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Overworld
{
    /// <summary>
    /// Confirm UI script
    /// </summary>
    public class ConfirmUI : BaseUI
    {
        public delegate void OnConfirmEvent();
        public static OnConfirmEvent Confirm;
        public static OnConfirmEvent Deny;

        private void OnEnable()
        {
            DialogueUI.OnConfirmRequest += EnableUI;
        }

        private void OnDisable()
        {
            DialogueUI.OnConfirmRequest -= EnableUI;
        }

        /// <summary>
        /// Enables UI
        /// </summary>
        private void EnableUI()
        {
            SetUIActive(true);
        }

        public void ConfirmInvoke()
        {
            Confirm?.Invoke();
            SetUIActive(false);
        }

        public void DenyInvoke()
        {
            Deny?.Invoke();
            SetUIActive(false);
        }
    }
}
