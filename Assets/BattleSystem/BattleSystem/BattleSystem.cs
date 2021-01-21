using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Individual;
using Mon.MonGeneration;
using Mon.Enums;

///<Summary>
///BattleSystem for the player
///</Summary>
public enum BattleState {START,PLAYERTURN,ENEMYTURN,ESCAPED,WON,LOST}
public class BattleSystem: MonoBehaviour
{

    [SerializeField] DialogueTexts dialogueText;
    
    public BattleState state;
    int currentAction;
    public int currentMove;
    
    public MonObject pokemon1;
    void Start()
    {
        state = BattleState.START;
        pokemon1 = new MonObject();
        MonBaseStats teststats = new MonBaseStats(100, 100, 100, 100, 100, 100);
        pokemon1.monStats = new MonStats(teststats);
        Debug.Log(pokemon1.monStats.GetStat(MonStatType.HP));
        Initialize();
    }



    void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            moveSelection();
        }
    }
    void Initialize()
    {
        //Instantiate Player/Enemy pokemons
        //Set curHP
        //Set curLVL

        //Decide who goes first based on speed

        //if ((player)speed > (enemy)speed)
        Debug.Log("Initlaizing");
        {
            state = BattleState.PLAYERTURN;
            PlayerAction();
        } /* else
        {
            state = BattleState.ENEMYTURN;
            PlayerAction();
        }*/

    }
    void PlayerAction()
    {
        //Player turn
        Debug.Log("SelectMove");

    }

    public void PlayerRun()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //Chance to escape
        bool canEscape = true;

        if(canEscape)
        {
            state = BattleState.ESCAPED;
            Escaped();
        } else
        {
            state = BattleState.ENEMYTURN;
            EnemyAction();
        }
    }
    public void PlayerAttack()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        switch (currentMove)
        {
            case 0:
                Debug.Log("skill 1");
                //
                //BattleTurn()
                //Use skill 1
                break;
            case 1:
                Debug.Log("skill 2");
                //Use skill 2
                break;
            case 2:
                Debug.Log("skill 3");
                //Use skill 3
                break;
            case 3:
                Debug.Log("skill 4");
                //Use skill 4
                break;
            default:
                break;
        }


        //Deal damage
        bool isDead = false;
        //Check if enemy dead -> state = won
        if (isDead)
        {
            state = BattleState.WON; //Win state
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            EnemyAction();
        }
    }


    public void PlayerItem()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //Use item
    }

    void EnemyAction()
    {
        bool isDead = false;
        //Check if player dead -> state = lost
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerAction();
        }

    }

    void BattleTurn()
    {
        //Check conditionals


        //Check if attack has priority
        //Then do damage
        //if (player.speed > enemy.speed)
        {
            
        }

    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            //EXP up
            //Money
        } else if (state == BattleState.LOST)
        {
            //Game over
        }
    }

    void Escaped()
    {
        //text = "Player ran away"
    }

    void moveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < 3) //max moves - 1
            {
                ++currentMove;
            } 
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentMove > 0)
                {
                    --currentMove;
                }
            }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentMove < 2)
                {
                    currentMove += 2;
                }
            } else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentMove > 1)
                {
                    currentMove -= 2;
                }
            }
        
        dialogueText.updateMoveSelection(currentMove);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerAttack();
        }
    }
}
