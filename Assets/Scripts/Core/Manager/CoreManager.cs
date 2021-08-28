using Core.Player;
using Core.World;
using Core.Dialogue;
using Core.PartyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using World;
using Core.AddressableSystem;
using System.Threading.Tasks;
using Mon.MonData;
using Inventory;
using CustomInput;

namespace Core
{
    /// <summary>
    /// Core manager singleton
    /// </summary>
    public class CoreManager : MonoBehaviour
    {
        private static CoreManager _instance;

        public static CoreManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(_instance);
            }
        }

        [Header("Player Objects")]
        [SerializeField]
        public PlayerController player;

        [SerializeField]
        public PlayerInteract interact;

        [SerializeField]
        public PlayerInputMap inputMap;

        [SerializeField]
        public PartyManager playerParty;

        [SerializeField]
        public CameraManager camera;

        [SerializeField]
        public InventoryManager playerInventory;

        [Header("Managers")]
        [SerializeField]
        public DexManager dexManager;

        [SerializeField]
        public EncounterManager encounterManager;

        [SerializeField]
        public WorldManager worldManager;

        [SerializeField]
        public LoadManager loadManager;

        [SerializeField]
        public DialogueManager dialogueManager;

        [SerializeField]
        public AddressablesManager addressablesManager;

        [SerializeField]
        public PartyUIManager partyUIManager;

        [SerializeField]
        public WorldStateManager worldStateManager;

        [Header("Scriptable Objects")]
        public TypeRelationSO typeRelationSO;

        /// <summary>
        /// Initializes game
        /// </summary>
        public async Task Initialize()
        {
            await dexManager.GenerateDex();
            SetPlayerActive(true);
        }

        /// <summary>
        /// Sets the player as active
        /// </summary>
        /// <param name="active"></param>
        public void SetPlayerActive(bool active)
        {
            player.gameObject.SetActive(active);
        }

        /// <summary>
        /// Moves player to found point in scene with scene name
        /// </summary>
        /// <param name="key"></param>
        public void TeleportToPoint(string sceneName, int key)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            //Check if scene returned was a valid scene (as in it is in the loaded hierarchy)
            if (scene.IsValid())
            {
                //Check root game objects for the ScenePoints object.
                GameObject[] objects = scene.GetRootGameObjects();
                GameObject target = null;
                foreach(GameObject obj in objects)
                {
                    if (obj.name.Equals("ScenePoints"))
                    {
                        target = obj;
                        break;
                    }
                }

                //If we have a ScenePoints Object
                if(target != null)
                {
                    //Find all the teleport points under the ScenePoints obj
                    TeleportPoint[] points = target.transform.GetComponentsInChildren<TeleportPoint>();
                    foreach(TeleportPoint point in points)
                    {
                        //If this is the key we want teleport here
                        if(point.key == key)
                        {
                            point.TeleportHere(player.gameObject);
                            break;
                        }
                    }
                }
                else
                {
                    Debug.LogError("CoreManager error: There is no ScenePoints object in the scene: " + sceneName);
                }
            }
            else
            {
                Debug.LogError("CoreManager error: Attempted to teleport to a point in a scene that isn't loaded: " + sceneName);
            }
        }
    }
}
