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

        protected override void HandleDisable()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down)) // If player wants to exit Party UI
            {
                PartyUIOff();               
            } 
            else if (Input.GetKeyDown(KeyCode.Return)) //If player selects current index
            {
                Debug.Log("Open Dropdown state");
                //Change state to Dropdown menu
                PartyMenuFire();
            }
        }

        /// <summary>
        /// Turns Party UI on
        /// Turns off player interact
        /// </summary>
        private void PartyUIOn()
        {
            state = UIState.Displaying;
            //SetUIActive(true);
            this.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Displaying");
            Reset();
            Core.CoreManager.Instance.player.DisableInput();
        }

        /// <summary>
        /// Turns Party UI off
        /// Turns off player interact
        /// </summary>
        private void PartyUIOff()
        {
            state = UIState.Off;
            //SetUIActive(false);
            this.transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Off");
            Core.CoreManager.Instance.player.EnableInput();
        }

        /// <summary>
        /// Fires a Party Drop Down Menu Event
        /// </summary>
        private void PartyMenuFire()
        {

            OnPartySelectFire?.Invoke(0);
        }
    }
}