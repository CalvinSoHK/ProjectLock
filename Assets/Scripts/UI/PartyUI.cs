using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PartyUI
{
    public class PartyUI : BaseUI
    {
        protected override void HandleDisplayState()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                { 
                    EnableUI();
                }
            }
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