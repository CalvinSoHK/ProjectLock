using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UnityEngine.UI;
namespace UI.Storage
{
    public class StorageElementUI : SelectorElementUI
    {
        public Text monName;


        public delegate void StorageMonSelectEvent(string key, int selectableKey);
        public static StorageMonSelectEvent StorageMonSelectFire;

        public override void Select()
        {
            base.Select();
            StorageMonSelectFire?.Invoke("StorageMon", selectableIndex);
        }
    }
}
