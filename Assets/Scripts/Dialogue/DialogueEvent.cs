using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Contains ID for what dialogue this entity has.
/// </summary>
public class DialogueEvent : MonoBehaviour
{
    [SerializeField]
    private string dialogueID = "NULL";

    [SerializeField]
    UnityEvent BeforeDialogueFire;

    [SerializeField]
    UnityEvent AfterDialogueFire;

    /// <summary>
    /// Fires the before dialogue event.
    /// Unsubscribes afterwards.
    /// </summary>
    private void FireBefore(string dialogueID)
    {
        BeforeDialogueFire?.Invoke();
        Core.Dialogue.DialogueManager.OnPreDialogueFire -= FireBefore;
    }

    /// <summary>
    /// Fires the after dialogue event.
    /// Unsubscribes afterwards.
    /// </summary>
    private void FireAfter(string dialogueID)
    {
        AfterDialogueFire?.Invoke();
        Core.Dialogue.DialogueManager.OnAfterDialogueFire -= FireAfter;
    }

    /// <summary>
    /// Fires this dialogue event
    /// </summary>
    public void FireDialogue()
    {
        Core.Dialogue.DialogueManager.OnPreDialogueFire += FireBefore;
        Core.Dialogue.DialogueManager.OnAfterDialogueFire += FireAfter;
        Core.CoreManager.Instance.dialogueManager.FireDialogue(dialogueID);
    }
}
