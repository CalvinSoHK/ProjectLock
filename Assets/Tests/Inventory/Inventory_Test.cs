using System.Collections;
using System.Collections.Generic;
using Core;
using Core.AddressableSystem;
using Core.Dialogue;
using Inventory;
using Inventory.Enums;
using Inventory.Items;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Inventory_Test
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Inventory_Tests()
    {
        //Create object with addressables manager and inventory manager
        GameObject obj = new GameObject();
        CoreManager core = obj.AddComponent<CoreManager>();
        core.playerInventory = obj.AddComponent<InventoryManager>();
        core.dialogueManager = obj.AddComponent<DialogueManager>();
        yield return null;

        InventoryData inventory = core.playerInventory.Inventory;
        string itemName = "TestItem";

        BaseInventoryItem item1 = new BaseInventoryItem("TestItem1", ItemMask.UsableInCombat, ItemCategory.Consumables, "Using a capture ball.");

     
        //Test valid item and using it
        TestValidItem(inventory);
    }

    private async void TestValidItem(InventoryData inventory)
    {
        //Test using an item (needed to be a valid item)
        string validItem = "DemotownKey";
        BaseInventoryItem itemKey = new BaseInventoryItem("DemotownKey", ItemMask.Unique, ItemCategory.Key, "Used in demotown.");
    }
}
