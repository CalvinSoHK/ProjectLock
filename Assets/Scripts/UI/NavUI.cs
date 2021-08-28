using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;

namespace UI
{
    public class NavUI : DropdownUI
    {
        protected override void Start()
        {
            base.Start();

            UnityAction partyAction = new UnityAction(() => Debug.Log("Party"));
            DropdownElementDTO partyElement = new DropdownElementDTO("Party", partyAction, "Party");

            UnityAction bagAction = new UnityAction(() => Debug.Log("Bag"));
            DropdownElementDTO bagElement = new DropdownElementDTO("Bag", bagAction, "Bag");

            UnityAction optionAction = new UnityAction(() => Debug.Log("Options"));
            DropdownElementDTO optionElement = new DropdownElementDTO("Options", optionAction, "Options");

            List<DropdownElementDTO> dropdownList = new List<DropdownElementDTO>();
            dropdownList.Add(partyElement);
            dropdownList.Add(bagElement);
            dropdownList.Add(optionElement);

            DropdownDTO testDTO = new DropdownDTO(groupKey, dropdownList);
            PopulateDropdown(testDTO);
        }

        protected override void HandleOffState()
        {
            base.HandleOffState();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld && Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Navigation, InputEnums.InputAction.Down))
            {
                ChangeState(UIState.Printing);
            }
        }
    }
}
