using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTexts : MonoBehaviour
{
    ///<Summary>
    ///Text system for during battle
    ///</Summary>
    public Text dialogueTexts;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveDetail;
    [SerializeField] List<Text> movesText;
    [SerializeField] List<Text> actionText;


    public void enableDialogueText(bool enabled)
    {
        dialogueTexts.gameObject.SetActive(enabled);
    }
    public void enableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        //Enable detail
    }

    public void enableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
        //Enable detail
    }

    public void updateMoveSelection(int selectedMove)
    {
        for (int i=0; i<movesText.Count; i++)
        {
            if (i == selectedMove)
            {
                movesText[i].color = Color.red;
            } else 
            {
                movesText[i].color = Color.black;
            }
        }
    }

    public void updateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionText.Count; i++)
        {
            if (i == selectedAction)
            {
                actionText[i].color = Color.red;
            }
            else
            {
                actionText[i].color = Color.black;
            }
        }
    }
}
