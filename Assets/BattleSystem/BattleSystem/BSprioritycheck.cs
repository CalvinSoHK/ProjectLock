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
        if (manager.currentAction == 0 && manager.aicurrentAction == 0)
        {
            moveCheck();
        }
        else if (manager.currentAction == 0 && manager.aicurrentAction == 1)
        {
            manager.playerPriority = false;
            manager.ChangeState(new BSaiResolve(manager));
        }
        else if (manager.currentAction == 0 && manager.aicurrentAction == 2)
        {
            manager.playerPriority = false;
            manager.ChangeState(new BSaiResolve(manager));
        }
        else if (manager.currentAction == 1)
        {
            itemCheck();
        }
        else if (manager.currentAction == 2)
        {
            swapCheck();
        }
        else if (manager.currentAction == 3)
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
        if (manager.monster1.speed >= manager.monster2.speed)
        {
            //player goes first
            manager.playerPriority = true;
            //Debug.Log(manager.playerPriority);
            manager.ChangeState(new BSplayerResolve(manager));

        }
        else
        {   
            //player goes second
            manager.playerPriority = false;
            //Debug.Log(manager.playerPriority);
            manager.ChangeState(new BSaiResolve(manager));
        }
    }

    void itemCheck()
    {
        manager.playerPriority = true;
        manager.ChangeState(new BSplayerResolve(manager));
    }

    void swapCheck()
    {
        manager.playerPriority = true;
        manager.ChangeState(new BSplayerResolve(manager));
    }

    void escapeCheck()
    {
        manager.playerPriority = true;
        manager.ChangeState(new BSplayerResolve(manager));
    }
}
