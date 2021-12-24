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


    /// <summary>
    /// Allows player to select their action (attack/item/swap/escape)
    /// </summary>
    void actionSelection()
    {
        isAction = true;
        stateManager.dialogueText.enableActionSelector(true);

        ;

        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Right, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentAction < 3) //max moves - 1
            {
                ++stateManager.currentAction;
            }
        }
        else if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Left, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentAction > 0)
            {
                --stateManager.currentAction;
            }
        }
        else if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Down, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentAction < 2)
            {
                stateManager.currentAction += 2;
            }
        }
        else if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Up, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentAction > 1)
            {
                stateManager.currentAction -= 2;
            }
        }

        stateManager.dialogueText.updateActionSelection(stateManager.currentAction);

        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Interact, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentAction == 0) //Fight
            {
                stateManager.dialogueText.enableActionSelector(false);
                isAction = false;
                isMove = true;
            } else if (stateManager.currentAction == 1) //Item
            {
                stateManager.ChangeState(new BSplayerItem(stateManager));
                return;
            } else if (stateManager.currentAction == 2) //Swap
            {
                stateManager.ChangeState(new BSplayerSwap(stateManager));
                return;
            } else if (stateManager.currentAction == 3) //Escape
            {
                //Escape checker?
                if (Core.CoreManager.Instance.encounterManager.EncounterInfo.encounterType != Core.Player.EncounterType.Wild)
                {
                    Debug.Log("You cannot escape from non-wild");
                }
                else
                {
                    stateManager.ChangeState(new BSaiTurn(stateManager));
                }
            }
        }
    }

    /// <summary>
    /// Allows player to select their move(attack)
    /// </summary>
    void moveSelection()
    {
        isMove = true;
        stateManager.dialogueText.enableMoveSelector(true);
        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Right, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentMove < stateManager.playerCurMonster.moveSet.MoveCount - 1) //max moves - 1
            {
                ++stateManager.currentMove;
            }
        }
        else if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Left, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentMove > 0)
            {
                --stateManager.currentMove;
            }
        }
        else if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Down, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentMove < 1 && stateManager.playerCurMonster.moveSet.MoveCount == 3)
            {
                stateManager.currentMove += 2;
            } else if (stateManager.currentMove < 2 && stateManager.playerCurMonster.moveSet.MoveCount == 4)
            {
                stateManager.currentMove += 2;
            }
        }
        else if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Up, CustomInput.InputEnums.InputAction.Down))
        {
            if (stateManager.currentMove > 1)
            {
                stateManager.currentMove -= 2;
            }
        }

        stateManager.dialogueText.updateMoveSelection(stateManager.currentMove);

        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Interact, CustomInput.InputEnums.InputAction.Down))
        {
            {
                isMove = false;
                stateManager.ChangeState(new BSaiTurn(stateManager));
            }
        }

        if (Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Return, CustomInput.InputEnums.InputAction.Down))
        {
            {
                isMove = false;
                isAction = true;
                stateManager.dialogueText.enableMoveSelector(false);

            }
        }
    }
}   
