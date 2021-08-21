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
        /// When true, at the beginning of this dialogue it will disable interact
        /// </summary>
        [SerializeField]
        private bool disableInteract = true;

        /// <summary>
        /// When true, at the end of this dialogue it will re-enable interact 
        /// </summary>
        [SerializeField]
        private bool enableInteract = true;

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
            if (disableInteract)
            {
                Core.CoreManager.Instance.interact.DisableInteract();
            }
            
            OnEventFire?.Invoke();
            DialogueManager.OnDialogueFire -= FireOnDialogue;
        }

        /// <summary>
        /// Fires the OnDialogueAfter unity events.
        /// </summary>
        /// <param name="obj"></param>
        private void FireOnDialogueAfter(DialogueObject obj)
        {
            if (enableInteract)
            {
                Core.CoreManager.Instance.interact.EnableInteract();
            }
            
            OnAfterEventFire?.Invoke();
            DialogueManager.OnDialogueAfterFire -= FireOnDialogueAfter;
        }
    }
}
