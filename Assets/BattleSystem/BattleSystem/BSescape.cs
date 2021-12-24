using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

public class BSescape : BSstate
{
    bool canEscape;
    public BSescape(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        escapeCheck();
    }

    public override void Run()
    {
        Escape();
    }

    /// <summary>
    /// Checks if player is able to escape
    /// </summary>
    void Escape()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (canEscape)
            {
                stateManager.swapManager.SaveStats(stateManager.playerCurMonster);
                Core.CoreManager.Instance.encounterManager.FinishEncounterAsync();
            }
             else
            {
                stateManager.playerHasGone = true;
                stateManager.ChangeState(new BSaiResolve(stateManager));
                return;
            }
        }
    }

    void escapeCheck()
    {
        //float odds = ((stateManager.playerCurMonster.stats.speed * 128) / (stateManager.aiCurMonster.stats.speed)) + (30 * attempts)
        float currentEscape = CoreManager.Instance.randomManager.Range(RandomType.Inconsistent, 0, 1f, "EscapeCheck");
        float odds = .8f;

        if (odds >= currentEscape)
        {

            stateManager.dialogueText.dialogueTexts.text = "You have escaped!";
            canEscape = true;
        }
        else
        {
            stateManager.dialogueText.dialogueTexts.text = "You are unable to escape!";
            canEscape = false;
        }
    }
}
