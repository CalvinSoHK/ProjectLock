using System.Collections;
using System.Collections.Generic;
using Mon.Enums;
using Mon.MonData;
using Mon.Moves;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MoveSet_Test
{
    // A Test behaves as an ordinary method
    [Test]
    public void MoveSet_TestSimplePasses()
    {
        // Use the Assert class to test conditions
        MoveSet moveSet = new MoveSet();

        //Test move set is empty
        Assert.AreEqual(0, moveSet.MoveCount);
        Assert.IsNull(moveSet.GetMove(0));

        //Test GetEmptyIndex works
        Assert.AreEqual(0, moveSet.GetEmptyIndex());

        MoveData move1 = new MoveData();
        move1.power = 50;
        move1.moveTyping = MonType.Fire;
        moveSet.LearnMove(move1, 0);

        //Test we learned move in right slot
        Assert.AreEqual(1, moveSet.MoveCount);
        Assert.AreEqual(move1, moveSet.GetMove(0));
        Assert.AreEqual(1, moveSet.GetEmptyIndex());

        MoveData move2 = new MoveData();
        move2.power = 20;
        move2.moveTyping = MonType.Water;
        moveSet.LearnMove(move2, 1);

        //Test we learned move in right slot
        Assert.AreEqual(2, moveSet.MoveCount);
        Assert.AreEqual(move2.moveName, moveSet.GetMove(1).moveName);

        //Test the first empty index is returned
        Assert.AreEqual(2, moveSet.GetEmptyIndex());

        //Test grab move power
        List<TypeMultiplier> typeMultiplierList = new List<TypeMultiplier>();
        typeMultiplierList.Add(new TypeMultiplier(MonType.Fire, 1));
        typeMultiplierList.Add(new TypeMultiplier(MonType.Water, 0.5f));
        List<MoveDamage> moveDamageList = moveSet.CalcMovePower(typeMultiplierList);

        //Test the list returned from GrabMovePower is accurate
        Assert.AreEqual(2, moveDamageList.Count);
        Assert.AreEqual(0, moveDamageList[0].index);
        Assert.AreEqual(50, moveDamageList[0].power);
        Assert.AreEqual(1, moveDamageList[1].index);
        Assert.AreEqual(10, moveDamageList[1].power);

        //Test we are able to swap moves
        moveSet.SwitchMoveIndex(0, 1);
        Assert.AreEqual(2, moveDamageList.Count);
        Assert.AreEqual(move2.moveName, moveSet.GetMove(0).moveName);
        Assert.AreEqual(move1.moveName, moveSet.GetMove(1).moveName);

        //Test we are able to unlearn a move
        moveSet.UnlearnMove(0);
        Assert.AreEqual(1, moveSet.MoveCount);
        Assert.AreEqual(0, moveSet.GetEmptyIndex());
        Assert.IsNull(moveSet.GetMove(0));
        Assert.IsNotNull(moveSet.GetMove(1));
    }
}
