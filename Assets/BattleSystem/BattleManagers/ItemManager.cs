using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using Inventory;
using Storage;

public class ItemManager : MonoBehaviour
{

    public BSstatemanager stateManager;

    bool healItem = true;

    public delegate void healDelegate(MonIndObj monster, int healthValue);

    public static healDelegate healEvent;

    /// <summary>
    /// Use item
    /// </summary>
    /// <param name="monster"></param>
    public void UseItem(MonIndObj monster) //Arg for Item? tag?
    {
        //Temp
        if (healItem)
        {
            HealItem(monster, 15);
        }
    }

    /// <summary>
    /// When mon uses an item, heals monster for healAmount
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="healAmount"></param>
    void HealItem(MonIndObj monster, int healAmount)
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


    /// <summary>
    /// Player uses item
    /// </summary>
    /// <param name="item"></param>
    public void UserItem(string item)
    {
        if (Core.CoreManager.Instance.playerInventory.Inventory.HasItem(item))
        {
            Core.CoreManager.Instance.playerInventory.Inventory.UseItem(item);
            
            if (item == stateManager.captureBall.name)
            {
                CatchhMon();
                Debug.Log("Caught Mon");
                stateManager.ChangeState(new BSwon(stateManager));
                return;

            }
        }
    }

    /// <summary>
    /// Catches mon
    /// Adds to Player Storage if no slot is available
    /// </summary>
    private void CatchhMon()
    {
        stateManager.swapManager.SaveStats(stateManager.aiCurMonster);

        if (!stateManager.playerParty.AddMember(stateManager.aiCurMonster))
        {
            for (int i = 0; i < Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList.Count; i++)
            {
                if (Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList[i].HasSpace())
                {
                    Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList[i].AddMonToFreeSlot(stateManager.aiCurMonster);
                    break;
                }
                else
                {
                    Debug.Log("No space going to try next box");
                }
            }
        }
    }
}
