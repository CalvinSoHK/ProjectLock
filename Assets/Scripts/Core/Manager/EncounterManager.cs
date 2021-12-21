using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Tile;
using World;
using System.Threading.Tasks;
using Utility;
using System.IO;

namespace Core.Player
{
    public class EncounterManager : MonoBehaviour
    {
        [Header("Encounter Generation")]
        public WorldEncounterData encounterData;

        [Header("Encounter Detection")]
        [SerializeField]
        LayerMask encounterLayers;
        float checkDistance = 2f;
        RaycastHit hitInfo = new RaycastHit();
        EncounterArea encounterArea = null;
        EncounterData curEncounter = null;

        [SerializeField]
        private Encounter encounterInfo;

        /// <summary>
        /// Current encounter information
        /// </summary>
        public Encounter EncounterInfo
        {
            get
            {
                return encounterInfo;
            }
        }

        [Header("Encounter Chance")]
        float curChance = 0;
        [SerializeField]
        float encounterChance = 0.25f;
        [SerializeField]
        float chanceGain = 0.05f;

        /// <summary>
        /// List of scenes to return to
        /// </summary>
        [SerializeField]
        private List<string> loadedScenes;

        private const string battleScene = "TestBattleSystem";

        private void OnEnable()
        {
            PlayerController.OnPlayerMove += CheckEncounter;
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerMove -= CheckEncounter;
        }

        private void CheckEncounter()
        {
            if (CheckEncounterableState())
            {
                if (curChance == 0)
                {
                    curChance = encounterChance;
                }

                if (CoreManager.Instance.randomManager.Range(0f, 1f, "CheckEncounter1") <= curChance)
                {
                    curEncounter = encounterArea.PickEncounter();
                    curChance = 0f;
                    MonIndObj indObj = new MonIndObj(CoreManager.Instance.dexManager.GetMonByID(curEncounter.dexID), curEncounter.level);
                    Party wildMonParty = new Party();
                    wildMonParty.AddMember(indObj);
                    FireEncounter(new Encounter(EncounterType.Wild, wildMonParty));
                }
                else
                {
                    curChance += chanceGain;
                }
            }
            else
            {
                encounterArea = null;
                curEncounter = null;
            }
        }

        private bool CheckEncounterableState()
        {
            if (Physics.Raycast(CoreManager.Instance.player.transform.position, Vector3.down, out hitInfo, checkDistance, encounterLayers))
            {
                EncounterTile tile = hitInfo.collider.transform.GetComponent<EncounterTile>();
                if (tile)
                {
                    encounterArea = tile.EncounterArea;
                }
                else
                {
                    Debug.LogError("No encounter tile on object that should have one: " + hitInfo.collider.gameObject);
                }
                return true;
            }
            return false;
        }

        public void FireEncounter(Encounter encounterInfo)
        {
            FireEncounterAsync(encounterInfo);
        }

        private async Task FireEncounterAsync(Encounter _encounterInfo)
        {
            encounterInfo = _encounterInfo;
            loadedScenes = CoreManager.Instance.worldManager.GetLoadedScenes();
            CoreManager.Instance.worldStateManager.SetWorldState(WorldState.Battle);
            CoreManager.Instance.SetPlayerActive(false);
            await CoreManager.Instance.worldManager.LoadScene(battleScene, UnityEngine.SceneManagement.LoadSceneMode.Single, true, false);
        }

        public async void FinishEncounterAsync()
        {
            await CoreManager.Instance.worldManager.LoadSceneList(loadedScenes);
            CoreManager.Instance.worldStateManager.SetWorldState(WorldState.Overworld);
            CoreManager.Instance.SetPlayerActive(true);
            loadedScenes.Clear();
            encounterInfo = null;
        }

        /// <summary>
        /// Initialize encounter data
        /// </summary>
        /// <returns></returns>
        public async Task InitEncounterData()
        {
            if (CheckValidData())
            {
                encounterData = await LoadData();
            }
            else
            {
                encounterData = new WorldEncounterData();
            }
        }

        /// <summary>
        /// Check if there is data the given path
        /// </summary>
        /// <returns></returns>
        private bool CheckValidData()
        {
            string checkPath = StaticPaths.SaveToGeneratedEncountersPaths + "/" + Core.CoreManager.Instance.randomManager.Seed + ".txt";
            Debug.Log("Checking file at path: " + checkPath);
            //Check if the generation inputted is valid.
            return File.Exists(checkPath);
        }

        /// <summary>
        /// Loading data for a given seed
        /// </summary>
        private async Task<WorldEncounterData> LoadData()
        {
            WorldEncounterData data = new WorldEncounterData();
            await data.LoadData();
            return data;
        }
    
        /// <summary>
        /// Check if given encounter ID is alreayd in the dict
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TryGetEncounter(string id, out List<EncounterData> dataList)
        {
            return encounterData.TryGetEncounters(id, out dataList);
        }
    }

    public enum EncounterType
    {
        Wild,
        Trainer,
        Gym
    }

    /// <summary>
    /// Class that describes an encounter
    /// </summary>
    [System.Serializable]
    public class Encounter
    {
        public EncounterType encounterType;
        public Party party;

        public Encounter(EncounterType _type, Party _party)
        {
            encounterType = _type;
            party = _party;
        }
    }
}