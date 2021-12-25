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
        private CategoryModelUI categoryModel;

        public override void HandleOffState()
        {
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld
                && Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Inventory, InputEnums.InputAction.Down)
                )
            {
                Core.CoreManager.Instance.player.DisableInputMovement();
                ChangeState(UIState.Printing);
            }
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

        public override void HandleHidingState()
        {
            base.HandleHidingState();
            Core.CoreManager.Instance.player.EnableInputMovement();
        }

        public override void HandleDisplayState()
        {
             IndexControl();
             OnReturnKey();
        }

        private void OnReturnKey()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down))
            {
                 ChangeState(UIState.Hiding);
            }
        }

        public override void Init()
        {
            if (!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }

            model = new CategoryModelUI();
            selectorModel = (SelectorModelUI)model;
            categoryModel = (CategoryModelUI)model;

            model.Init();
        }

        protected override void Refresh()
        {
            categoryModel.Refresh();
            categoryModel.InvokeModel(key);
        }
    }
}
