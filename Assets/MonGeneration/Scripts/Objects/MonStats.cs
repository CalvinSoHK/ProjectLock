using Mon.MonGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;

public class MonStats
{
    protected int hp, def, spDef, atk, spAtk, speed;

    public MonStats(MonBaseStats baseStats)
    {
        hp = baseStats.GetStat(MonStatType.HP);
        def = baseStats.GetStat(MonStatType.DEF);
        spDef = baseStats.GetStat(MonStatType.SPDEF);
        atk = baseStats.GetStat(MonStatType.ATK);
        spAtk = baseStats.GetStat(MonStatType.SPATK);
        speed = baseStats.GetStat(MonStatType.SPEED);
    }

    /// <summary>
    /// Applies base values for each stats.
    /// This means that if the base stat for a Mon is 1, it's value at level 1 isn't 1.
    /// It will be the base value + 1.
    /// </summary>
    private void ApplyStatFormula()
    {
        CalculateHP();
        CalculateDEF();
        CalculateSPDEF();
        CalculateATK();
        CalculateSPATK();
        CalculateSPEED();
    }

    private void CalculateHP()
    {

    }

    private void CalculateDEF()
    {

    }

    private void CalculateSPDEF()
    {

    }

    private void CalculateATK()
    {

    }

    private void CalculateSPATK()
    {

    }

    private void CalculateSPEED()
    {

    }
}
