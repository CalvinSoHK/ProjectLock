using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;

public class HealthManager : MonoBehaviour
{
    private BSstatemanager stateManager;
    public int playerMaxHP;
    public int playerCurHP;

    public int aiMaxHP;
    public int aiCurHP;


    /// <summary>
    /// Sets up Health for player
    /// </summary>
    /// <param name="monster"></param>
    public void HealthPlayerSetUp(MonIndObj monster)
    {
        playerMaxHP = monster.battleObj.monStats.hp;
        playerCurHP = monster.battleObj.monStats.hp;
    }

    /// <summary>
    /// Sets up Health for AI
    /// </summary>
    /// <param name="monster"></param>
    public void HealthAISetUp(MonIndObj monster)
    {
        aiMaxHP = monster.battleObj.monStats.hp;
        aiCurHP = monster.battleObj.monStats.hp;
    }


}
