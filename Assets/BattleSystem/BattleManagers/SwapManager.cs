using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UnityEngine.UI;
using UI.Party;

public class SwapManager : MonoBehaviour
{
    public BSstatemanager stateManager;

    /// <summary>
    /// Save current stats to somewhere ?
    /// </summary>
    public void SaveStats(MonIndObj monster)
    {
        //Save playerMonCurHP to somewhere? partyManager?
        if (monster == stateManager.playerCurMonster)
        {
            monster.battleObj.monStats.hp = stateManager.healthManager.playerCurHP;
        } else if (monster == stateManager.aiCurMonster)
        {
            monster.battleObj.monStats.hp = stateManager.healthManager.aiCurHP;
        }
    }

    /// <summary>
    /// Swaps to selected Mon and sets up UI, health
    /// </summary>
    /// <param name="selectedMon"></param>
    public void SwapToPlayer(int selectedMon)
    {
        stateManager.playerParty.SwapMembers(0, selectedMon);
        stateManager.playerCurMonster = stateManager.playerParty.GetPartyMember(0);
        stateManager.healthManager.HealthPlayerSetUp(stateManager.playerCurMonster);
        stateManager.monUIManager.SetUp();
    }

    /// <summary>
    /// Swaps to selected Mon and sets up UI, health
    /// </summary>
    /// <param name="selectedMon"></param>
    public void SwapToAI(int selectedMon)
    {
        stateManager.aiCurMonster = stateManager.aiParty.GetPartyMember(selectedMon);
        stateManager.healthManager.HealthAISetUp(stateManager.aiCurMonster);
        stateManager.monUIManager.SetUp();
        Debug.Log("swapped to " + selectedMon);
    }
}
