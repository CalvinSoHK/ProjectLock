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
        manager.mon1 = new MonObject();
        manager.mon2 = new MonObject();
        //MonBaseStats(HP, - , - , - , - , speed)
        MonBaseStats teststats1 = new MonBaseStats(100, 100, 100, 100, 100, 100);
        MonBaseStats teststats2 = new MonBaseStats(80, 100, 100, 100, 100, 110);
        manager.mon1.monStats = new MonStats(teststats1);
        manager.mon2.monStats = new MonStats(teststats2);
        //Debug.Log(manager.mon1.monStats.GetStat(MonStatType.HP));
        manager.mon1maxHP = manager.mon1.monStats.GetStat(MonStatType.HP);
        manager.mon2maxHP = manager.mon2.monStats.GetStat(MonStatType.HP);
        manager.mon1curHP = manager.mon1maxHP;
        manager.mon2curHP = manager.mon2maxHP;

        manager.mon1hpbar.fillAmount = (float)manager.mon1curHP / manager.mon2maxHP;
        manager.mon2hpbar.fillAmount = (float)manager.mon2curHP / manager.mon2maxHP;

        manager.mon1hpText.text = $"Health: {manager.mon1curHP} / {manager.mon1maxHP}";
        manager.mon2hpText.text = $"Health: {manager.mon2curHP} / {manager.mon2maxHP}";

        manager.aihealthpots = 2; //SO test
        Debug.Log("Initializing");
    }

    public override void Run()
    {
        Debug.Log("Exiting initialize");
        manager.ChangeState(new BSplayerTurn(manager));
    }

}   
