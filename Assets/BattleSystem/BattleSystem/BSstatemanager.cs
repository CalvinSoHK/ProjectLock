using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mon.Enums;
using Mon.Individual;
using Mon.MonGeneration;

public class BSstatemanager : MonoBehaviour
{
    private BSstate currentState;

    public DialogueTexts dialogueText;

    public int currentMove;
    public int currentAction;
    public string currentActionStr;

    public bool playerPriority;

    public int aicurrentAction;

    public MonsterSO monster1;
    public MonsterSO monster2;

    public int mon1curHP;
    public int mon2curHP;
    public int mon1maxHP;
    public int mon2maxHP;

    public Image mon1hpbar;
    public Image mon2hpbar;
    public Text mon1hpText;
    public Text mon2hpText;
    public float hpShrinkSpeed;

    public bool playerHasGone;
    public bool aiHasGone;

    public int aihealthpots;
    public int playerhealthpots;


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
