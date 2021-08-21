using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Inventory.Items;
using Inventory.Enums;

public class BaseInventoryItem_Test
{
    // A Test behaves as an ordinary method
    [Test]
    public void BaseInventoryItem_Tests()
    {
        string itemName = "TestItem";
        ItemMask itemMask = ItemMask.Buyable | ItemMask.ConsumedOnUse | ItemMask.UsableInCombat;
        ItemCategory itemCategory = ItemCategory.Consumables;
        string onUseString = "Using TestItem.";

        BaseInventoryItem item = new BaseInventoryItem(
            itemName,
            itemMask,
            itemCategory,
            onUseString);

        Assert.AreEqual(itemName, item.ItemName);
        Assert.AreEqual(itemMask, item.ItemMask);
        Assert.AreEqual(itemCategory, item.ItemCategory);
        Assert.AreEqual(onUseString, item.OnUseString);
    }
}
