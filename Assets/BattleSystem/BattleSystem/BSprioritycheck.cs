using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;
using Mon.MonGeneration;

public class BSprioritycheck : BSstate
{
    public BSprioritycheck(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Priority Check entered");
    }

    public override void Run()
    {
        if (stateManager.currentAction == 0 && stateManager.aicurrentAction == 0)
        {
            moveCheck();
        }
        else if (stateManager.currentAction == 0 && stateManager.aicurrentAction == 1)
        {
            stateManager.playerPriority = false;
            stateManager.ChangeState(new BSaiResolve(stateManager));
        }
        else if (stateManager.currentAction == 0 && stateManager.aicurrentAction == 2)
        {
            stateManager.playerPriority = false;
            stateManager.ChangeState(new BSaiResolve(stateManager));
        }
        else if (stateManager.currentAction == 1)
        {
            itemCheck();
        }
        else if (stateManager.currentAction == 2)
        {
            swapCheck();
        }
        else if (stateManager.currentAction == 3)
        {
            escapeCheck();
        }
    }

    public override void Leave()
    {

    }

    /// <summary>
    /// Priority checker if player uses a move/skill
    /// </summary>
    void moveCheck()
    {
        
        // If (skill priority > skill priority)

        // if Player > ai speed
        if (stateManager.playerCurMonster.battleObj.monStats.speed >= stateManager.aiCurMonster.battleObj.monStats.speed)
        {
            //player goes first
            stateManager.playerPriority = true;
            //Debug.Log(manager.playerPriority);
            stateManager.ChangeState(new BSplayerResolve(stateManager));

        }
        else
        {   
            //player goes second
            stateManager.playerPriority = false;
            //Debug.Log(manager.playerPriority);
            stateManager.ChangeState(new BSaiResolve(stateManager));
        }
    }

    void itemCheck()
    {
        stateManager.playerPriority = true;
        stateManager.ChangeState(new BSplayerResolve(stateManager));
    }

    void swapCheck()
    {
        stateManager.playerPriority = true;
        stateManager.ChangeState(new BSplayerResolve(stateManager));
    }

    void escapeCheck()
    {
        stateManager.playerPriority = true;
        stateManager.ChangeState(new BSplayerResolve(stateManager));
    }
}
