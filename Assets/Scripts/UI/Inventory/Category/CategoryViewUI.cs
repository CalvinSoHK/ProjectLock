using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Selector;
using UnityEngine;

namespace UI.Inventory.Category
{
    public class CategoryViewUI : SelectorViewUI
    {
        protected CategoryModelUI categoryModel;

        protected override void OnEnable()
        {
            CategoryModelUI.ModelUpdate += UpdateModel;
        }

        protected override void OnDisable()
        {
            CategoryModelUI.ModelUpdate -= UpdateModel;
        }

        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            categoryModel = (CategoryModelUI)_model;
        }
    }
}
