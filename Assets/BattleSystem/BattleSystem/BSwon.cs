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
        //Change scene back to overworld
    }

    public override void Run()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Core.CoreManager.Instance.encounterManager.FinishEncounter();
        }

    }


}
