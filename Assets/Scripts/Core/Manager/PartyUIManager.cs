using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;

namespace Core.PartyUI
{
    public class PartyUIManager : MonoBehaviour
    {
        /// <summary>
        /// Delegate event for party UI manager.
        /// </summary>
        public delegate void PartyEvent();
        public static PartyEvent OnPartyFire;
        public static PartyEvent OnPartyAfterFire;


        private void Update()
        {
            if (CoreManager.Instance.worldStateManager.State == WorldState.Overworld && CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Party, InputEnums.InputAction.Down))
            {
                FirePartyEvent();
            }
        }

        /// <summary>
        /// Fires a Party Menu event
        /// </summary>
        public void FirePartyEvent()
        {
            OnPartyFire?.Invoke();
        }

    }
}