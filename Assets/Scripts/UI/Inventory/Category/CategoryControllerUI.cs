using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Party;
using UI.Selector;
using UnityEngine;

namespace UI.Inventory.Category
{
    public class CategoryControllerUI : SelectorControllerUI
    {
        protected CategoryModelUI categoryModel;

        public CategoryControllerUI()
        {
            InventoryControllerUI.InventoryUIState += ChangeState;
        }

        /// <summary>
        /// Finalizer. Called when garbage collector comes along to remove it.
        /// </summary>
        ~CategoryControllerUI()
        {
            InventoryControllerUI.InventoryUIState -= ChangeState;
        }

        public override void HandlePrintingState()
        {
            //Setting data in Model
            StartIndexTimer();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld)
            {
                Refresh();
            }
            base.HandlePrintingState();
        }

        public override void HandleDisplayState()
        {
             IndexControl();
        }
        protected override void InitFresh()
        {
            model = new CategoryModelUI();
            selectorModel = (SelectorModelUI)model;
            categoryModel = (CategoryModelUI)model;
        }

        protected override void InitSet(string _JSONmodel)
        {
            categoryModel = JsonUtility.FromJson<CategoryModelUI>(_JSONmodel);
            selectorModel = categoryModel;
            model = categoryModel;           
        }
    }
}