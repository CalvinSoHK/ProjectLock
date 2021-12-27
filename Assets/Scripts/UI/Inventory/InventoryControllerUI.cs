using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Inventory.Category;
using UI.Inventory.Item;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryControllerUI : BaseControllerUI
    {
        private InventoryModelUI inventoryModel;

        private ItemControllerUI itemController = null;
        private CategoryControllerUI categoryController = null;

        public delegate void InventoryUI(UIState state);
        public static InventoryUI InventoryUIState;

        public override void Init()
        {
            itemController = Core.CoreManager.Instance.uiManager.itemController;
            categoryController = Core.CoreManager.Instance.uiManager.categoryController;

            model = new InventoryModelUI();
            inventoryModel = (InventoryModelUI)model;

            model.Init();
        }

        /// <summary>
        /// Override change state so we fire an event every time
        /// </summary>
        /// <param name="_state"></param>
        protected override void ChangeState(UIState _state)
        {
            base.ChangeState(_state);
            InventoryUIState?.Invoke(_state);
        }

        public override void HandleOffState()
        {
            base.HandleOffState();
            if (Core.CoreManager.Instance.worldStateManager.State == Core.WorldState.Overworld
                && Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Inventory, InputEnums.InputAction.Down)
                )
            {
                Core.CoreManager.Instance.player.DisableInputMovement();
                ChangeState(UIState.Printing);
            }
        }

        public override void HandleHidingState()
        {
            base.HandleHidingState();
            Core.CoreManager.Instance.player.EnableInputMovement();
        }
    }
}
