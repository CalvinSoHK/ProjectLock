using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSplayerResolve : BSstate
{

    public BSplayerResolve(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        manager.dialogueText.enableDialogueText(true);
        //Debug.Log("player resolve");
    }

    public override void Run()
    {
        if (!manager.playerHasGone)
        {
            if (manager.playerPriority || manager.aiHasGone) //Player priority or ai has gone
            {
                if (manager.currentAction == 0)
                {
                    manager.damageManager.DealDamage(manager.monster2, 30);
                    //DeathCheck();
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentMove}!";
                    manager.playerHasGone = true;
                }
                else if (manager.currentAction == 1)
                {
                    //use item
                    manager.itemManager.UseItem(manager.monster1);
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentAction}!";
                    manager.playerHasGone = true;
                }
                else if (manager.currentAction == 2)
                {
                    //swap
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentAction}!";
                    manager.playerHasGone = true;
                }
                else if (manager.currentAction == 3)
                {
                    //escape
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentAction}!"; ;
                    manager.ChangeState(new BSinitialize(manager));
                }
            }
            else
            {
                manager.ChangeState(new BSaiResolve(manager));
            }
        }
        else if (manager.playerHasGone && manager.playerPriority) //Player has gone and player goes first --> Ais turn
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                manager.ChangeState(new BSaiResolve(manager));
            }
        }
        else if (manager.playerHasGone && !manager.playerPriority) //Player has gone and player goes second --- > restart
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                manager.ChangeState(new BSpostResolve(manager));
                //manager.dialogueText.enableDialogueText(false);
            }
        }
    }

    public override void Leave()
    {
        base.Leave();
    }


    // TODO check if has remaining pokemon
    /// <summary>
    /// Checks if AI mon is still alive
    /// </summary>
    void DeathCheck()
    {
        if (manager.mon2curHP <= 0)
        {
            Debug.Log("Player wins");
            manager.dialogueText.enableDialogueText(false);
            manager.ChangeState(new BSwon(manager));
        }
    }


}
