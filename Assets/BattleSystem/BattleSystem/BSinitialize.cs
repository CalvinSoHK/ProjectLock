using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Mon.MonData;

public class BSinitialize : BSstate
{
    public BSinitialize(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        Debug.Log("Initializing");
        stateManager.uiManager = CoreManager.Instance.uiManager;
        Debug.Log("UIManager: " + stateManager.uiManager);
        stateManager.playerParty = CoreManager.Instance.playerParty.party;
        Debug.Log("Player Party: " + stateManager.playerParty);
        stateManager.aiParty = CoreManager.Instance.encounterManager.EncounterInfo.party;
        Debug.Log("Ai Party: " + stateManager.aiParty);

        //Save Locations of playerParty
        SavePartyIndex();

        Debug.Log(stateManager.captureBall.name);
        //Check for encounterType? Trainer or wild

        //Reference each players current Mon
        stateManager.playerCurMonster = stateManager.playerParty.GetFirstValidCombatant();
        if (stateManager.aiParty.GetFirstValidCombatant() == null)
        {
            Debug.LogError("null");
        }
        stateManager.aiCurMonster = stateManager.aiParty.GetFirstValidCombatant();

        //Setup mon health Cur/Max
        stateManager.healthManager.HealthPlayerSetUp(stateManager.playerCurMonster);
        stateManager.healthManager.HealthAISetUp(stateManager.aiCurMonster);
        
        //Fix nicknames

        stateManager.monUIManager.SetUp();
         
        //Testing Remove when implement inventory
        stateManager.aihealthpots = 2;
        stateManager.playerhealthpots = 2;

        //Test Code
        //CoreManager.Instance.encounterManager.encounteredMon = new Mon.MonData.MonIndObj(CoreManager.Instance.dexManager.GetMonByID(1),1);
        //manager.mon2maxHP = CoreManager.Instance.encounterManager.encounteredMon.stats.hp;
    }

    public override void Run()
    {
        Debug.Log("Exiting initialize");
        stateManager.ChangeState(new BSplayerTurn(stateManager));
        return;

    }

    private void SavePartyIndex()
    {
        for (int i = 0; i < stateManager.originalPartyOrder.Length; i++)
        {
            stateManager.originalPartyOrder[i] = stateManager.playerParty.GetPartyMember(i);
        }
    }
}   
