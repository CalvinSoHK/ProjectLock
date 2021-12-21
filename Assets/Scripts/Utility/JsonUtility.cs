using Core.AddressableSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// JSONUtility Class
    /// </summary>
    /// <typeparam name="T"> T is the type</typeparam>
    public class JsonUtility <T>
    {
        public enum LoadType
        {
            Addressable,
            Resources
        }

        /// <summary>
        /// Loads a JSON at "path" and turns into object type T
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> LoadJSON(string path, LoadType type = LoadType.Addressable)
        {
            if(type == LoadType.Addressable)
            {
                AddressablesManager addressManager = Core.CoreManager.Instance.addressablesManager;

                TextAsset asset = await addressManager.LoadAddressable<TextAsset>(path, false);

                return JsonUtility.FromJson<T>(asset.text);
            }
            else if(type == LoadType.Resources)
            {
                ResourceRequest request = Resources.LoadAsync<TextAsset>(path);
                Debug.Log("Attempting load at path: " + path);
                while (!request.isDone)
                {
                    Debug.Log("Progress: " + request.progress);
                    await Task.Delay(100);
                }
                TextAsset asset = (TextAsset)request.asset;
                return JsonUtility.FromJson<T>(asset.text);
            }
            throw new System.Exception("LoadJSON Error : Entered invalid loading mode: " + type);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        public void WriteJSON(T target, string path)
        {
            string jsonData = JsonUtility.ToJson(target, true);
            StreamWriter sw = new StreamWriter(path);
            sw.Write(jsonData);
            sw.Close();
        }
    }
}
