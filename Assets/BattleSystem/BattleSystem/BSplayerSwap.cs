using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Party;
using UI.Dropdown;
using Core.MessageQueue;

public class BSplayerSwap : BSstate
{
    public BSplayerSwap(BSstatemanager theManager) : base(theManager)
    {

    }

    int currentSelectedIndex;

    public override void Enter()
    {
        PartyElementUI.PartySelectFire += OnSelect;
        MessageQueue.MessageEvent += HandleMessage;

        //Set data? from playerParty
        stateManager.swapManager.SaveStats(stateManager.playerParty.GetPartyMember(0));
        for (int i = 0; i < stateManager.playerParty.PartySize; i++)
        {
            stateManager.uiManager.PartyBattleCheck(i, stateManager.playerParty.GetPartyMember(i));
        }
        stateManager.uiManager.PartyEnable();
    }

    private void HandleMessage (string _id, string _msg)
    {
        if (_id.Equals("UI"))
        {
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Battle)
            {
                if (_msg == "Swap")
                {
                    //Change cant swap with self
                    if (stateManager.healthManager.playerCurHP <= 0) // If new selected and current mon is dead
                    {
                        //What happens if died to Condition at end of turn
                        //Check by hasgone?
                        Debug.Log("Dead select new mon");
                        if (SelectMonCheck())
                        {
                            //stateManager.swapManager.currentActiveMon = currentSelectedIndex;
                            stateManager.swapManager.SwapToPlayer(currentSelectedIndex);
                            stateManager.uiManager.PartyDisable();
                            //firstIteration = true;
                            stateManager.ChangeState(new BSpostResolve(stateManager));
                            return;
                        }
                    }
                    else
                    {
                        if (SelectMonCheck())
                        {
                            stateManager.currentSelectedMon = currentSelectedIndex;
                            stateManager.uiManager.PartyDisable();
                            //firstIteration = true;
                            stateManager.ChangeState(new BSaiTurn(stateManager));
                            return;
                        }
                    }
                }
            }
        }
    }

    public override void Run()
    {

        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Interact, CustomInput.InputEnums.InputAction.Down))
        {
            stateManager.ChangeState(new BSplayerTurn(stateManager));
            stateManager.uiManager.PartyDisable();
            return;
        }
    }

    public override void Leave()
    {
        PartyElementUI.PartySelectFire -= OnSelect;
        MessageQueue.MessageEvent -= HandleMessage;
    }

    /// <summary>
    /// Checks if player selected Mon is allowed
    /// </summary>
    bool SelectMonCheck()
    {
        //Personal ID for every Mon of player?
        //First Caught = 1?
        //2nd = 2
        if (stateManager.playerParty.GetPartyMember(currentSelectedIndex) == stateManager.playerCurMonster)
        {
            Debug.Log("Already Selected");
            return false;
        }
        else if (stateManager.playerParty.GetPartyMember(currentSelectedIndex).battleObj.monStats.hp <= 0)
        {
            Debug.Log("Not Alive");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnSelect(string _key, int selectedMonIndex)
    {
        if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Battle)
        { 
            if (_key.Equals("Party"))
            {
                currentSelectedIndex = selectedMonIndex;
            }
        }
    }
}
