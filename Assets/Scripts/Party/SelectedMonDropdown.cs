using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;

namespace UI
{
    public class SelectedMonDropdown : DropdownUI
    {
        bool isPrinted = false;

        protected override void Start()
        {
            base.Start();
        }

        private void OnEnable()
        {
            PartyMonUI.OnMonSelectFire += Handler;
        }

        private void OnDisable()
        {
            PartyMonUI.OnMonSelectFire -= Handler;
        }

        protected override void HandlePrintingState()
        {
            base.HandlePrintingState();
            isPrinted = true;
        }

        protected override void HandleDisplayState()
        {
            if (Input.GetKeyDown(KeyCode.K)) 
            {
                ExitMenuPressed();
            }
        }
        protected override void HandleOffState()
        {
            base.HandleOffState();
            isPrinted = false;
        }

        /// <summary>
        /// Turns off state
        /// Only if List has been DroppedDown
        /// </summary>
        private void ExitMenuPressed()
        {
            if (isPrinted)
            {
                ResetList();
                ChangeState(UIState.Off);
            }
        }

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

        /// <summary>
        /// Selects DropdownElementDTO based on the current worldState
        /// </summary>
        private void Handler()
        {
            if(!isPrinted)
            { 
                switch (Core.CoreManager.Instance.worldStateManager.State)
                {
                    case Core.WorldState.Overworld:
                        OverworldParty();
                        break;
                    case Core.WorldState.Battle:
                        //BattleParty();
                        break;
                }
                ChangeState(UIState.Printing);
            }
        }

        private void OverworldParty()
        {
            List<DropdownElementDTO> dropdownList = new List<DropdownElementDTO>();
            dropdownList.Add(DetailAction);
            dropdownList.Add(SwapAction);
            dropdownList.Add(ItemAction);

            DropdownDTO testDTO = new DropdownDTO(groupKey, dropdownList);
            PopulateDropdown(testDTO);
        }


        private DropdownElementDTO DetailAction
        {
            get
            {
                UnityAction detailAction = new UnityAction(() => Debug.Log("Detail"));
                DropdownElementDTO detailElement = new DropdownElementDTO("Detail", detailAction, "Detail");
                return detailElement;
            }
        }

        private DropdownElementDTO SwapAction
        {
            get
            {
                UnityAction swapAction = new UnityAction(() => Debug.Log("Swap"));
                DropdownElementDTO swapElement = new DropdownElementDTO("Swap", swapAction, "Swap");
                return swapElement;
            }
        }

        private DropdownElementDTO ItemAction
        {
            get
            {
                UnityAction itemAction = new UnityAction(() => Debug.Log("Item"));
                DropdownElementDTO itemElement = new DropdownElementDTO("Item", itemAction, "Item");
                return itemElement;
            }
        }
    }

}
