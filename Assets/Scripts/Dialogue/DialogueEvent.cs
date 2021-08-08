using Core.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace World
{
    /// <summary>
    /// Contains ID for what dialogue this entity has.
    /// </summary>
    public class DialogueEvent : MonoBehaviour
    {
        [SerializeField]
        private string dialogueID = "NULL",
            sceneName = "";

        [SerializeField]
        UnityEvent OnDialogueFire;

        [SerializeField]
        UnityEvent OnDialogueAfterFire;

        /// <summary>
        /// Fires this dialogue event
        /// </summary>
        public void FireDialogue()
        {
            DialogueManager.OnDialogueFire += FireOnDialogue;
            DialogueManager.OnDialogueAfterFire += FireOnDialogueAfter;
            Core.CoreManager.Instance.dialogueManager.FireDialogueEvent(sceneName, dialogueID);
        }

        /// <summary>
        /// Fires the OnDialogueFire unity events.
        /// </summary>
        /// <param name="obj"></param>
        private void FireOnDialogue(DialogueObject obj)
        {
            Core.CoreManager.Instance.interact.DisableInteract();
            OnDialogueFire?.Invoke();
            DialogueManager.OnDialogueFire -= FireOnDialogue;
        }

        /// <summary>
        /// Fires the OnDialogueAfter unity events.
        /// </summary>
        /// <param name="obj"></param>
        private void FireOnDialogueAfter(DialogueObject obj)
        {
            Core.CoreManager.Instance.interact.EnableInteract();
            OnDialogueAfterFire?.Invoke();
            DialogueManager.OnDialogueAfterFire -= FireOnDialogueAfter;
        }
    }
}
