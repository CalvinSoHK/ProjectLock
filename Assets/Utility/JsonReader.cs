using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// JSONUtility Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonReader <T>
    {
        /// <summary>
        /// Loads a JSON at "path" and turns into object type T
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadJSON(string path)
        {
            TextAsset jsonObj = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(jsonObj.text);
        }
    }
}
