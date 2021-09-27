using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Storage
{
    public class MonStorageManager : MonoBehaviour
    {
        [SerializeField]
        private MonStorageData playerStorage = new MonStorageData();
        
        public MonStorageData PlayerStorage
        {
            get
            {
                return playerStorage;
            }
        }

        [ContextMenu("Check")]
        public void Check()
        {
            PlayerStorage.Test();
        }
    }
}
