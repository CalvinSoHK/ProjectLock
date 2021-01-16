using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<Summary>
///BattleSystem for the player
///</Summary>
public enum BattleState {START,PLAYERTURN,ENEMYTURN,ESCAPED,WON,LOST}
public class BattleSystem: MonoBehaviour
{

    public BattleState state;
    void Start()
    {
        state = BattleState.START;
        Initialize();
    }

    void Initialize()
    {
        //Instantiate Player/Enemy pokemons
        //Set curHP
        //Set curLVL
        state = BattleState.PLAYERTURN;
    }
    void PlayerAction()
    {
        //Player turn
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
}
