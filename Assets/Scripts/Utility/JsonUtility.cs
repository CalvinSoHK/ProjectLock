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
            ResourceRequest request = Resources.LoadAsync<TextAsset>(path);
            while(!request.isDone)
            {
                await Task.Delay(10);
            }
#if DEBUG_ENABLED
            Debug.Log("Completed request. Status: " + request.isDone);
#endif
            TextAsset jsonObj = (TextAsset)request.asset;
#if DEBUG_ENABLED
            Debug.Log("Loading path: " + path + " as object: " + jsonObj);
#endif
            if(jsonObj == null)
            {
                throw new System.Exception("JsonUtility: Failed to load json at path: " 
                    + path 
                    + " as type: " 
                    + typeof(T).ToString());
            }

            return JsonUtility.FromJson<T>(jsonObj.text);
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
