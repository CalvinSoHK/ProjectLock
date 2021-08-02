using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSplayerTurn : BSstate
{
    bool isAction = true;
    bool isMove = false;
    public BSplayerTurn(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered player turn");
        manager.playerHasGone = false;
        manager.aiHasGone = false;
    }

    public override void Run()
    {
        if (isAction)
        {
            actionSelection();
        } else if (isMove)
        {
            moveSelection();
        }
    }

    public override void Leave()
    {
        manager.dialogueText.enableMoveSelector(false);
        manager.dialogueText.enableActionSelector(false);
    }


    void actionSelection()
    {
        isAction = true;
        manager.dialogueText.enableActionSelector(true);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (manager.currentAction < 3) //max moves - 1
            {
                ++manager.currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (manager.currentAction > 0)
            {
                --manager.currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (manager.currentAction < 2)
            {
                manager.currentAction += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (manager.currentAction > 1)
            {
                manager.currentAction -= 2;
            }
        }

        manager.dialogueText.updateActionSelection(manager.currentAction);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (manager.currentAction == 0) //Fight
            {
                manager.dialogueText.enableActionSelector(false);
                isAction = false;
                isMove = true;
            } else if (manager.currentAction == 1) //Item
            {
                if(manager.playerhealthpots > 0)
                {
                    manager.ChangeState(new BSaiTurn(manager));
                }
                else
                {
                    Debug.Log("No Healing Pots Remaining");
                }
            } else if (manager.currentAction == 2) //Swap
            {
                manager.ChangeState(new BSaiTurn(manager));
            } else if (manager.currentAction == 3) //Escape
            {
                //Escape checker?
                manager.ChangeState(new BSaiTurn(manager));
            }
        }
    }

    void moveSelection()
    {
        isMove = true;
        manager.dialogueText.enableMoveSelector(true);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (manager.currentMove < 3) //max moves - 1
            {
                ++manager.currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (manager.currentMove > 0)
            {
                --manager.currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (manager.currentMove < 2)
            {
                manager.currentMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (manager.currentMove > 1)
            {
                manager.currentMove -= 2;
            }
        }

        manager.dialogueText.updateMoveSelection(manager.currentMove);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            {
                isMove = false;
                manager.ChangeState(new BSaiTurn(manager));
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            {
                isMove = false;
                isAction = true;
                manager.dialogueText.enableMoveSelector(false);

            }
        }
    }
}   
