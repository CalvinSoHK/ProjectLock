using Inventory;
using Inventory.Enums;
using Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Inventory.Category;
using UI.Inventory.Item;
using UnityEngine;

namespace UI.Inventory
{
    [System.Serializable]
    public class InventoryModelUI : Model
    {
        public CategoryModelUI categoryModel;

        public ItemModelUI itemModel;

        public delegate void InventoryModel(string key, InventoryModelUI model);
        public static InventoryModel ModelUpdate;

        [SerializeField]
        private ItemMask itemMask;

        public ItemMask ItemMask
        {
            get
            {
                return itemMask;
            }
        }

        [SerializeField]
        private ItemCategory selectedCategory;

        public ItemCategory SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
        }

        public override void Init()
        {
            itemModel = (ItemModelUI)Core.CoreManager.Instance.uiManager.itemController.model;
            categoryModel = (CategoryModelUI)Core.CoreManager.Instance.uiManager.categoryController.model;
        }

        public override void Reset()
        {
            
        }

        public virtual void SetItemMask(ItemMask _mask)
        {
            itemMask = _mask;
            categoryModel.SetMask(_mask);
        }

        public virtual void SetSelectedCategory(ItemCategory _category)
        {
            selectedCategory = _category;
            categoryModel.SetSelectedCategory(_category);
        }

        protected override void InvokeSpecificModel(string _key)
        {
            categoryModel.InvokeModel(_key);          
            ModelUpdate?.Invoke(_key, this);
        }
    }
}
