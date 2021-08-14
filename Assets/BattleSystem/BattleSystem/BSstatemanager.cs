using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mon.MonData;
using World;
using Core.Player;

public class BSstatemanager : MonoBehaviour
{
    private BSstate currentState;

    [Header("Managers")]
    public MonUIManager monUIManager;
    public HealthManager healthManager;
    public DamageManager damageManager;
    public DialogueTexts dialogueText;
    public ItemManager itemManager;
    public ConditionMananger conditionManager;
    public SwapManager swapManager;
    public PartyManager playerMonManager;
    public EncounterManager aiMonManager;

    public int currentMove;
    public int currentAction;
    public string currentActionStr;
    public int currentSelectedMon;

    public bool playerPriority;

    public int aiCurrentAction;
    public int aiCurrentMove;

    public MonIndObj playerCurMonster;
    public MonIndObj aiCurMonster;

    public GameObject swapScreen;

    public bool playerHasGone;
    public bool aiHasGone;

    //Temp
    public int aihealthpots;
    public int playerhealthpots;

    //Testing Condition
    public bool isPoisoned = true;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new BSinitialize(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Run();

    }

    public void ChangeState(BSstate newState)
    {
        if (currentState != null)
        {
            currentState.Leave();
        }

        currentState = newState;
        currentState.Enter();
    }

}


