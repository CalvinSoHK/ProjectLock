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
        stateManager.dialogueText.enableDialogueText(true);
        //Debug.Log("player resolve");
    }

    public override void Run()
    {
        if (!stateManager.playerHasGone)
        {
            if (stateManager.playerPriority || stateManager.aiHasGone) //Player priority or ai has gone
            {
                if (stateManager.currentAction == 0)
                {
                    stateManager.damageManager.DealDamage(stateManager.aiCurMonster, 3);
                    stateManager.dialogueText.dialogueTexts.text = $"Fix playerResolve nickname? i forget {stateManager.playerCurMonster.baseMon.name} uses {stateManager.currentMove}!";
                    DeathCheck();
                    stateManager.playerHasGone = true;
                }
                else if (stateManager.currentAction == 1)
                {
                    //use item
                    stateManager.itemManager.UseItem(stateManager.playerCurMonster);
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.playerCurMonster.baseMon.name} uses {stateManager.currentAction}!";
                    stateManager.playerHasGone = true;
                }
                else if (stateManager.currentAction == 2)
                {
                    //swap
                    stateManager.swapManager.SaveStats(stateManager.playerMonManager.GetPartyMember(stateManager.swapManager.currentDisplayedMon));
                    stateManager.swapManager.currentDisplayedMon = stateManager.currentSelectedMon;
                    //Debug.Log(stateManager.playerMonManager.GetPartyMember(stateManager.swapManager.currentDisplayedMon).baseMon.name);
                    stateManager.swapManager.SwapTo(stateManager.currentSelectedMon);
                    stateManager.dialogueText.dialogueTexts.text = $"Player swaps to {stateManager.currentSelectedMon} (Check swapManager)!";
                    stateManager.playerHasGone = true;
                }
                else if (stateManager.currentAction == 3)
                {
                    //escape
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.playerCurMonster.baseMon.name} uses {stateManager.currentAction}!";
                    stateManager.ChangeState(new BSinitialize(stateManager));
                }
            }
            else
            {
                stateManager.ChangeState(new BSaiResolve(stateManager));
            }
        }
        else if (stateManager.playerHasGone && stateManager.playerPriority) //Player has gone and player goes first --> Ais turn
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                stateManager.ChangeState(new BSaiResolve(stateManager));
            }
        }
        else if (stateManager.playerHasGone && !stateManager.playerPriority) //Player has gone and player goes second --- > restart
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                stateManager.ChangeState(new BSpostResolve(stateManager));
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
        if (stateManager.healthManager.aiCurHP <= 0)
        {
            
            Debug.Log("Player wins");
            //Earn XP based on enemy mon
            stateManager.ChangeState(new BSwon(stateManager));
        }
    }


}
