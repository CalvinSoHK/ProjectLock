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
        RevertOriginalPosition();
    }

    public override void Run()
    {
        //Change scene back to overworld

        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Interact, CustomInput.InputEnums.InputAction.Down) && !firedOnce)
        {
            firedOnce = true;
            Core.CoreManager.Instance.encounterManager.FinishEncounterAsync();
        }
    }

    private void RevertOriginalPosition()
    {
        for (int i = 0; i < stateManager.playerParty.PartySize; i++)
        {
            for (int k = 0; k < stateManager.originalPartyOrder.Length; k++)
            {
                if (stateManager.playerParty.GetPartyMember(i) == stateManager.originalPartyOrder[k])
                {
                    stateManager.playerParty.SwapMembers(i, k);
                }
            }
        }
    }

}
