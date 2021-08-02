﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public delegate void  OnWorldManagerEvent();

    /// <summary>
    /// Event that is invoked when the scene is a valid load and is about to be loaded.
    /// </summary>
    public static OnWorldManagerEvent OnSceneStartLoad;

    /// <summary>
    /// Event that is invoked when the scene is a valid load and is finished loading.
    /// </summary>
    public static OnWorldManagerEvent OnSceneLoaded;

    /// <summary>
    /// Event that is invoked when the scene is a valid unload and is about to be unloaded.
    /// </summary>
    public static OnWorldManagerEvent OnSceneStartUnload;

    /// <summary>
    /// Event that is invoked when the scene is a valid unload and is about to be unloaded.
    /// </summary>
    public static OnWorldManagerEvent OnSceneUnloaded;

    private void Start()
    {
        PreloadData();
        //TestLoad();
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
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {        
            Scene scene = SceneManager.GetSceneAt(i);
            AddSceneToList(scene.name);
        }
    }

    /// <summary>
    /// Async task to load a scene.
    /// Returns true if and when complete.
    /// False if unable to complete.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="loadSceneMode"></param>
    /// <returns></returns>
    public async Task<bool> LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        //Check that we don't already have that scene loaded.
        if (IsValidLoad(sceneName))
        {
            OnSceneStartLoad?.Invoke();

            AsyncOpHelper asyncOpHelper = new AsyncOpHelper();
            bool result = await asyncOpHelper.CompleteAsyncOp(SceneManager.LoadSceneAsync(sceneName, loadSceneMode), delayTick);
            
            //If it correctly loaded scene
            if (result)
            {
                if(loadSceneMode == LoadSceneMode.Single)
                {
                    ClearSceneList();
                }

                AddSceneToList(sceneName);

                OnSceneLoaded?.Invoke();
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
            OnSceneStartUnload?.Invoke();

            AsyncOpHelper asyncOpHelper = new AsyncOpHelper();
            bool result = await asyncOpHelper.CompleteAsyncOp(SceneManager.UnloadSceneAsync(sceneName), delayTick);
                     
            //If operation succeeded
            if (result)
            {
                RemoveSceneFromList(sceneName);

                OnSceneUnloaded?.Invoke();
            }

            return result;
        }
        else
        {
            Debug.LogError("WorldManager Error: Attempted to unload a scene that is not loaded: " + sceneName + " Aborted.");
            return false;
        }
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
            throw new System.Exception("WorldManager Error: Attempted to add scene to list even though it is already there. Scene: " + sceneName);
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
            throw new System.Exception("WorldManager Error: Attempted to remove scene to list even though it isn't already there. Scene: " + sceneName);
        }
    }     

    /// <summary>
    /// Empties loaded scene list.
    /// </summary>
    public void ClearSceneList()
    {
        loadedScenes.Clear();
    }
}