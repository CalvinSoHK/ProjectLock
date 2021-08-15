using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.AddressableSystem
{
    public class AddressablesManager : MonoBehaviour
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
        private Dictionary<string, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
            handleDict = new Dictionary<string, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();

        /// <summary>
        /// Loads given addressable asset.
        /// If the addressable is already loaded it will grab it from the dict instead.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> LoadAddressable<T>(string path, bool logProgress = false)
        {
            AsyncOperationHandle handle;
            if (IsPathLoaded(path))
            {
                handle = GetHandle(path);
            }
            else
            {
                handle = Addressables.LoadAssetAsync<T>(path);
            }

            await HandleHandle(handle, path, logProgress);
            return (T)handle.Result;
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
        /// </summary>
        /// <param name="path"></param>
        /// <param name="handle"></param>
        private void AddHandle(string path, AsyncOperationHandle handle)
        {
            if (!handleDict.ContainsKey(path))
            {
                handleDict.Add(path, handle);
            }
            else
            {
                throw new System.Exception("Attempting to load addressable that has already been loaded at path: " + path);
            }
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
            if (logProgress && handle.Status != AsyncOperationStatus.Succeeded)
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
                if (!IsPathLoaded(path))
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
        /// Only works if addressable is loaded into dict.
        /// </summary>
        /// <param name="path"></param>
        public void ReleaseAddressable(string path)
        {
            if (handleDict.ContainsKey(path))
            {
                AsyncOperationHandle handle;
                if (handleDict.TryGetValue(path, out handle))
                {
                    handleDict.Remove(path);
                    Addressables.Release(handle);
                }
                else
                {
                    throw new System.Exception("AddressablesManager Error: Failed to retrieve handle that is in dict at path: " + path);
                }
            }
            else
            {
                throw new System.Exception("AddressablesManager Error: Attempting to release addressable that is not in dict at path: " + path);
            }
        }

        /// <summary>
        /// Checks if the path is loaded
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsPathLoaded(string path)
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
