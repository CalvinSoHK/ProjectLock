using Mon.Enums;
using Mon.Individual;
using Mon.MonGeneration;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum BattleState { START, PLAYERTURN, BATTLEPHASE, ESCAPED, WON, LOST }
///<Summary>
///BattleSystem for the battle encounters
///</Summary>
public class BattleSystem : MonoBehaviour
{

    [SerializeField] DialogueTexts dialogueText;

    public BattleState state;
    int currentAction;
    public int currentMove;
    public int enemyMove;

    public MonObject mon1;
    public MonObject mon2;

    //TODO
    //Change curHP when switching mons?
    //Set curHP when entering battle instead
    int mon1curHP;
    int mon2curHP;
    int mon1maxHP;
    int mon2maxHP;

    public Image mon1hpbar;
    public Image mon2hpbar;

    void Start()
    {
        state = BattleState.START;
        mon1 = new MonObject();
        mon2 = new MonObject();
        //MonBaseStats(HP, - , - , - , - , speed)
        MonBaseStats teststats1 = new MonBaseStats(100, 100, 100, 100, 100, 100);
        MonBaseStats teststats2 = new MonBaseStats(80, 100, 100, 100, 100, 110);
        mon1.monStats = new MonStats(teststats1);
        mon2.monStats = new MonStats(teststats2);
        Debug.Log(mon1.monStats.GetStat(MonStatType.HP));
        mon1maxHP = mon1.monStats.GetStat(MonStatType.HP);
        mon2maxHP = mon2.monStats.GetStat(MonStatType.HP);
        mon1curHP = mon1maxHP;
        mon2curHP = mon2maxHP;
        Initialize();
    }



    void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            moveSelection();
        }

/*        if (Input.GetKey(KeyCode.Space))
        {
            dialogueText.enableDialogueText(true);
            dialogueText.enableMoveSelector(false);
        }*/
    }
    void Initialize()
    {
        //Instantiate Player/Enemy pokemons
        //Set curHP
        //Set curLVL

        //Decide who goes first based on speed

        Debug.Log("Initlaizing");
        {
            state = BattleState.PLAYERTURN;
            PlayerAction();
        }

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

        if (canEscape)
        {
            state = BattleState.ESCAPED;
            Escaped();
        }
        else
        {
            //state = BattleState.ENEMYTURN;
            enemyAttack();
        }
    }

    /// <summary>
    /// Takes move chosen in moveSelection()
    /// Deals damage to enemy
    /// </summary>
    public void PlayerAttack()
    {
        if (state != BattleState.PLAYERTURN) //Set to BATTLEPHASE
        {
            return;
        }

        switch (currentMove)
        {
            case 0:
                Debug.Log("Player skill 1");
                //dealDamage((attackPower + formula))
                dealDamage(50, "Attack 1");
                //Use skill 1
                break;
            case 1:
                Debug.Log("player skill 2");
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
    }

    //Choosing skill before battleTurn()
    void enemyAttack()
    {
        enemyMove = Random.Range(0, 3);
        switch (enemyMove)
        {
            case 0:
                Debug.Log("Enemy skill 1");
                break;
            case 1:
                Debug.Log("Enemy skill 2");
                break;
            case 2:
                Debug.Log("Enemy skill 3");
                break;
            case 3:
                Debug.Log("Enemy skill 4");
                break;
            default:
                break;
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

        if (mon1.monStats.GetStat(MonStatType.SPEED) > mon2.monStats.GetStat(MonStatType.SPEED))
        {
            PlayerAttack();
            isAlive();
            // Check ifAlive()
            Debug.Log("Enemy Uses skill (Goes second)");
            enemyAttack();
            isAlive();
            //Deal Damage
        }
        else
        {
            Debug.Log("Enemy uses skill (Goes first)");
            enemyAttack();
            isAlive();
            PlayerAttack();
            Debug.Log(mon2curHP);
            isAlive();
        }




    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("Won");
            //EXP up
            //Money
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("Lost");
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

    void dealDamage(int damage, string name)
    {
        Debug.Log("User does " + damage + " damage with" + name);
        //Check whether dead
        mon2curHP -= damage;
        Debug.Log((float)mon2curHP / mon2maxHP);
        mon2hpbar.fillAmount = (float)mon2curHP / mon2maxHP;

    }

    void isAlive()
    {
        if (mon1curHP <= 0)
        {
            //Check if player1 has pokemon
            state = BattleState.LOST;
            EndBattle();
        }
        if (mon2curHP <= 0)
        {
            //Check if player2 has pokemon
            state = BattleState.WON;
            EndBattle();
        }
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
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
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
