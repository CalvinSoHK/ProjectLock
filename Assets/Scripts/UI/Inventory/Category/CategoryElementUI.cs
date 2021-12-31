using Inventory.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Base;
using UI.Selector;
using UnityEngine;
using Utility;

namespace UI.Inventory.Category
{
    [RequireComponent(typeof(PointerColorPicker))]
    public class CategoryElementUI : SelectorElementUI
    {
        [SerializeField]
        private ItemCategory category = ItemCategory.Consumables;

        [SerializeField]
        private TextMeshProUGUI label;

        public delegate void CategorySelect(ItemCategory item);
        public static CategorySelect CategorySelectEvent;
        public override void Init()
        {
            base.Init();
            Prettify prettify = new Prettify();
            label.text = prettify.Pretty(category.ToString(), false);
        }

        public void SelectCategory()
        {
            CategorySelectEvent?.Invoke(category);
        }
    }
}
