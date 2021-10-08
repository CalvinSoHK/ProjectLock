using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Storage
{
    public class MonStorageManager : MonoBehaviour
    {
        //List of all the MonStorageData boxes? 
        private int boxCount = 2;

        private MonStorageData playerStorage;

        public MonStorageListData playerStorageList = new MonStorageListData();
        
        private void Start()
        {
            for (int i = 0; i < boxCount; i++)
            {
                playerStorage = new MonStorageData();
                playerStorageList.monStorageList.Add(playerStorage);
            }
        }

        [ContextMenu("Check")]
        public void Check()
        {
            playerStorage.Test();
        }
    }
}
