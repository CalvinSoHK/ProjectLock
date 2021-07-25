using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Action(manager.aicurrentAction);
    }

    void Action(int aiSkill)
    {
        switch (aiSkill)
        {
            case 0:
                Debug.Log($"{aiSkill} move used");
                manager.ChangeState(new BSprioritycheck(manager));
                break;
            case 1:
                Debug.Log($"{aiSkill} item used");
                manager.ChangeState(new BSprioritycheck(manager));
                break;
            case 2:
                Debug.Log($"{aiSkill} swap used");
                manager.ChangeState(new BSprioritycheck(manager));
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
            if (manager.aihealthpots > 0)
            {
                manager.aicurrentAction = 1;
            }
            else
            {
                Debug.Log("No more");
                manager.aicurrentAction = 0;
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
            manager.aicurrentAction = 0;
        }
        else
        {
            
        }

    }

    bool HealthTreshhold()
    {
        if (((float) manager.mon2curHP / manager.mon2maxHP) < itemTreshhold)
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
