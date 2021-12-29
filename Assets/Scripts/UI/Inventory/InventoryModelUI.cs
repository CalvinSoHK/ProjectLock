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
    public class InventoryModelUI : Model
    {
        public CategoryModelUI categoryModel;

        public ItemModelUI itemModel;

        public delegate void InventoryModel(string key, InventoryModelUI model);
        public static InventoryModel ModelUpdate;

        private ItemMask itemMask;

        public ItemMask ItemMask
        {
            get
            {
                return itemMask;
            }
        }

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
            categoryModel = new CategoryModelUI();
            itemModel = new ItemModelUI();
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
            itemModel.SetDisplayItems(Core.CoreManager.Instance.playerInventory.Inventory.GetItems(itemMask, SelectedCategory));
            itemModel.InvokeModel(_key);
            ModelUpdate?.Invoke(_key, this);
        }
    }
}
