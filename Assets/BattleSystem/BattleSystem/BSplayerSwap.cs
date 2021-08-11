using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSplayerSwap : BSstate
{
    public BSplayerSwap(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        stateManager.swapScreen.SetActive(true);
    }

    public override void Run()
    {

        SelectMon();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Personal ID for every Mon of player?
            //First Caught = 1?
            //2nd = 2
            if (stateManager.playerMonManager.GetPartyMember(stateManager.currentSelectedMon).baseMon.name == stateManager.playerCurMonster.baseMon.name)
            {
                Debug.Log("Alreaedy Selected");
            } 
            else
            {
                stateManager.ChangeState(new BSaiTurn(stateManager));
            }
            //Select Mon here
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateManager.ChangeState(new BSplayerTurn(stateManager));
        }
    }

    public override void Leave()
    {
        stateManager.swapScreen.SetActive(false);
    }
    //Todo Figure out variables for currentSelectedMon so it isnt static
    /// <summary>
    /// User selects mon with arrow keys
    /// </summary>
    void SelectMon()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (stateManager.currentSelectedMon > 2 && stateManager.currentSelectedMon <= stateManager.swapManager.playerParty.Count)
            {
                stateManager.currentSelectedMon -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (stateManager.currentSelectedMon >= 0 && stateManager.currentSelectedMon < (stateManager.swapManager.playerParty.Count - 1))
            {
                stateManager.currentSelectedMon += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (stateManager.currentSelectedMon > 0 && stateManager.currentSelectedMon <= stateManager.swapManager.playerParty.Count)
            {
                stateManager.currentSelectedMon -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (stateManager.currentSelectedMon >= 0 && stateManager.currentSelectedMon < stateManager.swapManager.playerParty.Count)
            {
                stateManager.currentSelectedMon += 1;
            }
        }

        stateManager.swapManager.updateMonSelection(stateManager.currentSelectedMon);
    }
}
