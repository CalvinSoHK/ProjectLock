using Inventory;
using Inventory.Items;
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
        ItemElementUI itemElement;

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

        protected override void UpdateView(Model _model)
        {
            base.UpdateView(_model);
            if (!itemModel.Active)
            {
                EmptyChildren();
            }
        }


        protected virtual void PopulateDisplayitems()
        {
            EmptyChildren();
            int index = 0;
            foreach (ItemStack itemStack in itemModel.DisplayItems)
            {
                ItemElementUI element = Instantiate(itemElement, targetTransform);
                element.SetIndex(index);
                InventoryItem item = Core.CoreManager.Instance.itemMaster.GetItem(itemStack.ItemID);
                element.SetItemStack(item, itemStack.ItemCount);
                index++;
                selectorElementList.Add(element);
                EnableElement(element);
                managedList.Add(element);
            }
            selectorBoundMax = selectorElementList.Count;
            Init();
        }

        /// <summary>
        /// Empties children so we can repopulate the menu
        /// </summary>
        protected virtual void EmptyChildren()
        {
            selectedIndex = 0;
            for (int i = targetTransform.childCount - 1; i > -1; i--)
            {
                managedList.Remove(targetTransform.GetChild(i).gameObject.GetComponent<BaseElementUI>());
                Destroy(targetTransform.GetChild(i).gameObject);
            }
            selectorElementList.Clear();        
        }

        protected override void RefreshUI()
        {          
            if (itemModel.Update)
            {
                PopulateDisplayitems();
                itemModel.SetUpdate(false);
            }
            UpdateDescription();
            base.RefreshUI();
        }

        /// <summary>
        /// Updates description text
        /// Only calls if there was an index change.
        /// </summary>
        private void UpdateDescription()
        {
            bool enabled = false;
            foreach (ItemElementUI element in selectorElementList)
            {
                if (element.SelectableIndex == selectedIndex)
                {
                    itemDescriptionText.text = element.DisplayItem.ItemDescription;
                    enabled = true;
                }            
            }

            if (!enabled)
            {
                itemDescriptionText.text = "";
            }
        }
    }
}
