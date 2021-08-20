using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UnityEngine.UI;

public class SwapManager : MonoBehaviour
{
    public BSstatemanager stateManager;
    
    [SerializeField]
    public List<MonIndObj> playerParty;

    [SerializeField]
    public List<Text> playerMon;

    //Index of current mon
    public int currentDisplayedMon;

    // Player clicks selects monsterSelect (0-5)
    // Changes from playerMonster[currentMonster] to playerMonster[monsterSelect]
    // monsterSelect = currentMonster
    // Display info in MonUIManager

    /// <summary>
    /// Loads party members onto swap screen
    /// Used in initialize
    /// </summary>
    public void SwapScreenSetUp()
    {
        AddToList();
        ChangeText();
    }

    /// <summary>
    /// Adds Party Members from PartyManager to playerMon list
    /// </summary>
    void AddToList()
    {
        for (int i = 0; i < playerMon.Count; i++)
        {
            if (stateManager.playerParty.GetPartyMember(i) != null)
            {
                playerParty.Add(stateManager.playerParty.GetPartyMember(i));
            }
        }    
    }

    /// <summary>
    /// Changes texts of mon in swap screen
    /// </summary>
    void ChangeText()
    {
        for (int i = 0; i < playerParty.Count; i++)
        {
            if (playerParty[i] != null)
            {
                playerMon[i].text = playerParty[i].baseMon.name;
                playerMon[i].gameObject.SetActive(true);
            } else
            {
                playerMon[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Displays Selected Mon in swap screen
    /// </summary>
    /// <param name="selectedMon"></param>
    public void updateMonSelection(int selectedMon)
    {
        for (int i = 0; i < playerParty.Count; i++)
        {
            if (i == selectedMon)
            {
                playerMon[i].color = Color.red;
            }
            else
            {
                playerMon[i].color = Color.black;
            }
        }
    }

    /// <summary>
    /// Save current stats to somewhere ?
    /// </summary>
    public void SaveStats(MonIndObj monster)
    {
        //Save playerMonCurHP to somewhere? partyManager?
        monster.battleObj.monStats.hp = stateManager.healthManager.playerCurHP;
    }

    /// <summary>
    /// Swaps to selected Mon and sets up UI, health
    /// </summary>
    /// <param name="selectedMon"></param>
    public void SwapTo(int selectedMon)
    {
        stateManager.playerCurMonster = stateManager.playerParty.GetPartyMember(selectedMon);
        stateManager.healthManager.HealthPlayerSetUp(stateManager.playerCurMonster);
        stateManager.monUIManager.SetUp();
    }
}
