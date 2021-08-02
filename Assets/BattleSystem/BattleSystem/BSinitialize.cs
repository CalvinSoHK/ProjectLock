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
        
        manager.mon1maxHP = manager.monster1.health;
        manager.mon2maxHP = manager.monster2.health;
        //manager.mon1curHP = manager.mon1maxHP;
        //manager.mon2curHP = manager.mon2maxHP;
        //manager.healthManager.playerCurHP = manager.mon1maxHP;
        //manager.healthManager.aiCurHP = manager.mon2maxHP;

        manager.healthManager.SetUp();

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
