using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        public T LoadJSON(string path)
        {
            TextAsset jsonObj = Resources.Load<TextAsset>(path);
            Debug.Log("Path is : " + path);
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
