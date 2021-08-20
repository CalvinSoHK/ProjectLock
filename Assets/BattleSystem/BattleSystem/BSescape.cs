using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Core.CoreManager.Instance.encounterManager.FinishEncounter();
            }
             else
            {
                stateManager.playerHasGone = true;
                stateManager.ChangeState(new BSaiResolve(stateManager));
            }
        }
    }

    void escapeCheck()
    {
        //float odds = ((stateManager.playerCurMonster.stats.speed * 128) / (stateManager.aiCurMonster.stats.speed)) + (30 * attempts)
        float currentEscape = Random.Range(0, 1f);
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
