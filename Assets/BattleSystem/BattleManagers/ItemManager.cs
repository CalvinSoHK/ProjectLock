using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public BSstatemanager stateManager;

    bool healItem = true;




    public delegate void healDelegate(int healthValue);

    public static healDelegate healEvent;

    
    public void UseItem(MonsterSO monster) //Arg for Item?
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
    void HealItem(MonsterSO monster, int healAmount)
    {
        if (monster.name == stateManager.monster1.name)
        {
            stateManager.playerhealthpots -= 1;
            if (stateManager.healthManager.playerCurHP + healAmount > stateManager.mon1maxHP)
            {
                stateManager.healthManager.playerCurHP = stateManager.mon1maxHP;
            }
            else
            {
                stateManager.healthManager.playerCurHP += healAmount;
            }
            healEvent?.Invoke(stateManager.healthManager.playerCurHP);
        } else if (monster.name == stateManager.monster2.name)
        {
            stateManager.aihealthpots -= 1;
            if (stateManager.healthManager.aiCurHP + healAmount > stateManager.mon2maxHP)
            {
                stateManager.healthManager.aiCurHP = stateManager.mon2maxHP;
            }
            else
            {
                stateManager.healthManager.aiCurHP += healAmount;
            }
            healEvent?.Invoke(stateManager.healthManager.aiCurHP);
        }
    }
}
