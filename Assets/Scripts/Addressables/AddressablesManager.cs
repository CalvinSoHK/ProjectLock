using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.AddressableSystem
{
    public class AddressablesManager
    {
        public delegate void AddressableProgressEvent(float progress);
        /// <summary>
        /// Event that will propagate load progress of an addressable.
        /// Useful for loading screens or debugging.
        /// </summary>
        public static AddressableProgressEvent LoadProgressEvent;

        /// <summary>
        /// Dictionary that stores addressable handles with key: path and value: handles
        /// </summary>
        private ConcurrentDictionary<string, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
            handleDict = new ConcurrentDictionary<string, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();


        /// <summary>
        /// Tries to load addressable from a path
        /// If it succeeds, it will return true
        /// Call LoadAddressable again to grab it with the same path. (Will not go through full load again when you do)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="logProgress"></param>
        /// <returns></returns>
        public async Task<bool> TryLoadAddressable<T>(string path, bool logProgress = false)
        {
            AsyncOperationHandle handle;
            //If we've already loaded the path then this is definitely valid.
            if (IsHandleLoaded(path))
            {
                return true;
            }
            else
            {
                handle = Addressables.LoadAssetAsync<T>(path);
            }

            try
            {
                await HandleHandle(handle, path, logProgress);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Loads given addressable asset.
        /// If the addressable is already loaded it will grab it from the dict instead.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> LoadAddressable<T>(string path, bool logProgress = false)
        {
            if(await TryLoadAddressable<T>(path, logProgress))
            {
                AsyncOperationHandle handle = GetHandle(path);
                return (T)handle.Result;
            }
            else
            {
                throw new System.Exception("AddressablesManager Exception: Attempted to load path that isn't valid. Be sure to call TryLoadAddressable before calling LoadAddressable.");
            }
        }

        /// <summary>
        /// Loads addressable assets by tag
        /// Returns an IList<T> of all assets related to tag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tag"></param>
        /// <param name="logProgress"></param>
        /// <returns></returns>
        public async Task<IList<T>> LoadAddressablesByTag<T>(string tag, bool logProgress = false)
        {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(tag, obj =>
            { //NOTE for some reason you are required to give a callback or you can't load by tag.
            });

            await HandleHandle(handle, tag, logProgress);

            return handle.Result;
        }

        /// <summary>
        /// Adds a handle to the dict with path as key
        /// Returns true if successful
        /// </summary>
        /// <param name="path"></param>
        /// <param name="handle"></param>
        private bool AddHandle(string path, AsyncOperationHandle handle)
        {
            return handleDict.TryAdd(path, handle);
        }

        /// <summary>
        /// Handles a returned handle.
        /// When task is complete the handle's result is ready or failed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handle"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task HandleHandle(AsyncOperationHandle handle, string path, bool logProgress)
        {
            //If we want to log progress on this load
            if (logProgress)
            {
                do
                {
                    LoadProgressEvent.Invoke(handle.PercentComplete);
                    await Task.Delay(1000);
                } while (!handle.IsDone);
            }
            else //Otherwise just wait for handle to finish
            {
                await handle.Task;
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (!IsHandleLoaded(path))
                {
                    AddHandle(path, handle);
                }             
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception("AddressablesManager Error: Failed to load addressable at path: " + path);
            }
            else
            {
                throw new Exception("AddressablesManager Error: Addressable returned none status at path: " + path);
            }
        }

        /// <summary>
        /// Releases addressable at given path.
        /// Returns true if successful
        /// </summary>
        /// <param name="path"></param>
        public bool ReleaseAddressable(string path)
        {
            AsyncOperationHandle handle;
            if (handleDict.TryRemove(path, out handle))
            {
                Addressables.Release(handle);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if there is a handle loaded for the given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsHandleLoaded(string path)
        {
            if (handleDict.ContainsKey(path))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Grabs an already loaded handle from dict
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AsyncOperationHandle GetHandle(string path)
        {
            AsyncOperationHandle result;
            if (handleDict.TryGetValue(path, out result))
            {
                return result;
            }
            throw new System.Exception("AddressablesManager Error: Attempting to get handle that isn't in dict at path: " + path);
        }
    }
}
