using Mon.MonData;
using Mon.MonGeneration;
using Mon.Moves;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Manages the current MonDex
    /// </summary>
    public class DexManager : MonoBehaviour
    {
        private MonDex monDex = new MonDex();

        private MoveDex moveDex = new MoveDex();

        private bool dexReady = false;
        public bool DexReady { get { return dexReady; } }

        private void Start()
        {
            //InitDex(1);
        }

        /// <summary>
        /// Initializes and loads an inputted MonDex
        /// </summary>
        /// <param name="generationID"></param>
        public async Task InitDex(MonDex inputDex = null)
        {
            //monDex.generationID = generationID;

            await moveDex.LoadDex();

            if (inputDex != null)
            {
                monDex = inputDex;
            }
            else
            {
                await monDex.LoadDex();
            }

            
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
