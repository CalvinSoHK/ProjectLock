using Mon.MonData;
using Mon.MonGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Manages the current MonDex
    /// </summary>
    public class MonDexManager : MonoBehaviour
    {
        private MonDex monDex = new MonDex();

        private bool dexReady = false;
        public bool DexReady { get { return dexReady; } }

        private void Start()
        {
            InitDex(1);
        }

        /// <summary>
        /// Initializes and loads the mondex with given generation ID;
        /// </summary>
        /// <param name="generationID"></param>
        public async void InitDex(int generationID)
        {
            monDex.generationID = generationID;
            await monDex.LoadDex();
            dexReady = true;
        }

        /// <summary>
        /// Grabs the 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GeneratedMon GetMonByID(int ID)
        {
            return monDex.GetMonByID(ID);
        }

        /// <summary>
        /// Creates an individual mon with given dex ID and level.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public MonIndObj CreateIndMon(int ID, int level)
        {
            MonIndObj individualMon = new MonIndObj(GetMonByID(ID), level);
            return individualMon;
        }
    }
}
