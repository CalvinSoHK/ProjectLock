using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTexts : MonoBehaviour
{
    ///<Summary>
    ///Text system for during battle
    ///</Summary>
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetail;
    [SerializeField] List<Text> movesText;
    [SerializeField] List<Text> actionText;


    public void enableDialogueText(bool enabled)
    {
        dialogueText.enabled = enabled;
    }
    public void enableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
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
}
