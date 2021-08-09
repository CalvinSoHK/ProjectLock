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
    /// <typeparam name="T"></typeparam>
    public class JsonUtility <T>
    {
        /// <summary>
        /// Loads a JSON at "path" and turns into object type T
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> LoadJSON(string path)
        {
            AddressablesManager addressManager = Core.CoreManager.Instance.addressablesManager;

            TextAsset asset = await addressManager.LoadAddressable<TextAsset>(path, false);

            return JsonUtility.FromJson<T>(asset.text);
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
