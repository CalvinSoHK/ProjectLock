using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using UI.Base;

namespace UI.Storage
{
    public class StorageViewUI : SelectorViewUI
    {
        private StorageModelUI storageModel;

        [SerializeField]
        List<StorageElementUI> storageList = new List<StorageElementUI>();

        protected override void OnEnable()
        {
            StorageModelUI.ModelUpdate += UpdateModel;
        }

        protected override void OnDisable()
        {
            StorageModelUI.ModelUpdate -= UpdateModel;
        }


        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            storageModel = (StorageModelUI)_model;
        }

        private void UpdateModel(string _key, Model _model)
        {
            if (_key.Equals(controllerKey))
            {
                UpdateView(_model);
            }
        }

        protected override void UpdateView(Model _model)
        {
            base.UpdateView(_model);
            //Set elements
            SetElement();
        }

        private void SetElement()
        {
            //CurrentTemp
            for (int i = 0; i < storageList.Count; i++)
            {
                if (storageModel.playerStorage[i] != null)
                {
                    storageList[i].monName.text = storageModel.playerStorage[i].Nickname;
                }
                else
                {
                    //Enable but make it empty?
                    //Make it unable to be selected but can be hovered
                    storageList[i].monName.text = "null";
                }
                storageList[i].EnableElement();
            }
        }
    }
}