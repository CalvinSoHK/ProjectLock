using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

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
        private Dictionary<string, SceneDialogueObject> dialogueDict = new Dictionary<string, SceneDialogueObject>();

        private void OnEnable()
        {
            Core.World.WorldManager.OnSceneStartLoad += RegisterSceneDialogue;
            Core.World.WorldManager.OnSceneStartUnload += UnregisterSceneDialogue;
        }

        private void OnDisable()
        {
            Core.World.WorldManager.OnSceneStartLoad -= RegisterSceneDialogue;
            Core.World.WorldManager.OnSceneStartUnload -= UnregisterSceneDialogue;
        }

        /// <summary>
        /// Preloads data.
        /// </summary>
        private void Start()
        {
            PreloadData();
        }

        /// <summary>
        /// Preloads data. Helpful when we start in editor in different scenes.
        /// </summary>
        private void PreloadData()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                RegisterSceneDialogue(scene.name);
            }
        }

        /// <summary>
        /// Loads and constructs dialogue dict given a scene name.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private async Task<SceneDialogueObject> LoadDialogueDict(string sceneName)
        {
#if DEBUG_ENABLED
            Debug.Log("DialogueManager: Loading scene: " + sceneName);
#endif
            JsonUtility<SceneDialogueObjectJSON> jsonUtility = new JsonUtility<SceneDialogueObjectJSON>();
            SceneDialogueObjectJSON jsonObj = await jsonUtility.LoadJSON(CreatePath(sceneName));
            SceneDialogueObject sceneObj = new SceneDialogueObject(jsonObj);

            return sceneObj;
        }

        /// <summary>
        /// Registers the dialogue for a given scene when loaded.
        /// </summary>
        private async Task RegisterSceneDialogue(string sceneName)
        {
            if (!dialogueDict.ContainsKey(sceneName))
            {
#if DEBUG_ENABLED
                Debug.Log("DialogueManager: Registering scene: " + sceneName);
#endif
                //Load dialogue dict
                SceneDialogueObject sceneDict = await LoadDialogueDict(sceneName);

                dialogueDict.Add(sceneName, sceneDict);
            }
            else
            {
                throw new System.Exception("Dialogue Manager Error: " +
                    "Attempting to register dialogue from scene that is already registered: " + sceneName);
            }
        }

        /// <summary>
        /// Unregisters the dialogue for a given scene when unloaded.
        /// </summary>
        private async Task UnregisterSceneDialogue(string sceneName)
        {
            SceneDialogueObject sceneDict;
#if DEBUG_ENABLED
            Debug.Log("DialogueManager: Dict contains key: " + dialogueDict.ContainsKey(sceneName));
#endif

            if (dialogueDict.TryGetValue(sceneName, out sceneDict))
            {
#if DEBUG_ENABLED
                Debug.Log("DialogueManager: Unregistering scene: " + sceneName);
#endif
                //Remove that scene's dialogue
                dialogueDict.Remove(sceneName);
                CoreManager.Instance.addressablesManager.ReleaseAddressable(CreatePath(sceneName));
            }
            else
            {
                throw new System.Exception("Dialogue Manager Error: Trying to unregister dialogue for scene: " + sceneName);
            }
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
        /// Creates the right path with scenename
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private string CreatePath(string sceneName)
        {
            return StaticPaths.DialoguePath + "/" + sceneName;
        }
    }
}
