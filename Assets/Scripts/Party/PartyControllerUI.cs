using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
using UI.Selector;

namespace UI.Party
{
    public class PartyControllerUI : SelectorControllerUI
    {
        public override void HandleHidingState()
        {
            base.HandleHidingState();
            //ResetList();
        }

        /*
        /// <summary>
        /// Destroys list gameObject
        /// </summary>
        private void ResetList()
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        */
    }

}
