using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UI.Selector;
using UI.Nav;
using UI.Party;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Selector Controller
    /// </summary>
    SelectorControllerUI selectorController = new SelectorControllerUI();

    NavControllerUI navController = new NavControllerUI();

    PartyControllerUI partyController = new PartyControllerUI();

    List<IControllerUI> controllers = new List<IControllerUI>();

    private void Start()
    {
        InitControllers();
    }

    private void InitControllers()
    {
        selectorController.SetupController("Selector");
        selectorController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
        controllers.Add(selectorController);

        navController.SetupController("Dropdown");
        navController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
        //navController
        controllers.Add(navController);

        partyController.SetupController("Party");
        partyController.SetNavigation(UI.SelectableDirEnum.Horizontal);
        controllers.Add(partyController);


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
}
