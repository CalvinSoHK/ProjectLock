using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

namespace Core.World
{


    /// <summary>
    /// Handles the world, as in loading and unloading scenes
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        /// <summary>
        /// Const for how often to check for async operation completions.
        /// </summary>
        private const int delayTick = 100;

        /// <summary>
        /// List of loaded scenes.
        /// </summary>
        private List<string> loadedScenes = new List<string>();

        /// <summary>
        /// Event that is invoked when the scene is a valid load and is about to be loaded.
        /// </summary>
        public static AsyncDelegateT<string>.Del1 OnSceneStartLoad;

        /// <summary>
        /// Event that is invoked when the scene is a valid load, finished loading, and is about to fade in.
        /// </summary>
        public static AsyncDelegateT<string>.Del1 OnSceneLoadedBeforeFadeIn;


        /// <summary>
        /// Event that is invoked when the scene is a valid load, finished loading, and has finished fading in.
        /// </summary>
        public static AsyncDelegateT<string>.Del1 OnSceneLoadedAfterFadeIn;

        /// <summary>
        /// Event that is invoked when the scene is a valid unload and is about to be unloaded.
        /// </summary>
        public static AsyncDelegateT<string>.Del1 OnSceneStartUnload;

        /// <summary>
        /// Event that is invoked when the scene is a valid unload and is about to be unloaded.
        /// </summary>
        public static AsyncDelegateT<string>.Del1 OnSceneUnloaded;

        /// <summary>
        /// Helps run the AsyncDelegates.
        /// </summary>
        AsyncDelegateT<string> delegateHelper = new AsyncDelegateT<string>();

        private void Start()
        {
            PreloadData();
        }

