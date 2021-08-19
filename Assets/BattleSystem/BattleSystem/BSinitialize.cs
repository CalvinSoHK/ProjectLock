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
        stateManager.playerMonManager = CoreManager.Instance.playerParty;
        Debug.Log("Player Party: " + stateManager.playerMonManager);
        stateManager.aiMonManager = CoreManager.Instance.encounterManager;
        Debug.Log("Ai Party: " + stateManager.aiMonManager);
        

        //Debug.Log(stateManager.typeRelation.typeRelationSO);
        //Check for encounterType? Trainer or wild
        stateManager.swapManager.SwapScreenSetUp();

        //Reference each players current Mon
        stateManager.playerCurMonster = stateManager.playerMonManager.GetFirstValidCombatant();
        stateManager.aiCurMonster = stateManager.aiMonManager.EncounteredMon;

        stateManager.aiDecisionMove.MoveSelection();

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

    }

}   
