using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checking for conditions post turn
/// Poison? Etc
/// </summary>
public class BSpostResolve : BSstate
{
    public BSpostResolve(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateManager.conditionManager.ConditionEffect();
        stateManager.conditionManager.ConditionDispel();
        stateManager.dialogueText.enableDialogueText(false);
        stateManager.ChangeState(new BSplayerTurn(stateManager));
    }

    public override void Run()
    {

    }

}
