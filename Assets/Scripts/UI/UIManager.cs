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
        DropdownControllerUI.DropdownOptionFire += Test;
        PartyElementUI.PartySelectFire += Temp;
    }

    private void OnDisable()
    {
        PartyElementUI.PartySelectFire -= Temp;
        DropdownControllerUI.DropdownOptionFire -= Test;
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

    private void Temp(string _key, int selectedIndex)
    {
        List<string> partyDropdownList = new List<string>();
        switch(_key)
        {
            //Enums?
            case "Party":
                partyDropdownList.Add("Swap");
                partyDropdownList.Add("Details");
                break;
            default:
                Debug.Log("Wrong Key specified");
                break;
        }
        dropdownController.MakeOrReplaceDropdown(partyDropdownList);
    }

    private void Test(string _key, string _optionKey)
    {
        Debug.Log("Test");
    }
}
