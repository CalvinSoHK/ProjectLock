using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using Mon.Moves;

public class DamageManager : MonoBehaviour
{
    public BSstatemanager stateManager;




    public delegate void damageDelegate(MonIndObj monster, int healthValue);

    public static damageDelegate damageEvent;

    /// <summary>
    /// Monster deals damage
    /// subtracts enemey monster name health by damageValue
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="damageValue"></param>
    public void DealDamage(MonIndObj monster, int damageValue) //new param for Skill name? May not need damageValue
    {
        if (monster == stateManager.playerCurMonster)
        {
            //Deal Damage
            if (stateManager.healthManager.playerCurHP - damageValue < 0)
            {
                stateManager.healthManager.playerCurHP = 0;
            }
            else
            {
                stateManager.healthManager.playerCurHP -= damageValue;
            }
            damageEvent?.Invoke(monster, stateManager.healthManager.playerCurHP);
        }
        else if (monster == stateManager.aiCurMonster)
        {
            if (stateManager.healthManager.aiCurHP - damageValue < 0)
            {
                stateManager.healthManager.aiCurHP = 0;
            }
            else
            {
                stateManager.healthManager.aiCurHP -= damageValue;
            }
            damageEvent?.Invoke(monster, stateManager.healthManager.aiCurHP);
        }

    }

    /// <summary>
    /// Does calculation for how much damage move deals
    /// Used in DealDamage
    /// </summary>
    /// <param name="monMove"></param>
    /// <returns></returns>
    public int DamageCalculation(MoveData monMove)
    {
        int damageValue = (monMove.power/ (stateManager.playerCurMonster.battleObj.monStats.atk/10));
        Debug.Log(damageValue);
        return damageValue;
    }

}
