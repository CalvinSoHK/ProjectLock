using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
using Core.PartyUI;

namespace UI
{
    public class PartyUI : SelectorUI
    {

        /// <summary>
        /// Delegate for when party mon is selected
        /// </summary>
        public delegate void PartyMenuEvent(int curIndex);
        public static PartyMenuEvent OnPartySelectFire;

        public delegate void OnPartyReady();
        public static OnPartyReady OnPartyReadyFire;

        protected override void OnEnable()
        {
            base.OnEnable();
            PartyUIManager.OnPartyFire += PartyUIOn;
            PartyMonUI.MonRecount += Init;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PartyUIManager.OnPartyFire -= PartyUIOn;
            PartyMonUI.MonRecount -= Init;
        }

        /// <summary>
        /// Turns Party UI on
        /// Turns off player interact
        /// </summary>
        private void PartyUIOn()
        {
            ChangeState(UIState.Printing);
        }

        /// <summary>
        /// Turns Party UI off
        /// Turns off player interact
        /// </summary>
        private void PartyUIOff()
        {
            ChangeState(UIState.Off);
        }

        protected override void HandlePrintingState()
        {
            base.HandlePrintingState();
            CountSelectables();
            OnPartyReadyFire?.Invoke();
        }

    }
}