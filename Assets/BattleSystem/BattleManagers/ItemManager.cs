using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public BSstatemanager stateManager;

    bool healItem = true;




    public delegate void healDelegate(PlayerMonster.TrainerMonster monster, int healthValue);

    public static healDelegate healEvent;

    
    public void UseItem(PlayerMonster.TrainerMonster monster) //Arg for Item? tag?
    {
        //Temp
        if (healItem)
        {
            HealItem(monster, 50);
        }
    }

    /// <summary>
    /// When mon uses an item, heals monster for healAmount
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="healAmount"></param>
    void HealItem(PlayerMonster.TrainerMonster monster, int healAmount)
    {
        if (monster == stateManager.playerCurMonster)
        {
            stateManager.playerhealthpots -= 1;
            if (stateManager.healthManager.playerCurHP + healAmount > stateManager.healthManager.playerMaxHP)
            {
                stateManager.healthManager.playerCurHP = stateManager.healthManager.playerMaxHP;
            }
            else
            {
                stateManager.healthManager.playerCurHP += healAmount;
            }
            healEvent?.Invoke(monster, stateManager.healthManager.playerCurHP);
        } else if (monster == stateManager.aiCurMonster)
        {
            stateManager.aihealthpots -= 1;
            if (stateManager.healthManager.aiCurHP + healAmount > stateManager.healthManager.aiMaxHP)
            {
                stateManager.healthManager.aiCurHP = stateManager.healthManager.aiMaxHP;
            }
            else
            {
                stateManager.healthManager.aiCurHP += healAmount;
            }
            healEvent?.Invoke(monster, stateManager.healthManager.aiCurHP);
        }
    }
}
