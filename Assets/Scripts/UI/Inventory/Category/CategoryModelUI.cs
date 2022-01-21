using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Selector;
using Inventory.Enums;

namespace UI.Inventory.Category
{
    [System.Serializable]
    public class CategoryModelUI : SelectorModelUI
    {
        [SerializeField]
        private ItemCategory selectedCategory;

        public ItemCategory SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
        }

        [SerializeField]
        private ItemMask selectedMask = ItemMask.Unique;

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

        protected override void InvokeSpecificModel(string _key)
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
