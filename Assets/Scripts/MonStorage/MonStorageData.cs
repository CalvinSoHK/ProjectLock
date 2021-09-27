using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;

namespace Storage
{
    public class MonStorageData
    {
        //Move somewhere else in future
        private const int storageSize = 50;

        // -1 if no Free Slots
        private int freeIndex = -1;

        private MonIndObj[] monStorage = new MonIndObj[storageSize];

        public MonIndObj this[int i]
        {
            get
            {
                if (i < 0 || i > monStorage.Length)
                {
                    throw new IndexOutOfRangeException("Out Of Range");
                }
                return monStorage[i];
            }
            set
            {
                if (i < 0 || i > monStorage.Length)
                {
                    throw new IndexOutOfRangeException("Out Of Range");
                }
                monStorage[i] = value;
            }
        }
        /// <summary>
        /// Checks whether storage has space
        /// Sets freeIndex if True
        /// </summary>
        private bool HasSpace()
        {
            for (int i = 0; i < monStorage.Length; i++)
            {
                if (monStorage[i] == null)
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
                monStorage[freeIndex] = caughtMon;
                Debug.Log(caughtMon.Nickname + " addded to slot " + (freeIndex));
            }
        }

        public void Test()
        {
            for (int i = 0; i < monStorage.Length; i++)
            {
                if (monStorage[i] == null)
                {
                    Debug.Log("Null at slot " + i);
                    break;
                }
                Debug.Log(monStorage[i].Nickname + " " + monStorage[i].battleObj.monStats.hp +"/"+ monStorage[i].stats.hp);
            }
        }
    }

}
