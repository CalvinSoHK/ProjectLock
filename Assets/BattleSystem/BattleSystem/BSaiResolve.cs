using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSaiResolve : BSstate
{


    public BSaiResolve(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        manager.dialogueText.enableDialogueText(true);
        //Debug.Log("Ai resolve");
    }

    public override void Run()
    {
        if (!manager.aiHasGone)
        {
            if (!manager.playerPriority || manager.playerHasGone)
            {

                if (manager.aicurrentAction == 0)
                {
                    DealDamage(10);
                    manager.dialogueText.dialogueTexts.text = $"Ai uses {manager.aicurrentAction}!";
                    manager.aiHasGone = true;
                } 
                else if (manager.aicurrentAction == 1)
                {
                    HealthPotion(50);
                    manager.dialogueText.dialogueTexts.text = $"Ai uses {manager.aicurrentAction}!";
                    manager.aiHasGone = true;
                }
                else if (manager.aiHasGone)
                {
                    manager.ChangeState(new BSplayerTurn(manager));
                }
            }
            else
            {
                manager.ChangeState(new BSplayerResolve(manager));
            }
        }
        else if (manager.aiHasGone && !manager.playerPriority)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                manager.ChangeState(new BSplayerResolve(manager));
            }
        }
        else if (manager.aiHasGone && manager.playerPriority)
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
        //Debug.Log($"Ai deals {damage} damage");
        manager.mon1curHP -= damage;       
        manager.mon1hpbar.fillAmount = (float)manager.mon1curHP / manager.mon1maxHP;
        manager.mon1hpText.text = $"Health: {manager.mon1curHP} / {manager.mon1maxHP}";
        DeathCheck();
    }

    void DeathCheck()
    {
        if (manager.mon1curHP <= 0)
        {
            Debug.Log("AI wins");

            manager.dialogueText.enableDialogueText(false);
            manager.ChangeState(new BSlost(manager));
        }
    }

    void HealthPotion(int heal)
    {
        Debug.Log($"Ai heals for {heal}");
        manager.aihealthpots -= 1;
        manager.mon2curHP += heal;
        manager.mon1hpbar.fillAmount = (float)manager.mon1curHP / manager.mon1maxHP;
        manager.mon1hpText.text = $"Health: {manager.mon1curHP} / {manager.mon1maxHP}";
    }
}
    