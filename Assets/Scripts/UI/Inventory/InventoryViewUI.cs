using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryViewUI : BaseViewUI
    {
        protected InventoryModelUI inventoryModel;

        protected virtual void OnEnable()
        {
            InventoryModelUI.ModelUpdate += UpdateModel;
        }

        protected virtual void OnDisable()
        {
            InventoryModelUI.ModelUpdate -= UpdateModel;
        }

        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            inventoryModel = (InventoryModelUI)_model;
        }

        private void UpdateModel(string _key, Model _model)
        {
            if (_key.Equals(controllerKey))
            {
                UpdateView(_model);
            }
        }
    }
}