        private void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.W))
            {
                TestUnload();
            }*/
        }

        private async void TestLoad()
        {
            Debug.Log("Currently loaded: " + loadedScenes.Count + " scenes");
            Debug.Log("Testing async load. Running now...");
            if (await LoadScene("SampleScene", LoadSceneMode.Additive))
            {
                Debug.Log("Async load completed.");
            }
            Debug.Log("Currently loaded: " + loadedScenes.Count + " scenes");
        }

        private async void TestUnload()
        {
            Debug.Log("Testing async unload. Running now...");
            if (await UnloadScene("SampleScene"))
            {
                Debug.Log("Async unload completed.");
            }
            Debug.Log("Currently loaded: " + loadedScenes.Count + " scenes");
        }

        /// <summary>
        /// Preloads data. Helpful when we start in editor in different scenes.
        /// </summary>
        private void PreloadData()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                AddSceneToList(scene.name);
            }
        }

        /// <summary>
        /// Async task to load a scene.
        /// Returns true if and when complete.
        /// False if unable to complete.
        /// LoadingScreen is only used for Single loadmode.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="loadSceneMode"></param>
        /// <param name="loadingScreen"></param>
        /// <returns></returns>
        public async Task<bool> LoadScene(string sceneName, LoadSceneMode loadSceneMode, bool loadingScreen = true, bool changePlayerState = true)
        {
            //Check that we don't already have that scene loaded.
            if (IsValidLoad(sceneName))
            {
                await delegateHelper.RunAsyncDelegate(OnSceneStartLoad, sceneName);

                //We want to use additive so loading screen remains. Manually remove all other scenes here first.
                if (loadSceneMode == LoadSceneMode.Single)
                {
                    if (loadingScreen)
                    {
                        await Core.CoreManager.Instance.loadManager.LoadLoadingScreen(changePlayerState);
                    }                  

                    //Find all scenes that aren't loading screen
                    List<string> targetList = new List<string>();
                    for (int i = 0; i < SceneManager.sceneCount; i++)
                    {
                        Scene scene = SceneManager.GetSceneAt(i);
                        if (!scene.name.Equals(LoadManager.LoadSceneName))
                        {
                            targetList.Add(scene.name);
                        }
                    }

                    //Unload all those scenes
                    foreach (string name in targetList)
                    {
                        await UnloadScene(name);
                    }

                }

                AsyncOpHelper asyncOpHelper = new AsyncOpHelper();
                bool result = await asyncOpHelper.CompleteAsyncOp(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive), delayTick);

                //If it correctly loaded scene
                if (result)
                {
                    AddSceneToList(sceneName);

                    await delegateHelper.RunAsyncDelegate(OnSceneLoadedBeforeFadeIn, sceneName);

                    if (loadingScreen)
                    {
                        await CoreManager.Instance.loadManager.UnloadLoadingScreen(changePlayerState);
                    }               

                    await delegateHelper.RunAsyncDelegate(OnSceneLoadedAfterFadeIn, sceneName);
                }

                return result;
            }
            else
            {
                Debug.LogError("WorldManager Error: Attempted to load a scene that is already loaded: " + sceneName + " Aborted.");
                return false;
            }
        }

        /// <summary>
        /// Async task to unload a scene.
        /// Returns true if and when complete.
        /// False if unable to complete.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public async Task<bool> UnloadScene(string sceneName)
        {
            //Check that it's valid to unload this scene
            if (IsValidUnload(sceneName))
            {
                await delegateHelper.RunAsyncDelegate(OnSceneStartUnload, sceneName);

                AsyncOpHelper asyncOpHelper = new AsyncOpHelper();
                bool result = await asyncOpHelper.CompleteAsyncOp(SceneManager.UnloadSceneAsync(sceneName), delayTick);

                //If operation succeeded
                if (result)
                {
                    RemoveSceneFromList(sceneName);

                    await delegateHelper.RunAsyncDelegate(OnSceneUnloaded, sceneName);
                }

                return result;
            }
            else
            {
#if DEBUG_ENABLED
                Debug.Log("WorldManager: Attempted to unload a scene that is not loaded: " + sceneName + " Aborted.");
#endif            
                return false;
            }
        }

        /// <summary>
        /// Loads a given list of scene names
        /// </summary>
        /// <param name="sceneList"></param>
        /// <returns></returns>
        public async Task LoadSceneList(List<string> sceneList)
        {
            //Init loading screen
            await Core.CoreManager.Instance.loadManager.LoadLoadingScreen(false);

            if(sceneList.Count > 0)
            {
                await LoadScene(sceneList[0], LoadSceneMode.Single, false, false);
            }

            foreach (string sceneName in sceneList)
            {
                if(!await LoadScene(sceneName, LoadSceneMode.Additive, false, false))
                {
                    throw new WorldManagerException("Failed to load scene: " + sceneName);
                }
            }

            //Unload loading screen
            await CoreManager.Instance.loadManager.UnloadLoadingScreen(false);
        }

        /// <summary>
        /// Says if we can load a given scene
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private bool IsValidLoad(string sceneName)
        {
            return !loadedScenes.Contains(sceneName);
        }

        /// <summary>
        /// Says if we can unload a given scene.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private bool IsValidUnload(string sceneName)
        {
            return loadedScenes.Contains(sceneName);
        }

        /// <summary>
        /// Load scene data
        /// Returns true if operation succeeds
        /// </summary>
        /// <param name="scene"></param>
        public void AddSceneToList(string sceneName)
        {
            if (IsValidLoad(sceneName))
            {
                loadedScenes.Add(sceneName);
            }
            else
            {
                throw new WorldManagerException("WorldManager Error: Attempted to add scene to list even though it is already there. Scene: " + sceneName);
            }
        }

        /// <summary>
        /// Unload scene data
        /// Returns true if operation succeeds
        /// </summary>
        /// <param name="scene"></param>
        public void RemoveSceneFromList(string sceneName)
        {
            if (IsValidUnload(sceneName))
            {
                loadedScenes.Remove(sceneName);
            }
            else
            {
                throw new WorldManagerException("Attempted to remove scene to list even though it isn't already there. Scene: " + sceneName);
            }
        }

        /// <summary>
        /// Empties loaded scene list.
        /// </summary>
        public void ClearSceneList()
        {
            loadedScenes.Clear();
        }

        /// <summary>
        /// Gives the list of all the loaded scenes
        /// </summary>
        /// <returns></returns>
        public List<string> GetLoadedScenes()
        {
            return loadedScenes;
        }
    }

    /// <summary>
    /// Exception for WorldManager
    /// </summary>
    public class WorldManagerException : Exception
    {
        public WorldManagerException (string msg) : base(msg)
        {

        }
    }
}

