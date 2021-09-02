using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using CustomInput;

namespace UI
{
    public class PartySwapManager : MonoBehaviour
    {
        bool firstIteration = true;
        bool secondIteration = false;

        [SerializeField]
        int savedIndex, newSwapMon;

        [SerializeField]
        private string targetGroupKey, targetKey;

        public delegate void OnPartySwapEvent();
        public static OnPartySwapEvent OnPartySwapFire;

        private void OnEnable()
        {
            DropdownUI.DropdownOptionFire += EnableSwap;
            PartyMonUI.OnMonSelectFire += SaveMon;
            //SelectableUI.SelectableSelectFire += EnableUI;
        }

        private void OnDisable()
        {
            DropdownUI.DropdownOptionFire -= EnableSwap;
            PartyMonUI.OnMonSelectFire -= SaveMon;
            //SelectableUI.SelectableSelectFire -= EnableUI;
        }

        private void Update()
        {
            
        }


        /// <summary>
        /// Enables Swap if _groupKey and _Key matches
        /// </summary>
        /// <param name="_groupKey"></param>
        /// <param name="_key"></param>
        private void EnableSwap(string _groupKey, string _key)
        {
            //Hide This Dropdown
            //Show PartyMonUI

            if (_groupKey.Equals(targetGroupKey) && _key.Equals(targetKey))
            {
                Debug.Log(firstIteration + " " + secondIteration);  
                SwapMons(savedIndex, newSwapMon);
                Debug.Log("Swap Mons");
                OnPartySwapFire?.Invoke();
                return;
                //Select second mon
            }
        }

        private void SaveMon(int _savedIndex)
        {
            if (firstIteration)
            {
                savedIndex = _savedIndex;
                firstIteration = false;
            } else
            {
                newSwapMon = _savedIndex;
                secondIteration = true;
            }
        }
        /// <summary>
        /// Swaps currently Selected Index with New Index
        /// </summary>
        /// <param name="newMonster"></param>
        private void SwapMons(int _savedIndex, int _selectedMon)
        {
            if (_savedIndex != _selectedMon)
            {
                Core.CoreManager.Instance.playerParty.party.SwapMembers(_savedIndex, _selectedMon);
                Debug.Log("Swapped");
            }
        }

    }
}
