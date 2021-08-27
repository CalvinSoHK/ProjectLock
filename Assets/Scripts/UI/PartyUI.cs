using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
using Core.PartyUI;

namespace UI.PartyUI
{
    public class PartyUI : BaseUI
    {

        private void OnEnable()
        {
            PartyUIManager.OnPartyFire += PartyUIOn;
        }

        private void OnDisable()
        {
            PartyUIManager.OnPartyFire -= PartyUIOn;
        }

        protected override void HandlePrintingState()
        {
            ChangeState(UIState.Displaying);
        }

        protected override void HandleDisplayState()
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


        /// <summary>
        /// Enables UI
        /// </summary>
        private void EnableUI()
        {
            SetUIActive(true);
        }

    }
}