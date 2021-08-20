using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSwon : BSstate
{
    private bool firedOnce = false;

    public BSwon(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateManager.dialogueText.dialogueTexts.text = "You have won!";
        stateManager.swapManager.SaveStats(stateManager.playerCurMonster);
    }

    public override void Run()
    {
        //Change scene back to overworld

        if (Input.GetKeyDown(KeyCode.Return) && !firedOnce)
        {
            firedOnce = true;
            Core.CoreManager.Instance.encounterManager.FinishEncounterAsync();
        }

    }


}
