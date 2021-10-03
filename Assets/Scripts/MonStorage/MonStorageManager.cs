using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Storage
{
    public class MonStorageManager : MonoBehaviour
    {
        //List of all the MonStorageData boxes? 

        [SerializeField]
        private MonStorageData playerStorage = new MonStorageData();
        
        public MonStorageData PlayerStorage
        {
            get
            {
                return playerStorage;
            }
        }

        private void Start()
        {
            
        }


        [ContextMenu("Check")]
        public void Check()
        {
            PlayerStorage.Test();
        }
    }
}
