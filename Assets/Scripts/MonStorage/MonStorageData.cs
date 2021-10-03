using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;

namespace Storage
{
    /// <summary>
    /// MonStorageData for an individual box
    /// </summary>
    public class MonStorageData
    {
        //Move somewhere else in future
        private const int _storageSize = 50;

        public int storageSize
        {
            get
            {
                return _storageSize;
            }
        }

        // -1 if no Free Slots
        private int freeIndex = -1;

        private MonIndObj[] MonStorage = new MonIndObj[_storageSize];

        public MonIndObj[] monStorage
        {
            get
            {
                return MonStorage;
            }
        }

        public MonIndObj this[int i]
        {
            get
            {
                if (i < 0 || i > MonStorage.Length)
                {
                    throw new IndexOutOfRangeException("Out Of Range");
                }
                return MonStorage[i];
            }
            set
            {
                if (i < 0 || i > MonStorage.Length)
                {
                    throw new IndexOutOfRangeException("Out Of Range");
                }
                MonStorage[i] = value;
            }
        }
        /// <summary>
        /// Checks whether storage has space
        /// Sets freeIndex if True
        /// </summary>
        private bool HasSpace()
        {
            for (int i = 0; i < MonStorage.Length; i++)
            {
                if (MonStorage[i] == null)
                {
                    freeIndex = i;
                    return true;
                }
            }
            //No freeIndex
            freeIndex = -1;
            return false;
        }

        public void AddMonToFreeSlot(MonIndObj caughtMon)
        {
            if (HasSpace())
            {
                caughtMon.FullReset();
                MonStorage[freeIndex] = caughtMon;
                Debug.Log(caughtMon.Nickname + " addded to slot " + (freeIndex));
            }
        }

        //Should use MonIndObj instead? what if null
        public void SwapMonsByIndex(int firstIndex, int secondIndex)
        {
            //What if null>
            MonIndObj temp = MonStorage[secondIndex];

            MonStorage[secondIndex] = MonStorage[firstIndex];
            MonStorage[firstIndex] = temp;
        }

        /// <summary>
        /// Adds selectedMon into first free slot of player party
        /// Returns false is no free slots
        /// </summary>
        /// <param name="selectedMon"></param>
        /// <returns></returns>
        public bool AddToParty(MonIndObj selectedMon)
        {
            for (int i = 0; i < Core.CoreManager.Instance.playerParty.party.PartySize; i++)
            {
                if (Core.CoreManager.Instance.playerParty.party.GetPartyMember(i) == null)
                {
                    Core.CoreManager.Instance.playerParty.party.SetPartyMember(i, selectedMon);
                    return true;
                }
            }
            return false;
        }

        public void Test()
        {
            for (int i = 0; i < MonStorage.Length; i++)
            {
                if (MonStorage[i] == null)
                {
                    Debug.Log("Null at slot " + i);
                    break;
                }
                Debug.Log(MonStorage[i].Nickname + " " + MonStorage[i].battleObj.monStats.hp +"/"+ MonStorage[i].stats.hp);
            }
        }
    }

}
