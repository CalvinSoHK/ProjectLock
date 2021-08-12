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
        Debug.Log(stateManager.swapManager.playerParty.Count);
    }

    public override void Run()
    {

        SelectMon();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (stateManager.healthManager.playerCurHP <= 0) // If new selected and current mon is dead
            {
                Debug.Log("Dead select new mon");
                if (SelectMonCheck())
                {
                    stateManager.swapManager.currentDisplayedMon = stateManager.currentSelectedMon;
                    stateManager.swapManager.SwapTo(stateManager.currentSelectedMon);
                    stateManager.ChangeState(new BSplayerTurn(stateManager));
                }
            } else
            {            
                if (SelectMonCheck())
                {
                    stateManager.ChangeState(new BSaiResolve(stateManager));
                }

            }
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

    /// <summary>
    /// User selects mon with arrow keys
    /// </summary>
    void SelectMon()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (stateManager.currentSelectedMon > 2 && stateManager.currentSelectedMon <= stateManager.swapManager.playerParty.Count - 1)
            {
                stateManager.currentSelectedMon -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (stateManager.currentSelectedMon >= 0 && stateManager.currentSelectedMon < (stateManager.swapManager.playerParty.Count - 2))
            {
                stateManager.currentSelectedMon += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (stateManager.currentSelectedMon > 0 && stateManager.currentSelectedMon <= stateManager.swapManager.playerParty.Count -1)
            {
                stateManager.currentSelectedMon -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (stateManager.currentSelectedMon >= 0 && stateManager.currentSelectedMon < stateManager.swapManager.playerParty.Count -1)
            {
                stateManager.currentSelectedMon += 1;
            }
        }

        stateManager.swapManager.updateMonSelection(stateManager.currentSelectedMon);
    }

    /// <summary>
    /// Checks if player selected Mon is allowed
    /// </summary>
    bool SelectMonCheck()
    {
        //Personal ID for every Mon of player?
        //First Caught = 1?
        //2nd = 2
        if (stateManager.playerMonManager.GetPartyMember(stateManager.currentSelectedMon).baseMon.name == stateManager.playerCurMonster.baseMon.name)
        {
            Debug.Log("Alreaedy Selected");
            return false;
        }
        else if (stateManager.playerMonManager.GetPartyMember(stateManager.currentSelectedMon).battleObj.monStats.hp <= 0)
        {
            Debug.Log("Not Alive");
            return false;
        }
        else
        {
            return true;
        }
    }
}