using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialogue
{
    /// <summary>
    /// Organizes dialogue with a scene name, gets written into a JSON
    /// </summary>
    public class SceneDialogueObject
    {
        public string sceneName;

        /// <summary>
        /// Dictionary form, key is dialogueID, object as value
        /// </summary>
        public Dictionary<string, DialogueObject> dialogueObjects = new Dictionary<string, DialogueObject>();

        /// <summary>
        /// Default constructor, doesn't populate anything.
        /// </summary>
        public SceneDialogueObject() { }

        /// <summary>
        /// Constructs SceneDialogueObject from SceneDialogueObjectJSON
        /// </summary>
        /// <param name=""></param>
        public SceneDialogueObject(SceneDialogueObjectJSON jsonObj)
        {
            sceneName = jsonObj.sceneName;

            foreach(DialogueObject obj in jsonObj.dialogueObjects)
            {
                if (!dialogueObjects.ContainsKey(obj.dialogueID))
                {
                    dialogueObjects.Add(obj.dialogueID, obj);
                }
                else
                {
                    throw new System.Exception("Dialogue Manager Error: " +
                        "Attempted to load in two dialogue objects with the same key: " + obj.dialogueID +
                        "in scene: " + sceneName);
                }             
            }
        }
    }
}
