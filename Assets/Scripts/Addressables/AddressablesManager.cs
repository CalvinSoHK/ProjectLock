using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

/// <summary>
/// Helps us use addressables
/// </summary>
public class AddressablesManager<T>
{
    /// <summary>
    /// Loads given addressable asset
    /// Applies callback to when the handle is completed.
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public void LoadAddressable(string address, 
        Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle> callback)
    {
        UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle handle =
            Addressables.LoadAssetAsync<T>(address);
        handle.Completed += callback;
    }
}
