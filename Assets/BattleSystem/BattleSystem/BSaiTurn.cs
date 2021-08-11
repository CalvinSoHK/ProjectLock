using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ai decision tree. Happens at the beginning
/// </summary>
public class BSaiTurn : BSstate
{
    float itemTreshhold = .30f;
    public BSaiTurn(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //manager.aicurrentAction = Random.Range(0,3);
        ToItem();

    }

    public override void Run()
    {
        Action(stateManager.aicurrentAction);
    }

    void Action(int aiSkill)
    {
        switch (aiSkill)
        {
            case 0:
                Debug.Log($"{aiSkill} move used");
                stateManager.ChangeState(new BSprioritycheck(stateManager));
                break;
            case 1:
                Debug.Log($"{aiSkill} item used");
                stateManager.ChangeState(new BSprioritycheck(stateManager));
                break;
            case 2:
                Debug.Log($"{aiSkill} swap used");
                stateManager.ChangeState(new BSprioritycheck(stateManager));
                break;
            default:
                break;

        }
    }

    /// <summary>
    /// Determing to use item or not
    /// </summary>
    void ToItem()
    {
        //isTrainer?    
        if (HealthTreshhold())
        {

            //Check health potions available
            if (stateManager.aihealthpots > 0)
            {
                stateManager.aicurrentAction = 1;
            }
            else
            {
                Debug.Log("No more");
                stateManager.aicurrentAction = 0;
            }
        }
        else if (!HealthTreshhold() && HaveCondition())
        {

        }
        else if (!HealthTreshhold() && !HaveCondition()) 
        {
            //Check swap
            Debug.Log("Check Swap");
            //manager.aicurrentAction = 2;
            stateManager.aicurrentAction = 0;
        }
        else
        {
            
        }

    }

    /// <summary>
    /// Checking for Ai Health threshold to use heal
    /// </summary>
    /// <returns></returns>
    bool HealthTreshhold()
    {
        if (((float) stateManager.healthManager.aiCurHP / stateManager.healthManager.aiMaxHP) < itemTreshhold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    bool HaveCondition()
    {
        return false;
    }
}
