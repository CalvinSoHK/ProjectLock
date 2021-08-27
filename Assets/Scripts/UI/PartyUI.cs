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

        protected override void HandleDisable()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down))
            {
                PartyUIOff();               
            }
        }

        /// <summary>
        /// Turns Party UI on
        /// Turns off player interact
        /// </summary>
        private void PartyUIOn()
        {
            state = UIState.Displaying;
            SetUIActive(true);
            Debug.Log("Displaying");
            ResetUI();
            Core.CoreManager.Instance.player.DisableInput();
        }

        /// <summary>
        /// Turns Party UI off
        /// Turns off player interact
        /// </summary>
        private void PartyUIOff()
        {
            state = UIState.Off;
            SetUIActive(false);
            Debug.Log("Off");
            Core.CoreManager.Instance.player.EnableInput();
        }
    }
}