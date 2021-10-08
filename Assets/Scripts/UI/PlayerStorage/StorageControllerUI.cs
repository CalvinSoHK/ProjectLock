using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UI.Base;
using CustomInput;

namespace UI.Storage
{
    public class StorageControllerUI : SelectorControllerUI
    {
        private StorageModelUI storageModel;

        //Action in future to change to printing when interact
        public override void HandleOffState()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeState(UIState.Printing);
            }
        }

        public override void HandlePrintingState()
        {
            //Setting data in Model
            StartIndexTimer();
            storageModel.SetPlayerStorage(Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList[storageModel.activeBox].monStorage);
            base.HandlePrintingState();
        }

        public override void HandleDisplayState()
        {
            base.HandleDisplayState();
            SwapMons(0,1);
            SwapBox();
            ChangeActiveBox();
        }
        public override void Init()
        {
            if (!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }

            model = new StorageModelUI();
            storageModel = (StorageModelUI)model;
            selectorModel = (SelectorModelUI)model;


            model.Init();
        }

        private void SwapMons(int firstIndex, int secondIndex)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList[0].SwapMonsByIndex(firstIndex, secondIndex);
                HandlePrintingState();
            }
        }

        /// <summary>
        /// Swap Position Testing
        /// </summary>
        private void SwapBox()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Core.CoreManager.Instance.monStorageManager.playerStorageList.SwapBoxPosition(0,1);
                for (int i = 0; i < 2;i++)
                {
                    if (Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList[i].monStorage[0] == null)
                    {
                        Debug.Log(i + ": Null");
                    }
                    else
                    {
                        Debug.Log(i + " " + Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList[i].monStorage[0].Nickname);
                    }
                }
                HandlePrintingState();
            }
        }

        private void ChangeActiveBox()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (storageModel.activeBox == Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList.Count-1)
                {
                    storageModel.activeBox = 0;
                }
                else
                {
                    storageModel.activeBox += 1;
                }

                HandlePrintingState();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (storageModel.activeBox == 0)
                {
                    storageModel.activeBox = Core.CoreManager.Instance.monStorageManager.playerStorageList.monStorageList.Count-1;
                }
                else
                {
                    storageModel.activeBox -= 1;
                }

                HandlePrintingState();
            }
        }

    }
}
