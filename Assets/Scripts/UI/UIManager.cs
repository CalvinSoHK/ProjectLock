using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UI.Selector;
using UI.Nav;
using UI.Party;
using UI.Dropdown;
using Mon.MonData;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Selector Controller
    /// </summary>
    public SelectorControllerUI selectorController = new SelectorControllerUI();

    public NavControllerUI navController = new NavControllerUI();

    public PartyControllerUI partyController = new PartyControllerUI();

    //public DropdownControllerUI dropdownController = new DropdownControllerUI();

    List<IControllerUI> controllers = new List<IControllerUI>();

    private void OnEnable()
    {
        //DropdownControllerUI.DropdownOptionFire += OnDropdownPress;
        //PartyElementUI.PartySelectFire += OnUISelect;
    }

    private void OnDisable()
    {
        //PartyElementUI.PartySelectFire -= OnUISelect;
        //DropdownControllerUI.DropdownOptionFire -= OnDropdownPress;
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

        partyController.SetupController("Party");
        partyController.SetNavigation(UI.SelectableDirEnum.Horizontal);
        controllers.Add(partyController);

        navController.SetupController("Navigation");
        navController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
        controllers.Add(navController);


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

    public int selectedIndex = -1;
/*    private void OnUISelect(string _key, int _selectedIndex)
    {
        List<string> partyDropdownList = new List<string>();
        //Clean this up     

        switch (_key)
        {
            //Enums?
            case "Party":
                if (partyController.savedSelectedIndex != -1)
                {
                    selectedIndex = _selectedIndex;
                }
                if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
                {
                    partyDropdownList.Add("Swap");
                    partyDropdownList.Add("Details");
                    partyController.SaveIndex(_selectedIndex);
                    if (partyController.firstIteration)
                    {
                        dropdownController.MakeOrReplaceDropdown(partyDropdownList);
                        dropdownController.EnableState();
                        partyController.firstIteration = false;
                    }
                    else
                    {
                        partyController.SwapMonOverworld(partyController.savedSelectedIndex, selectedIndex);
                    }
                } 
                else if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Battle)
                {
                    if (partyController.firstIteration)
                    {
                        partyDropdownList.Add("Swap");
                        partyDropdownList.Add("Item");
                        partyDropdownList.Add("Details");
                        dropdownController.MakeOrReplaceDropdown(partyDropdownList);
                        dropdownController.EnableState();
                        partyController.firstIteration = false;
                    }
                }
                break;
            default:
                Debug.Log("Wrong Key specified");
                break;
        }

        //dropdownController should partyController.SetLocked(true) when return key is pressed
        //And hide dropdown
    }*/
/*
    private void OnDropdownPress(string _key, string _optionKey)
    {
        //Hide Dropdown Needs change. Button still appears and works despite DisableState()
        //dropdownController.DisableState();
        if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
        {
            if (_optionKey == "Swap")
            {
                //Unlock changing party
                if (!partyController.firstIteration)
                {
                    partyController.model.SetLocked(false);
                    partyController.SelectorSetSelect(false);
                    //partyController.firstIteration = false; Here?
                    Debug.Log("Setting to second iteration");
                }
            }
        }
    }*/
    // When first is selected, Swap has not been pressed. Return down. Still locked
    public void PartyEnable()
    {
        partyController.EnableState();
    }

    public void PartyDisable()
    {
        partyController.SelectorSetSelect(false);
        partyController.model.SetLocked(false);
        partyController.firstIteration = true;
        partyController.DisableState();
    }
     
    public void PartyBattleCheck(int monNumber, MonIndObj playerMon)
    {
        partyController.MonInfoBattle(monNumber, playerMon);
    }
}
