using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialogue
{
    /// <summary>
    /// Handles dialogue.
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        /// <summary>
        /// Delegate event for dialogue manager.
        /// </summary>
        /// <param name="dialogueID"></param>
        public delegate void DialogueEvent(string dialogueID);
        public static DialogueEvent OnPreDialogueFire;
        public static DialogueEvent OnDialogueFire;
        public static DialogueEvent OnAfterDialogueFire;

        /// <summary>
        /// Contains all the DialogueObjects in a given instance.
        /// First string is the scene ID for a given Dictionary.
        /// Second string is the dialogueID for a given object.
        /// Third string is the dialogueObject itself.
        /// </summary>
        private Dictionary<string, Dictionary<string, DialogueObject>> dialogueDict;

        private void OnEnable()
        {
            //Core.World.WorldManager.OnSceneStartLoad += 
        }

        private void OnDisable()
        {
            
        }

        /// <summary>
        /// Registers the dialogue for a given scene when loaded.
        /// </summary>
        private void RegisterSceneDialogue()
        {

        }

        /// <summary>
        /// Unregisters the dialogue for a given scene when unloaded.
        /// </summary>
        private void UnregisterSceneDialogue()
        {
            
        }

        /// <summary>
        /// Empties the dictionary.
        /// If need to add cleanup can be added here.
        /// </summary>
        private void ClearDict()
        {
            dialogueDict.Clear();
        }

        /// <summary>
        /// Fires a given dialogue ID.
        /// </summary>
        /// <param name="dialogueID"></param>
        public void FireDialogue(string dialogueID)
        {

        }
    }
}
