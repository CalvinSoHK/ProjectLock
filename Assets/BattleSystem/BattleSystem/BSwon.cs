using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSwon : BSstate
{
    public BSwon(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Won");
        stateManager.dialogueText.dialogueTexts.text = "You have won!";
        stateManager.swapManager.SaveStats(stateManager.playerCurMonster);
    }

    public override void Run()
    {
        //Change scene back to overworld

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Core.CoreManager.Instance.encounterManager.FinishEncounter();
        }

    }


}
