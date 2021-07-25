using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSplayerResolve : BSstate
{

    public BSplayerResolve(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        manager.dialogueText.enableDialogueText(true);
        //Debug.Log("player resolve");
    }

    public override void Run()
    {
        if (!manager.playerHasGone)
        {
            if (manager.playerPriority || manager.aiHasGone) //Player priority or ai has gone
            {
                if (manager.currentAction == 0)
                {
                    DealDamage(20);
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentMove}!";
                    manager.playerHasGone = true;
                }
                else if (manager.currentAction == 1)
                {
                    //use item
                    HealthPotion(50);
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentAction}!";
                    manager.playerHasGone = true;
                }
                else if (manager.currentAction == 2)
                {
                    //swap
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentAction}!";
                    manager.playerHasGone = true;
                }
                else if (manager.currentAction == 3)
                {
                    //escape
                    manager.dialogueText.dialogueTexts.text = $"{manager.monster1.name} uses {manager.currentAction}!"; ;
                    manager.ChangeState(new BSinitialize(manager));
                }
            }
            else
            {
                manager.ChangeState(new BSaiResolve(manager));
            }
        }
        else if (manager.playerHasGone && manager.playerPriority) //Player has gone and player goes first --> Ais turn
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                manager.ChangeState(new BSaiResolve(manager));
            }
        }
        else if (manager.playerHasGone && !manager.playerPriority) //Player has gone and player goes second --- > restart
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                manager.ChangeState(new BSplayerTurn(manager));
                manager.dialogueText.enableDialogueText(false);
            }
        }
    }

    void DealDamage(int damage)
    {
        //Debug.Log($"Player deals {damage} damage");
        manager.mon2curHP -= damage;
        manager.mon2hpbar.fillAmount = (float) manager.mon2curHP / manager.mon2maxHP;
        manager.mon2hpText.text = $"Health: {manager.mon2curHP} / {manager.mon2maxHP}";
        DeathCheck();
    }

    // TODO check if has remaining pokemon
    void DeathCheck()
    {
        if (manager.mon2curHP <= 0)
        {
            Debug.Log("Player wins");
            manager.dialogueText.enableDialogueText(false);
            manager.ChangeState(new BSwon(manager));
        }
    }

    void HealthPotion(int heal)
    {
        Debug.Log($"Player heals for {heal}");
        manager.playerhealthpots -= 1;
        if (manager.mon1curHP + heal > manager.mon1maxHP)
        {
            manager.mon1curHP = manager.mon1maxHP;
        } else
        {
            manager.mon1curHP += heal;
        }
        manager.mon1hpbar.fillAmount = (float)manager.mon1curHP / manager.mon1maxHP;
        manager.mon1hpText.text = $"Health: {manager.mon1curHP} / {manager.mon1maxHP}";
    }

}
