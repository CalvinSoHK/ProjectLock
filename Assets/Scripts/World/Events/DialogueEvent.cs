using Core.Dialogue;
using UnityEngine;

namespace World.Event
{
    /// <summary>
    /// Contains ID for what dialogue this entity has.
    /// </summary>
    public class DialogueEvent : BaseEvent
    {
        [SerializeField]
        private string dialogueID = "NULL",
            sceneName = "";

        /// <summary>
        /// Fires this dialogue event as a normal dialogue event.
        /// Will re-enable player interact at the end of dialogue.
        /// </summary>
        public void FireDialogue()
        {
            OnBeforeEventFire?.Invoke();
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
            OnEventFire?.Invoke();
            DialogueManager.OnDialogueFire -= FireOnDialogue;
        }

        /// <summary>
        /// Fires the OnDialogueAfter unity events.
        /// </summary>
        /// <param name="obj"></param>
        private void FireOnDialogueAfter(DialogueObject obj)
        {
            Core.CoreManager.Instance.interact.EnableInteract();
            OnAfterEventFire?.Invoke();
            DialogueManager.OnDialogueAfterFire -= FireOnDialogueAfter;
        }
    }
}
