using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public BSstatemanager stateManager;




    public delegate void damageDelegate(PlayerMonster.TrainerMonster monster, int healthValue);

    public static damageDelegate damageEvent;

    /// <summary>
    /// Monster deals damage
    /// subtracts enemey monster name health by damageValue
    /// </summary>
    /// <param name="monName"></param>
    /// <param name="damageValue"></param>
    public void DealDamage(PlayerMonster.TrainerMonster monster, int damageValue) //new param for Skill name? May not need damageValue
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


}
