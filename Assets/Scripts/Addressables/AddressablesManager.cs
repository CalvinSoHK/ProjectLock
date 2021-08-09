using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

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
        /// Loads given addressable asset
        /// Applies callback to when the handle is completed.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> LoadAddressable<T>(string path, bool logProgress)
        {
            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle handle =
                Addressables.LoadAssetAsync<T>(path);

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

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                AddHandle(path, handle);
                return (T)handle.Result;
            }
            else if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed)
            {
                throw new System.Exception("AddressablesManager Error: Failed to load addressable at path: " + path);
            }
            else
            {
                throw new System.Exception("AddressablesManager Error: Addressable returned none status at path: " + path);
            }
        }

        /// <summary>
        /// Adds a handle to the dict with path as key
        /// </summary>
        /// <param name="path"></param>
        /// <param name="handle"></param>
        private void AddHandle(string path, UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle handle)
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
        /// Releases addressable at given path.
        /// Only works if addressable is loaded into dict.
        /// </summary>
        /// <param name="path"></param>
        public void ReleaseAddressable(string path)
        {
            if (handleDict.ContainsKey(path))
            {
                UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle handle;
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
    }
}
