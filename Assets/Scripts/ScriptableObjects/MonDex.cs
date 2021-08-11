using Mon.MonGeneration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A MonDex that holds a generation of mons used for a run
/// </summary>
namespace Mon.MonData
{
    public class MonDex
    {
        /// <summary>
        /// Monster Dictionary
        /// </summary>
        public Dictionary<int, GeneratedMon> monDict = new Dictionary<int, GeneratedMon>();

        /// <summary>
        /// Length of this dex
        /// </summary>
        public int dexLength = 0;

        /// <summary>
        /// Generation ID.
        /// </summary>
        public int generationID;

        /// <summary>
        /// Checks if the id is a valid mon.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckValidID(int id)
        {
            GeneratedMon mon = new GeneratedMon();
            return monDict.TryGetValue(id, out mon);
        }

        /// <summary>
        /// Retrieves a mon with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GeneratedMon GetMonByID(int id)
        {
            GeneratedMon mon = new GeneratedMon();
            monDict.TryGetValue(id, out mon);

            if (mon != null)
            {
                return mon;
            }
            else
            {
                Debug.LogError("Error, invalid ID. No Mon exists with that ID: " + id);
                return null;
            }
        }

        /// <summary>
        /// Saves every entry in this dex into a folder
        /// </summary>
        public void SaveDex()
        {
#if DEBUG_ENABLED && MON_GEN
            Debug.Log("Saving dex initializing...");
            Debug.Log("Dex length is : " + dexLength);
#endif
            for (int i = 1; i < dexLength + 1; i++)
            {
                //Check if valid. (Should be)
                if (CheckValidID(i))
                {
                    GeneratedMon mon = GetMonByID(i);
                    mon.SaveData();
                }
                else
                {
                    throw new System.Exception("MonDex save error: invalid ID");
                }
            }
#if DEBUG_ENABLED && MON_GEN
            Debug.Log("Saving dex completed.");
#endif
        }

        /// <summary>
        /// Loads data from JSON files into dex
        /// </summary>
        public async Task LoadDex()
        {
            //Clear in case we have any data.
            monDict.Clear();
            dexLength = 0;

            //Initialize json utilty and path to load from
            Utility.JsonUtility<GeneratedMon> jsonUtility = new Utility.JsonUtility<GeneratedMon>();
            string checkPath = StaticPaths.SaveToGeneratedPath + "/" + generationID;
            string loadPath = StaticPaths.LoadFromGeneratedPath + "/" + generationID;

            //Check if the generation inputted is valid.
            if(!Directory.Exists(checkPath))
            {
                throw new System.Exception("MonDex Error: Generation path does not exist: " + checkPath);
            }

            var info = new DirectoryInfo(checkPath);
            var fileInfo = info.GetFiles();
            foreach(FileInfo file in fileInfo)
            {
                //Ignore meta files
                if (!file.Name.Contains("meta"))
                {                   
                    string[] separatedFile = file.Name.Split('.');
                    if (separatedFile.Length < 2)
                    {
                        throw new System.Exception("MonDex Error: Load path for base mon has too few segments separated by '.' at: " + checkPath + file.Name);
                    }
                    else if (separatedFile.Length > 2)
                    {
                        throw new System.Exception("MonDex Error: Load path for base mon has too many segments separated by '.' at: " + checkPath + file.Name);
                    }
                    GeneratedMon mon = await jsonUtility.LoadJSON(loadPath + "/" + separatedFile[0]);
                    monDict.Add(mon.ID, mon);
                    dexLength++;
                }
            }
#if DEBUG_ENABLED
            Debug.Log("Loaded " + dexLength + " entries.");
#endif
        }
    }
}
