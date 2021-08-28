using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

namespace UI
{
    public class PartyMonSelectUI : SelectorUI
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            PartyUI.OnPartySelectFire += OnPartySelectMenu;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PartyUI.OnPartySelectFire -= OnPartySelectMenu;
        }

        protected override void HandlePrintingState()
        {
        }


        protected override void HandleDisable()
        {
            
        }

        /// <summary>
        /// Turns Party UI on
        /// </summary>
        private void OnPartySelectMenu(int curIndex)
        {
            state = UIState.Printing;
            this.transform.gameObject.SetActive(true);
            Debug.Log("Check");
        }

        /// <summary>
        /// Turns Party UI off
        /// </summary>
        private void OffPartySelectMenu()
        {
            this.transform.gameObject.SetActive(false);
        }

        
    }
}
