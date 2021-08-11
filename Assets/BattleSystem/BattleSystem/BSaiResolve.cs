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
        stateManager.dialogueText.enableDialogueText(true);
        //Debug.Log("Ai resolve");
    }

    public override void Run()
    {
        if (!stateManager.aiHasGone)
        {
            if (!stateManager.playerPriority || stateManager.playerHasGone)
            {

                if (stateManager.aicurrentAction == 0)
                {
                    stateManager.damageManager.DealDamage(stateManager.playerCurMonster, 1);
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.aiCurMonster.baseMon.name} uses {stateManager.aicurrentAction}!";
                    DeathCheck();
                    stateManager.aiHasGone = true;
                }
                else if (stateManager.aicurrentAction == 1)
                {
                    stateManager.itemManager.UseItem(stateManager.aiCurMonster);
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.aiCurMonster.baseMon.name} uses {stateManager.aicurrentAction} heal!";
                    stateManager.aiHasGone = true;
                }
                else if (stateManager.aiHasGone)
                {
                    stateManager.ChangeState(new BSplayerTurn(stateManager));
                }
            }
            else
            {
                stateManager.ChangeState(new BSplayerResolve(stateManager));
            }
        }
        else if (stateManager.aiHasGone && !stateManager.playerPriority)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                stateManager.ChangeState(new BSplayerResolve(stateManager));
            }
        }
        else if (stateManager.aiHasGone && stateManager.playerPriority) //Both AI and Player has gone --> restart
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //manager.ChangeState(new BSplayerTurn(manager));
                //manager.dialogueText.enableDialogueText(false);
                stateManager.ChangeState(new BSpostResolve(stateManager));
            }
        }
    }





    /// <summary>
    /// Checks if players mon is still alive
    /// </summary>
    void DeathCheck()
    {
        if (stateManager.healthManager.playerCurHP <= 0)
        {
            //Debug.Log("AI wins");
            //stateManager.dialogueText.enableDialogueText(false);
            stateManager.ChangeState(new BSlost(stateManager));
        }
    }

}
    