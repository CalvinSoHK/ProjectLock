using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialogue
{
    [System.Serializable]
    /// <summary>
    /// Dialogue object.
    /// </summary>
    public class DialogueObject
    {
        /// <summary>
        /// ID of this dialogue
        /// </summary>
        public string dialogueID;

        /// <summary>
        /// Scene name this dialogue is from
        /// </summary>
        public string sceneName;

        /// <summary>
        /// Name of the speaker for this dialogue.
        /// </summary>
        public string speakerName;

        /// <summary>
        /// The dialogue text.
        /// </summary>
        public string dialogueText;

        /// <summary>
        /// Whether or not there is a next dialogue point
        /// </summary>
        public bool hasNext;

        /// <summary>
        /// ID of the next dialogue part. Only uses this if hasNext is true.
        /// </summary>
        public string dialogueNextID;

        /// <summary>
        /// Whether or not we want to ask for confirmation after dialogue line.
        /// NOTE: Confirm event is attached to the given entity. If there is none it will fail to do anything.
        /// </summary>
        public bool requestConfirm;
    }
}
