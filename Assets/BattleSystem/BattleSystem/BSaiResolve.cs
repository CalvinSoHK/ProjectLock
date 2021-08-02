using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BSaiResolve : BSstate
{

    public BSaiResolve(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        manager.dialogueText.enableDialogueText(true);
        //Debug.Log("Ai resolve");
    }

    public override void Run()
    {
        if (!manager.aiHasGone)
        {
            if (!manager.playerPriority || manager.playerHasGone)
            {

                if (manager.aicurrentAction == 0)
                {
                    manager.damageManager.DealDamage(manager.monster1, 10);
                    //DeathCheck();
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster2.name} uses {manager.aicurrentAction}!";
                    manager.aiHasGone = true;
                }
                else if (manager.aicurrentAction == 1)
                {
                    manager.itemManager.UseItem(manager.monster2);
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster2.name} uses {manager.aicurrentAction} heal!";
                    manager.aiHasGone = true;
                }
                else if (manager.aiHasGone)
                {
                    manager.ChangeState(new BSplayerTurn(manager));
                }
            }
            else
            {
                manager.ChangeState(new BSplayerResolve(manager));
            }
        }
        else if (manager.aiHasGone && !manager.playerPriority)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                manager.ChangeState(new BSplayerResolve(manager));
            }
        }
        else if (manager.aiHasGone && manager.playerPriority) //Both AI and Player has gone --> restart
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //manager.ChangeState(new BSplayerTurn(manager));
                //manager.dialogueText.enableDialogueText(false);
                manager.ChangeState(new BSpostResolve(manager));
            }
        }
    }





    /// <summary>
    /// Checks if players mon is still alive
    /// </summary>
    void DeathCheck()
    {
        if (manager.mon1curHP <= 0)
        {
            Debug.Log("AI wins");
            manager.dialogueText.enableDialogueText(false);
            manager.ChangeState(new BSlost(manager));
        }
    }

}
    