using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
using Core.PartyUI;

namespace UI
{
    public class PartyUI : SelectorUI
    {

        protected override void OnEnable()
        {
            base.OnEnable();
            PartyUIManager.OnPartyFire += PartyUIOn;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PartyUIManager.OnPartyFire -= PartyUIOn;
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
    }
}