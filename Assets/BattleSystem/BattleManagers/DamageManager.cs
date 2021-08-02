using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public BSstatemanager stateManager;




    public delegate void damageDelegate(int healthValue);

    public static damageDelegate damageEvent;

    /// <summary>
    /// Monster deals damage
    /// subtracts enemey monster name health by damageValue
    /// </summary>
    /// <param name="monName"></param>
    /// <param name="damageValue"></param>
    public void DealDamage(MonsterSO monster, int damageValue) //new param for Skill name? May not need damageValue
    {
        if (monster.name == stateManager.monster1.name)
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
            damageEvent?.Invoke(stateManager.healthManager.playerCurHP);
        }
        else if (monster.name == stateManager.monster2.name)
        {
            if (stateManager.healthManager.aiCurHP - damageValue < 0)
            {
                stateManager.healthManager.aiCurHP = 0;
            }
            else
            {
                stateManager.healthManager.aiCurHP -= damageValue;
            }
            damageEvent?.Invoke(stateManager.healthManager.aiCurHP);
        }


    }


}
