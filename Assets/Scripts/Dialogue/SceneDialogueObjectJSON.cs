using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialogue
{ 
    [System.Serializable]
    /// <summary>
    /// Organizes dialogue with a scene name, gets written into a JSON
    /// </summary>
    public class SceneDialogueObjectJSON
    {
        public string sceneName;

        public List<DialogueObject> dialogueObjects = new List<DialogueObject>();

        /// <summary>
        /// Default constructor. Doesn't fill anything.
        /// </summary>
        public SceneDialogueObjectJSON() { }

        /// <summary>
        /// Constructs SceneDialogueObjectJSON from SceneDialogueObject
        /// </summary>
        /// <param name="obj"></param>
        public SceneDialogueObjectJSON(SceneDialogueObject obj)
        {
            sceneName = obj.sceneName;

            //Takes all dialogue objects and stores them
            foreach(DialogueObject dialogueObj in obj.dialogueObjects.Values)
            {
                if (!dialogueObjects.Contains(dialogueObj))
                {
                    dialogueObjects.Add(dialogueObj);
                }
                else
                {
                    throw new System.Exception("Dialogue Manager Error: " +
                        "Attempted to load in two dialogue objects with the same key into JSON format: " + dialogueObj.dialogueID +
                        "in scene: " + sceneName);
                }

            }
        }
    }
}
