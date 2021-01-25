using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Individual;
using Mon.MonGeneration;
using Mon.Enums;
using Mon.Moves;

public enum BattleState {START,PLAYERTURN,BATTLEPHASE,ESCAPED,WON,LOST}
///<Summary>
///BattleSystem for the battle encounters
///</Summary>
public class BattleSystem: MonoBehaviour
{

    [SerializeField] DialogueTexts dialogueText;
    
    public BattleState state;
    int currentAction;
    public int currentMove;
    
    public MonObject mon1;
    public MonObject mon2;
 
    void Start()
    {
        state = BattleState.START;
        mon1 = new MonObject();
        mon2 = new MonObject();
        MonBaseStats teststats1 = new MonBaseStats(100, 100, 100, 100, 100, 100);
        MonBaseStats teststats2 = new MonBaseStats(100, 100, 100, 100, 100, 110);
        mon1.monStats = new MonStats(teststats1);
        mon2.monStats = new MonStats(teststats2);
        Debug.Log(mon1.monStats.GetStat(MonStatType.HP));
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
            //state = BattleState.ENEMYTURN;
            EnemyAction();
        }
    }

    //choosing skill before battleTurn()
    public void PlayerAttack()
    {
        if (state != BattleState.PLAYERTURN) //Set to BATTLEPHASE
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
            //state = BattleState.ENEMYTURN;
            EnemyAction();
        }
    }

    //Choosing skill before battleTurn()
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
    
    void battleTurn()
    {
        //Check conditionals

        //What if 

        //Check if attack has priority
        /*
        if (playerskill.priority > enemyskill.priority)
        {
            PlayerAttack();
            EnemyAttack();
        } else if (playerskill.priority < enemyskill.priority)
        {
            EnemyAttack();
            PlayerAttack();
        } else 
        {
            break;
        }
        */

        //Then do damage
        if (mon1.monStats.GetStat(MonStatType.SPEED) > mon2.monStats.GetStat(MonStatType.SPEED))
        {
            PlayerAttack();
            Debug.Log("Enemy Uses skill");
        } else
        {
            Debug.Log("Enemy uses skill");
            PlayerAttack();
            //enemyAttack();
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


    public void PlayerItem()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //Use item
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
            battleTurn();
        }
    }
}
