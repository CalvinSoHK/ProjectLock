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
    public class DexManager
    {
        public MonDex Dex
        {
            get
            {
                return monDex;
            }
        }

        private MonDex monDex = new MonDex();

        private MoveDex moveDex = new MoveDex();

        private bool dexReady = false;
        public bool DexReady { get { return dexReady; } }

        private void Start()
        {
            //InitDex(1);
        }

        /// <summary>
        /// Generates a fresh new dex
        /// </summary>
        /// <returns></returns>
        public async Task GenerateDex()
        {
            //If moveDex is not ready wait for to finish first
            if (!moveDex.IsReady)
            {
                await moveDex.LoadDex();
            }

            Debug.Log("Generating dex");

            //Create a generator to make a whole new generation. Then generate mons by key
            MonGenerator generator = new MonGenerator();
            await generator.GenerateMonsByKey();

            //Set the dex to the generated dex
            monDex = generator.monDex;

            //Mark as ready
            dexReady = true;
        }

        /// <summary>
        /// Initializes and loads an inputted MonDex
        /// </summary>
        /// <param name="generationID"></param>
        public async Task LoadDex(MonDex inputDex = null)
        {
            //If moveDex is not ready wait for to finish first
            if (!moveDex.IsReady)
            {
                await moveDex.LoadDex();
            }
            
            //If we were inputted an inputDex set that as the dex
            if (inputDex != null)
            {
                monDex = inputDex;
            }
            else //Otherwise load dex that was set in it's variable
            {
                await monDex.LoadDex();
            }

            //Mark the dexManager as dexReady
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
