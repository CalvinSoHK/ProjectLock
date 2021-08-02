using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMananger : MonoBehaviour
{
    public BSstatemanager stateManager;

    bool isPoisoned = true;

    /// <summary>
    /// Post Resolve
    /// Checks for Condition effect and deals damage
    /// Poison/Burn
    /// </summary>
    public void ConditionEffect()
    {
         if (isPoisoned)
        {
            stateManager.damageManager.DealDamage(stateManager.monster1, PoisonDamage(stateManager.mon1maxHP));
            Debug.Log($"Player takes {PoisonDamage(stateManager.mon1maxHP)} damage");
        }
    }

    /// <summary>
    /// Returns poison tick damage
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <returns></returns>
    int PoisonDamage(int maxHealth)
    {
        return maxHealth / 8;
    }

    /// <summary>
    /// Chance for condition to be removed post resolve
    /// </summary>
    public void ConditionDispel()
    {
        if (isPoisoned)
        {
            if (Random.Range(0,100) < 50)
            {
                isPoisoned = false;
            }
                Debug.Log(isPoisoned);
        }
    }
}
