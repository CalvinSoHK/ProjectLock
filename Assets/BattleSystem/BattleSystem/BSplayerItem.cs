using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSplayerItem : BSstate
{
    public BSplayerItem(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {


        //Temporary

        /*if(stateManager.playerhealthpots > 0)
        {
            stateManager.ChangeState(new BSaiTurn(stateManager));
            return;
        }
        else
        {
            Debug.Log("No Healing Pots Remaining");
        }*/

        if (Core.CoreManager.Instance.encounterManager.EncounterInfo.encounterType == Core.Player.EncounterType.Wild)
        {
            stateManager.ChangeState(new BSaiTurn(stateManager));
            return;
        }
        else
        {
            Debug.Log("Cannot catch non-wild");
            stateManager.ChangeState(new BSplayerTurn(stateManager));
            return;
        }

    }

    public override void Run()
    {
        //Select item here
    }
}
