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
        stateManager.dialogueText.dialogueTexts.gameObject.SetActive(false);
        stateManager.playerHasGone = false;
        stateManager.aiHasGone = false;
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
        stateManager.dialogueText.enableMoveSelector(false);
        stateManager.dialogueText.enableActionSelector(false);
    }


    void actionSelection()
    {
        isAction = true;
        stateManager.dialogueText.enableActionSelector(true);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (stateManager.currentAction < 3) //max moves - 1
            {
                ++stateManager.currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (stateManager.currentAction > 0)
            {
                --stateManager.currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (stateManager.currentAction < 2)
            {
                stateManager.currentAction += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (stateManager.currentAction > 1)
            {
                stateManager.currentAction -= 2;
            }
        }

        stateManager.dialogueText.updateActionSelection(stateManager.currentAction);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (stateManager.currentAction == 0) //Fight
            {
                stateManager.dialogueText.enableActionSelector(false);
                isAction = false;
                isMove = true;
            } else if (stateManager.currentAction == 1) //Item
            {
                if(stateManager.playerhealthpots > 0)
                {
                    stateManager.ChangeState(new BSaiTurn(stateManager));
                }
                else
                {
                    Debug.Log("No Healing Pots Remaining");
                }
            } else if (stateManager.currentAction == 2) //Swap
            {
                
                stateManager.ChangeState(new BSplayerSwap(stateManager));
            } else if (stateManager.currentAction == 3) //Escape
            {
                //Escape checker?
                stateManager.ChangeState(new BSaiTurn(stateManager));
            }
        }
    }

    void moveSelection()
    {
        isMove = true;
        stateManager.dialogueText.enableMoveSelector(true);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (stateManager.currentMove < 3) //max moves - 1
            {
                ++stateManager.currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (stateManager.currentMove > 0)
            {
                --stateManager.currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (stateManager.currentMove < 2)
            {
                stateManager.currentMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (stateManager.currentMove > 1)
            {
                stateManager.currentMove -= 2;
            }
        }

        stateManager.dialogueText.updateMoveSelection(stateManager.currentMove);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            {
                isMove = false;
                stateManager.ChangeState(new BSaiTurn(stateManager));
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            {
                isMove = false;
                isAction = true;
                stateManager.dialogueText.enableMoveSelector(false);

            }
        }
    }
}   
