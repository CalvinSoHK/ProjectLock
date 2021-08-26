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
        Debug.Log("Enemy Resolve");
        stateManager.dialogueText.enableDialogueText(true);
        //Debug.Log("Ai resolve");
    }

    public override void Run()
    {
        if (!stateManager.aiHasGone)
        {
            if (!stateManager.playerPriority || stateManager.playerHasGone)
            {

                if (stateManager.aiCurrentAction == 0)
                {
                    stateManager.aiDecisionMove.MoveSelection();
                    //Debug.Log("Hypothetical Move: " + stateManager.aiCurrentMove);
                    stateManager.damageManager.DealDamage(stateManager.playerCurMonster, stateManager.damageManager.DamageCalculationAI(stateManager.aiCurMonster.moveSet.GetMove(stateManager.aiCurrentMove)));
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.aiCurMonster.baseMon.name} uses {stateManager.aiCurMonster.moveSet.GetMove(stateManager.aiCurrentMove).moveName}!";
                    if (DeathCheck())
                    {
                        if (stateManager.playerParty.GetFirstValidCombatant() != null)
                        {
                            Debug.Log(stateManager.playerParty.GetFirstValidCombatant().baseMon.name);

                            stateManager.ChangeState(new BSplayerSwap(stateManager));
                            return;
                        }
                        else
                        {
                            stateManager.ChangeState(new BSlost(stateManager));
                            return;
                        }
                    }
                    stateManager.aiHasGone = true;
                }
                else if (stateManager.aiCurrentAction == 1)
                {
                    stateManager.itemManager.UseItem(stateManager.aiCurMonster);
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.aiCurMonster.baseMon.name} uses {stateManager.aiCurrentAction} heal!";
                    stateManager.aiHasGone = true;
                }
                else if (stateManager.aiHasGone)
                {
                    stateManager.ChangeState(new BSplayerTurn(stateManager));
                    return;
                }
            }
            else
            {
                stateManager.ChangeState(new BSplayerResolve(stateManager));
                return;
            }
        }
        else if (stateManager.aiHasGone && !stateManager.playerPriority)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                stateManager.ChangeState(new BSplayerResolve(stateManager));
                return;
            }
        }
        else if (stateManager.aiHasGone && stateManager.playerPriority) //Both AI and Player has gone --> restart
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //manager.ChangeState(new BSplayerTurn(manager));
                //manager.dialogueText.enableDialogueText(false);
                stateManager.ChangeState(new BSpostResolve(stateManager));
                return;
            }
        }
    }





    /// <summary>
    /// Checks if players mon is still alive
    /// If there is swap to Alive Mon
    /// Else lose state
    /// </summary>
    bool DeathCheck()
    {
        if (stateManager.healthManager.playerCurHP <= 0)
        {
            stateManager.swapManager.SaveStats(stateManager.playerParty.GetPartyMember(stateManager.swapManager.currentActiveMon));
            return true;
        }

        return false;
    }
}
    