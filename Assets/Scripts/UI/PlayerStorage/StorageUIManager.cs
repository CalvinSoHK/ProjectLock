using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Party;

namespace UI.Storage
{
    public class StorageUIManager : MonoBehaviour
    {
        public GameObject storage;


        public delegate void TestEvent();
        public static TestEvent TestFire;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {   
                if (!storage.activeSelf)
                {
                    storage.SetActive(true);
                    SetPrintingState();
                } else
                {
                    storage.SetActive(false);
                }
            }
        }

        private void SetPrintingState()
        {
            TestFire?.Invoke();
        }
    }
}
