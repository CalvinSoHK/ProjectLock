using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using Inventory.Enums;

namespace UI.Inventory.Category
{
    public class CategoryModelUI : SelectorModelUI
    {
        ItemCategory selectedCategory;

        public ItemCategory SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
        }

        ItemMask selectedMask = ItemMask.Unique;

        public ItemMask SelectedMask
        {
            get
            {
                return selectedMask;
            }
        }

        public override void Init()
        {
            base.Init();
            selectedCategory = ItemCategory.Consumables;
        }

        public override void Reset()
        {
            base.Reset();
            selectedCategory = ItemCategory.Consumables;
        }

        public delegate void CategoryModel(string key, CategoryModelUI model);
        public new static CategoryModel ModelUpdate;

        public override void InvokeModel(string _key)
        {
            ModelUpdate?.Invoke(_key, this);
        }

        public void SetSelectedCategory(ItemCategory _category)
        {
            selectedCategory = _category;
        }

        public void SetMask(ItemMask _mask)
        {
            selectedMask = _mask;
        }
    }
}
