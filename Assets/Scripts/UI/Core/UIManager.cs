using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UI.Selector;
using UI.Nav;
using UI.Party;
using UI.Dropdown;
using Mon.MonData;
using UI.Inventory.Category;
using UI.Inventory.Item;
using UI.Inventory;
using UI.Page;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        public UIPageStack pageStack = new UIPageStack();

        SelectorControllerUI selectorController = new SelectorControllerUI();

        public NavControllerUI navController = new NavControllerUI();

        public PartyControllerUI partyController = new PartyControllerUI();

        public CategoryControllerUI categoryController = new CategoryControllerUI();

        public ItemControllerUI itemController = new ItemControllerUI();

        public InventoryControllerUI inventoryController = new InventoryControllerUI();

        List<IControllerUI> controllers = new List<IControllerUI>();

        private void Start()
        {
            InitControllers();
        }

        private void OnDestroy()
        {
            foreach(IControllerUI controller in controllers)
            {
                controller.DestroyController();
            }
        }      

        private void Update()
        {
            HandleControllers();
        }
        private void InitControllers()
        {
            selectorController.SetupController("Selector");
            selectorController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
            controllers.Add(selectorController);

            partyController.SetupController("Party");
            partyController.SetNavigation(UI.SelectableDirEnum.Horizontal);
            controllers.Add(partyController);

            navController.SetupController("Navigation");
            navController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
            controllers.Add(navController);

            categoryController.SetupController("Category");
            categoryController.SetNavigation(UI.SelectableDirEnum.VerticalFlipped);
            controllers.Add(categoryController);

            itemController.SetupController("Item");
            itemController.SetNavigation(UI.SelectableDirEnum.Horizontal);
            controllers.Add(itemController);

            inventoryController.SetupController("Inventory");
            controllers.Add(inventoryController);
        }

        private void HandleControllers()
        {
            foreach (IControllerUI controller in controllers)
            {
                controller.HandleState();
            }
        }

        // When first is selected, Swap has not been pressed. Return down. Still locked
        public void PartyEnable()
        {
            partyController.TryEnableState();
        }

        public void PartyDisable()
        {
            partyController.SelectorSetSelect(false);
            partyController.model.SetLocked(false);
            partyController.TryDisableState();
        }

        public void PartyBattleCheck(int monNumber, MonIndObj playerMon)
        {
            partyController.MonInfoBattle(monNumber, playerMon);
        }
    }
}
