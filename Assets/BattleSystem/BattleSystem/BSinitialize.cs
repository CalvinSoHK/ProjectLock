using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;
using Mon.Individual;
using Mon.MonGeneration;

public class BSinitialize : BSstate
{
    public BSinitialize(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        //Debug.Log(manager.mon1.monStats.GetStat(MonStatType.HP));
        //manager.mon1maxHP = manager.mon1.monStats.GetStat(MonStatType.HP);
        manager.mon1maxHP = manager.monster1.health;
        manager.mon2maxHP = manager.monster2.health;
        manager.mon1curHP = manager.mon1maxHP;
        manager.mon2curHP = manager.mon2maxHP;

        manager.mon1hpbar.fillAmount = (float)manager.mon1curHP / manager.mon1maxHP;
        manager.mon2hpbar.fillAmount = (float)manager.mon2curHP / manager.mon2maxHP;

        manager.mon1hpText.text = $"Health: {manager.mon1curHP} / {manager.mon1maxHP}";
        manager.mon2hpText.text = $"Health: {manager.mon2curHP} / {manager.mon2maxHP}";

        manager.aihealthpots = 2; //SO test
        manager.playerhealthpots = 2;
        Debug.Log("Initializing");
    }

    public override void Run()
    {
        Debug.Log("Exiting initialize");
        manager.ChangeState(new BSplayerTurn(manager));
    }

}   
