using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int playerMaxHP;
    public int playerCurHP;

    public int aiMaxHP;
    public int aiCurHP;


    /// <summary>
    /// Sets up Health for player
    /// </summary>
    /// <param name="monster"></param>
    public void HealthPlayerSetUp(PlayerMonster.TrainerMonster monster)
    {
        playerMaxHP = monster.monMaxHealth;
        playerCurHP = monster.monCurHealth;
    }

    /// <summary>
    /// Sets up Health for AI
    /// </summary>
    /// <param name="monster"></param>
    public void HealthAISetUp(PlayerMonster.TrainerMonster monster)
    {
        aiMaxHP = monster.monMaxHealth;
        aiCurHP = monster.monCurHealth;
    }
}
