using System.Collections;
using System.Collections.Generic;
using Core;
using Core.AddressableSystem;
using Core.Dialogue;
using Inventory;
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
        core.addressablesManager = obj.AddComponent<AddressablesManager>();
        core.playerInventory = obj.AddComponent<InventoryManager>();
        core.dialogueManager = obj.AddComponent<DialogueManager>();
        yield return null;

        InventoryData inventory = core.playerInventory.Inventory;
        string itemName = "TestItem";

        //Test the item list is empty
        Assert.AreEqual(0, inventory.ItemList.Count);

        //Test adding one item
        inventory.AddItem(itemName);
        Assert.AreEqual(1, inventory.GetItemCount(itemName));

        //Test the inventory has that item
        Assert.IsTrue(inventory.HasItem(itemName));

        //Test the ItemList is updated
        Assert.AreEqual(1, inventory.ItemList.Count);
        Assert.AreEqual(itemName, inventory.ItemList[0]);

        //Test adding multiple of an item
        string itemName2 = "TestItem1";
        inventory.AddItem(itemName2, 3);
        Assert.AreEqual(3, inventory.GetItemCount(itemName2));

        //Check item list count is updated
        Assert.AreEqual(2, inventory.ItemList.Count);

        //Test adding multiple of an item to an existing item
        inventory.AddItem(itemName, 2);
        Assert.AreEqual(3, inventory.GetItemCount(itemName));

        //Check item list count was not changed
        Assert.AreEqual(2, inventory.ItemList.Count);

        //Test removing some but not all of an item
        inventory.RemoveItem(itemName2, 2);
        Assert.AreEqual(1, inventory.GetItemCount(itemName2));

        //Test removing all of an item
        inventory.RemoveItem(itemName2);
        Assert.IsFalse(inventory.HasItem(itemName2));

        //Test valid item and using it
        TestValidItem(inventory);
    }

    private async void TestValidItem(InventoryData inventory)
    {
        //Test using an item (needed to be a valid item)
        string validItem = "DemotownKey";
        inventory.AddItem(validItem);
        Assert.AreEqual(1, inventory.GetItemCount(validItem));
        Assert.IsTrue(await inventory.UseItem(validItem));
        Assert.IsFalse(inventory.HasItem(validItem));
    }
}
