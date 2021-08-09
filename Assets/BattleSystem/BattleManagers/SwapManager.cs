using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapManager : MonoBehaviour
{
    public BSstatemanager stateManager;

    int currentMonster;
    int monsterSelect;
    // Player clicks selects monsterSelect (0-5)
    // Changes from playerMonster[currentMonster] to playerMonster[monsterSelect]
    // monsterSelect = currentMonster
    // Display info in MonUIManager

    public PlayerMonster.TrainerMonster GetPlayerMonster(int i)
    {
        return stateManager.playerMonManager.playerMonster[i];
    }

    public void SwapTo()
    {
        Debug.Log(stateManager.playerMonManager.playerMonster[0].monName);
        Debug.Log(GetPlayerMonster(1).monName);
        ActivePokemonUI(GetPlayerMonster(1));
        Debug.Log("Swapped");
        
    }

    void ActivePokemonUI(PlayerMonster.TrainerMonster playerMon)
    {
        stateManager.playerCurMonster = playerMon;
        stateManager.healthManager.HealthPlayerSetUp(playerMon);
        Debug.Log("ActivePokemonUI" + playerMon.monCurHealth);
        stateManager.monUIManager.SetUp();
    }

}
