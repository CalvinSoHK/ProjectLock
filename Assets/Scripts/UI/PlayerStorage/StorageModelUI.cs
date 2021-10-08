using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using UI.Selector;

namespace UI.Storage
{
    public class StorageModelUI : SelectorModelUI
    {
        //private MonIndObj[] _playerStorage = new MonIndObj[Core.CoreManager.Instance.monStorageManager.playerStorage.storageSize];
        public int activeBox = 0;

        private MonIndObj[] _playerStorage = new MonIndObj[50];

        public MonIndObj[] playerStorage
        {
            get
            {
                return _playerStorage;
            }
        }

        public void SetPlayerStorage(MonIndObj[] currentStorage)
        {
            _playerStorage = currentStorage;
        }

        public delegate void StorageModel(string key, StorageModelUI model);
        public static new StorageModel ModelUpdate;

        public override void InvokeModel(string _key)
        {
            ModelUpdate?.Invoke(_key, this);
        }
    }
}
