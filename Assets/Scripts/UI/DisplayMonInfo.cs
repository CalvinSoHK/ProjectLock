using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonGeneration;
using Mon.Enums;
using UnityEngine.UI;

public class DisplayMonInfo : MonoBehaviour
{
    private int level = 100;

    [SerializeField]
    Text id, monName, type1, type2, stage, growth, catchRate, weight, exp, evolution, previous, hp, def, spDef, atk, spAtk, speed, lvl, total;

    [SerializeField]
    Button prevButton, nextButton;
     
    int hpCalc, defCalc, spDefCalc, atkCalc, spAtkCalc, speedCalc, totalCalc;

    public MonGenerator generator;

    public GeneratedMon displayMon;

    private int currentID = 1;

    private void Update()
    {
        if (generator.monDex.CheckValidID(currentID))
        {
            displayMon = generator.monDex.GetMonByID(currentID);
            CheckButtonActive();

            lvl.text = level.ToString();
            id.text = displayMon.ID.ToString();
            monName.text = displayMon.name;
            type1.text = displayMon.primaryType.ToString();
            type2.text = displayMon.secondaryType.ToString();
            stage.text = displayMon.stage.ToString();
            growth.text = displayMon.growthType.ToString();
            catchRate.text = displayMon.catchRate.ToString();
            weight.text = displayMon.weight.ToString();
            exp.text = displayMon.exp_gain.ToString();
            evolution.text = generator.monDex.CheckValidID(displayMon.next_evoID) ?
                generator.monDex.GetMonByID(displayMon.next_evoID).name + 
                " " + generator.monDex.GetMonByID(displayMon.next_evoID).ID : "";
            previous.text = generator.monDex.CheckValidID(displayMon.prev_evoID) ?
                generator.monDex.GetMonByID(displayMon.prev_evoID).name +
                " " + generator.monDex.GetMonByID(displayMon.prev_evoID).ID : "";


            hpCalc = Mathf.RoundToInt((level / 100f * displayMon.baseStats.GetStat(MonStatType.HP)));
            defCalc = Mathf.RoundToInt((level / 100f * displayMon.baseStats.GetStat(MonStatType.DEF)));
            spDefCalc = Mathf.RoundToInt((level / 100f * displayMon.baseStats.GetStat(MonStatType.SPDEF)));
            atkCalc = Mathf.RoundToInt((level / 100f * displayMon.baseStats.GetStat(MonStatType.ATK)));
            spAtkCalc = Mathf.RoundToInt((level / 100f * displayMon.baseStats.GetStat(MonStatType.SPATK)));
            speedCalc = Mathf.RoundToInt((level / 100f * displayMon.baseStats.GetStat(MonStatType.SPEED)));
            totalCalc = hpCalc + defCalc + spDefCalc + atkCalc + spAtkCalc + speedCalc;

            hp.text = hpCalc.ToString();
            def.text = defCalc.ToString();
            spDef.text = spDefCalc.ToString();
            atk.text = atkCalc.ToString();
            spAtk.text = spAtkCalc.ToString();
            speed.text = speedCalc.ToString();
            total.text = totalCalc.ToString();
        }
    }

    public void IncrementID()
    {
        currentID++;
    }

    public void DecrementID()
    {
        currentID--;
    }

    public void ResetID()
    {
        currentID = 1;
    }

    public void UpdateLevel(Slider slider)
    {
        level = Mathf.RoundToInt(slider.value);
    }

    public void CheckButtonActive()
    {
        if (!generator.monDex.CheckValidID(currentID + 1))
        {
            nextButton.gameObject.SetActive(false);            
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }

        if (currentID - 1 <= 0 || !generator.monDex.CheckValidID(currentID - 1))
        {
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
        }
    }
}
