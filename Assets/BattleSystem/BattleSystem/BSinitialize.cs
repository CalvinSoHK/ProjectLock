using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class BSinitialize : BSstate
{
    public BSinitialize(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {

        Debug.Log("Initializing");
        
        manager.playerMonManager.SetUpValues();
        manager.aiMonManager.SetUpValues();
        manager.playerCurMonster = manager.playerMonManager.FirstAvailable();
        manager.aiCurMonster = manager.aiMonManager.FirstAvailable();



        manager.healthManager.HealthPlayerSetUp(manager.playerCurMonster);
        manager.healthManager.HealthAISetUp(manager.aiCurMonster);

        Debug.Log(manager.healthManager.playerCurHP);

        manager.monUIManager.SetUp();

        //Testing Remove when implement inventory
        manager.aihealthpots = 2;
        manager.playerhealthpots = 2;

        //Test Code
        CoreManager.Instance.encounterManager.encounteredMon = new Mon.MonData.MonIndObj(CoreManager.Instance.dexManager.GetMonByID(1),1);
        //manager.mon2maxHP = CoreManager.Instance.encounterManager.encounteredMon.stats.hp;
    }

    public override void Run()
    {
        Debug.Log("Exiting initialize");
        //Debug.Log(CoreManager.Instance.encounterManager.encounteredMon.stats.hp);
        //Debug.Log((float)manager.mon2curHP / manager.mon2maxHP);
        manager.ChangeState(new BSplayerTurn(manager));

    }

}   
