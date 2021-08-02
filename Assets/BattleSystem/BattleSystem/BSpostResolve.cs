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
        manager.conditionManager.ConditionEffect();
        manager.conditionManager.ConditionDispel();
        manager.ChangeState(new BSplayerTurn(manager));
        manager.dialogueText.enableDialogueText(false);
    }

    public override void Run()
    {

    }

}
