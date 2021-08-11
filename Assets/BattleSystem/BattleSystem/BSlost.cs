using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSlost : BSstate
{
    public BSlost(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        Debug.Log("Lost");
        stateManager.dialogueText.dialogueTexts.text = "You have lost.";
        //Change scenes here
    }

    public override void Run()
    {

    }
}
