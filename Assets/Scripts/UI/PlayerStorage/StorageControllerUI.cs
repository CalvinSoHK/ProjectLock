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
            storageModel.SetPlayerStorage(Core.CoreManager.Instance.monStorageManager.PlayerStorage.monStorage);
            base.HandlePrintingState();
        }

        public override void HandleDisplayState()
        {
            base.HandleDisplayState();
            SwapMons(0,1);
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
            if (Input.GetKeyDown(KeyCode.S))
            {
                Core.CoreManager.Instance.monStorageManager.PlayerStorage.SwapMonsByIndex(firstIndex, secondIndex);
                HandlePrintingState();
            }
        }
    }
}
