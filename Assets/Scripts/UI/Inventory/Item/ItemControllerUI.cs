using CustomInput;
using Inventory;
using Inventory.Enums;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Selector;
using UnityEngine;

namespace UI.Inventory.Item
{
    public class ItemControllerUI : SelectorControllerUI
    {
        protected ItemModelUI itemModel;

        public ItemControllerUI()
        {
            InventoryControllerUI.InventoryUIState += ChangeState;
        }

        /// <summary>
        /// Finalizer. Called when garbage collector comes along to remove it.
        /// </summary>
        ~ItemControllerUI()
        {
            InventoryControllerUI.InventoryUIState -= ChangeState;
        }

        public override void HandlePrintingState()
        {
            //Setting data in Model
            StartIndexTimer();
            Refresh();
            base.HandlePrintingState();
        }
        public override void HandleDisplayState()
        {
            IndexControl();
        }

        public override void Init()
        {
            if (!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }

            model = new ItemModelUI();
            selectorModel = (SelectorModelUI)model;
            itemModel = (ItemModelUI)model;

            model.Init();
        }

        public void EnableItemView(ItemMask _mask, ItemCategory _category)
        {
            itemModel.ResetSelectIndex();
            itemModel.SetActive(true);
            itemModel.SetUpdate(true);
            itemModel.SetDisplayItems(Core.CoreManager.Instance.playerInventory.Inventory.GetItems(_mask, _category));
            itemModel.InvokeModel(key);
        }
    }
}
