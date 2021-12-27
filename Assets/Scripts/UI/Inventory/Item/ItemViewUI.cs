using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Base;
using UI.Selector;
using UnityEngine;

namespace UI.Inventory.Item
{
    public class ItemViewUI : SelectorViewUI
    {
        protected ItemModelUI itemModel;

        [Tooltip("Which transform the elements will be spawned parented to")]
        [SerializeField]
        Transform targetTransform;

        [Tooltip("Prefab element that will be used as elements for this view")]
        [SerializeField]
        GameObject itemElement;

        [Tooltip("Text box to put selected item description.")]
        [SerializeField]
        TextMeshProUGUI itemDescriptionText;

        protected override void OnEnable()
        {
            ItemModelUI.ModelUpdate += UpdateModel;
        }

        protected override void OnDisable()
        {
            ItemModelUI.ModelUpdate -= UpdateModel;
        }

        protected override void SetModel(Model _model)
        {
            base.SetModel(_model);
            itemModel = (ItemModelUI)_model;
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
