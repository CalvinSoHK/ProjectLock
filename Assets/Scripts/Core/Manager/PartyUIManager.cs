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
        /// <param name="dialogueID"></param>
        public delegate void PartyEvent();
        public static PartyEvent OnPartyFire;
        public static PartyEvent OnPartyAfterFire;


        private void Update()
        {
            if (CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Party, InputEnums.InputAction.Down))
            {
                Debug.Log("Pressed");
                FirePartyEvent();
            }
        }


        public void FirePartyEvent()
        {
            OnPartyFire?.Invoke();
        }

    }
}