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

    }

    public override void Run()
    {
        if (!stateManager.playerHasGone)
        {
            if (stateManager.playerPriority || stateManager.aiHasGone) //Player priority or ai has gone
            {
                if (stateManager.currentAction == 0)
                {
                    Debug.Log(stateManager.currentMove);
                    stateManager.damageManager.DealDamage(stateManager.aiCurMonster, stateManager.damageManager.DamageCalculationPlayer(stateManager.playerCurMonster.moveSet.GetMove(stateManager.currentMove)));
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.playerCurMonster.Nickname} uses {stateManager.playerCurMonster.moveSet.GetMove(stateManager.currentMove).name}!";
                    if (DeathCheck())
                    {
                        if (Core.CoreManager.Instance.encounterManager.EncounterInfo.encounterType != Core.Player.EncounterType.Wild)
                        {
                            stateManager.swapManager.SaveStats(stateManager.aiCurMonster);
                            if (stateManager.aiParty.GetFirstValidCombatant() != null)
                            {
                                stateManager.aiDecisionSwap.aiAverageSwap(stateManager.aiParty, stateManager.playerCurMonster);
                                //Reset?
                                stateManager.aiHasGone = true;
                                stateManager.ChangeState(new BSpostResolve(stateManager));
                                return;
                            }
                            else
                            {
                                stateManager.ChangeState(new BSwon(stateManager));
                                return;
                            }
                        }
                        else
                        {
                            stateManager.ChangeState(new BSwon(stateManager));
                            return;
                        }
                    }
                    else
                    {
                        stateManager.playerHasGone = true;
                    }
                }
                else if (stateManager.currentAction == 1)
                {
                    //use item
                    stateManager.itemManager.UserItem(stateManager.captureBall.ItemID);
                    //stateManager.itemManager.UseItem(stateManager.playerCurMonster);
                    stateManager.dialogueText.dialogueTexts.text = $"{stateManager.playerCurMonster.Nickname} uses {stateManager.currentAction}!";
                    stateManager.playerHasGone = true;
                }
                else if (stateManager.currentAction == 2)
                {
                    //swap
                    stateManager.swapManager.SaveStats(stateManager.playerParty.GetPartyMember(0));
                    stateManager.swapManager.SwapToPlayer(stateManager.currentSelectedMon);
                    stateManager.dialogueText.dialogueTexts.text = $"Player swaps to {stateManager.currentSelectedMon} (Check swapManager)!";
                    stateManager.playerHasGone = true;
                }
                else if (stateManager.currentAction == 3)
                {
                    //escape
                    stateManager.ChangeState(new BSescape(stateManager));
                    return;
                }
            }
            else
            {
                stateManager.ChangeState(new BSaiResolve(stateManager));
                return;
            }
        }
        else if (stateManager.playerHasGone && stateManager.playerPriority) //Player has gone and player goes first --> Ais turn
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Interact, CustomInput.InputEnums.InputAction.Down))
            {
                stateManager.ChangeState(new BSaiResolve(stateManager));
                return;
            }
        }
        else if (stateManager.playerHasGone && !stateManager.playerPriority) //Player has gone and player goes second --- > restart
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Interact, CustomInput.InputEnums.InputAction.Down))
            {
                stateManager.ChangeState(new BSpostResolve(stateManager));
                return;
                //manager.dialogueText.enableDialogueText(false);
            }
        }
    }

    public override void Leave()
    {
        base.Leave();
    }


    // TODO check if AI has remaining pokemon
    /// <summary>
    /// Checks if AI mon is still alive
    /// </summary>
    bool DeathCheck()
    {
        if (stateManager.healthManager.aiCurHP <= 0)
        {
            return true;
        }

        return false;
    }

}
