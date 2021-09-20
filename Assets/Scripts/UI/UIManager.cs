using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UI.Selector;
using UI.Nav;
using UI.Party;
using UI.Dropdown;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Selector Controller
    /// </summary>
    SelectorControllerUI selectorController = new SelectorControllerUI();

    NavControllerUI navController = new NavControllerUI();

    PartyControllerUI partyController = new PartyControllerUI();

    DropdownControllerUI dropdownController = new DropdownControllerUI();

    List<IControllerUI> controllers = new List<IControllerUI>();

    private void OnEnable()
    {
        DropdownControllerUI.DropdownOptionFire += OnDropdownPress;
        PartyElementUI.PartySelectFire += OnUISelect;
    }

    private void OnDisable()
    {
        PartyElementUI.PartySelectFire -= OnUISelect;
        DropdownControllerUI.DropdownOptionFire -= OnDropdownPress;
    }

    private void Start()
    {
        InitControllers();
    }

    private void InitControllers()
    {
        selectorController.SetupController("Selector");
        selectorController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
        controllers.Add(selectorController);

        /*navController.SetupController("Dropdown");
        navController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
        //navController
        controllers.Add(navController);*/

        partyController.SetupController("Party");
        partyController.SetNavigation(UI.SelectableDirEnum.Horizontal);
        controllers.Add(partyController);

        dropdownController.SetupController("Dropdown");
        dropdownController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
        controllers.Add(dropdownController);


    }

    private void Update()
    {
        HandleControllers();
    }

    private void HandleControllers()
    {
        foreach(IControllerUI controller in controllers)
        {
            controller.HandleState();
        }
    }

    int selectedIndex = -1;
    private void OnUISelect(string _key, int _selectedIndex)
    {
        List<string> partyDropdownList = new List<string>();
        if (partyController.savedSelectedIndex != -1)
        {
            selectedIndex = _selectedIndex;
        }
        //Clean this up     
        if (partyController.firstIteration)
        {
            switch (_key)
            {
                //Enums?
                case "Party":
                    partyDropdownList.Add("Swap");
                    partyDropdownList.Add("Details");
                    partyController.SaveIndex(_selectedIndex);
                    break;
                default:
                    Debug.Log("Wrong Key specified");
                    break;
            }

            dropdownController.MakeOrReplaceDropdown(partyDropdownList);
            dropdownController.EnableState();
        } else
        {
            //Stuck here 
            partyController.SwapMon(partyController.savedSelectedIndex, selectedIndex);
        }
    }

    private void OnDropdownPress(string _key, string _optionKey)
    {
        if (_optionKey == "Swap")
        {
            //Hide Dropdown Needs change
            dropdownController.DisableState();

            //Unlock changing party
            if (partyController.firstIteration)
            {
                partyController.model.SetLocked(false);
                partyController.SelectorSetSelect(false);
                partyController.firstIteration = false;
                Debug.Log("Setting to second iteration");
            }
        }
    }
}
