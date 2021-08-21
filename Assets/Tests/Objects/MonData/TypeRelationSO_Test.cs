using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mon.Enums;
using Mon.MonData;
using Mon.MonGeneration;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TypeRelationSO_Test
{
    TypeRelationSO typeRelation;

    // A Test behaves as an ordinary method
    [Test]
    public void TypeRelationSO_TestSimplePasses()
    {

        //Test Constructor of TypeRelations
        TypeRelations normalRelations = new TypeRelations(MonType.Normal);

        //Test that the constructor did create a null
        Assert.IsNotNull(normalRelations);

        //Test the default values in it are all set to neutral
        Assert.AreEqual(TypeRelation.Neutral, normalRelations.GetRelation(MonType.Normal).relation);
        Assert.AreEqual(TypeRelation.Neutral, normalRelations.GetRelation(MonType.Rock).relation);

        //Test we can change a relation in it and it is reflected.
        //NOTE: This is an internal test function as type relations shouldn't change at run time, but we need it to test further tests.
        ReplaceRelation(normalRelations, MonType.Rock, TypeRelation.WeakTo);
        Assert.AreEqual(TypeRelation.WeakTo, normalRelations.GetRelation(MonType.Rock).relation);

        //Test that the TypeRelationsSO initializes properly
        TypeRelationSO typeRelationSO = new TypeRelationSO();

        //Test list is initialized with all enum types
        int count = Enum.GetValues(typeof(MonType))
                                .Cast<MonType>()
                                .ToList().Count;
        //Decrement by one since MonType.None will not be used in TypeRelationsSO
        count -= 1;
        Assert.AreEqual(count, typeRelationSO.TypeRelations.Count);

        //Test that all of them have their relations set to neutral
        Assert.AreEqual(TypeRelation.Neutral, typeRelationSO.GetRelationBetween(MonType.Bug, MonType.Bug));

        //Test we can grab a TypeRelations class from TypeRelationSO
        TypeRelations curBugRelations = typeRelationSO.GetRelationOf(MonType.Bug);
        Assert.IsNotNull(curBugRelations);

        //Test we can grab a SingleTypeRelation class from TypeRelations
        SingleTypeRelation bugTobug = curBugRelations.GetRelation(MonType.Bug);
        Assert.IsNotNull(bugTobug);

        //Test we can update a relation with the function
        typeRelationSO.targetType1 = MonType.Bug;
        typeRelationSO.targetType2 = MonType.Bug;
        typeRelationSO.targetRelation = TypeRelation.ImmuneTo;
        typeRelationSO.UpdateRelation();
        Assert.AreEqual(TypeRelation.ImmuneTo, typeRelationSO.GetRelationBetween(MonType.Bug, MonType.Bug));

        //Test TypeRelation GetSortedWeakness function
        //Set up type relations
        TypeRelations fireRelations = new TypeRelations(MonType.Fire);
        TypeRelations bugRelations = new TypeRelations(MonType.Bug);

        //Combination of relationships below should result in:
        // 4x Weakness to Rock at Index 0
        // 2x Weakness to Water at Index 1
        // 0.5x Defense to Steel at Index 15
        // 0.25x Defense to Psychic at Index 16
        // 0x Defense to Flying at Index 17
        ReplaceRelation(fireRelations, MonType.Rock, TypeRelation.WeakTo);
        ReplaceRelation(fireRelations, MonType.Water, TypeRelation.WeakTo);
        ReplaceRelation(fireRelations, MonType.Psychic, TypeRelation.ResistantTo);
        ReplaceRelation(fireRelations, MonType.Flying, TypeRelation.ImmuneTo);
        fireRelations.UpdateDict();

        ReplaceRelation(bugRelations, MonType.Rock, TypeRelation.WeakTo);
        ReplaceRelation(bugRelations, MonType.Steel, TypeRelation.ResistantTo);
        ReplaceRelation(bugRelations, MonType.Psychic, TypeRelation.ResistantTo);
        ReplaceRelation(bugRelations, MonType.Flying, TypeRelation.ImmuneTo);
        bugRelations.UpdateDict();

        List<TypeRelations> testRelations = new List<TypeRelations>();
        testRelations.Add(fireRelations);
        testRelations.Add(bugRelations);

        typeRelation = new TypeRelationSO();
        typeRelation.TypeRelations = testRelations;
        typeRelation.UpdateDict();

        //Make a test mon, type fire bug
        GeneratedMon testGenMon = new GeneratedMon();
        testGenMon.primaryType = Mon.Enums.MonType.Fire;
        testGenMon.secondaryType = Mon.Enums.MonType.Bug;
        testGenMon.baseStats = new MonBaseStats(100, 100, 100, 100, 100, 100);

        MonIndObj testMon = new MonIndObj(testGenMon, 1);

        List<TypeMultiplier> weakTypes = typeRelation.GetSortedWeakness(testMon);

        Assert.IsTrue(weakTypes != null);
        Assert.AreEqual(18, weakTypes.Count);
        Assert.AreEqual(MonType.Rock, weakTypes[0].type);
        Assert.AreEqual(4, weakTypes[0].multiplier);
        Assert.AreEqual(MonType.Water, weakTypes[1].type);
        Assert.AreEqual(2, weakTypes[1].multiplier);
        Assert.AreEqual(MonType.Steel, weakTypes[15].type);
        Assert.AreEqual(0.5, weakTypes[15].multiplier);
        Assert.AreEqual(MonType.Psychic, weakTypes[16].type);
        Assert.AreEqual(0.25, weakTypes[16].multiplier);
        Assert.AreEqual(MonType.Flying, weakTypes[17].type);
        Assert.AreEqual(0, weakTypes[17].multiplier);

    }

    /// <summary>
    /// Replaces a relation in the list of type with type relation
    /// </summary>
    /// <param name="relationList"></param>
    /// <param name="type"></param>
    /// <param name="relation"></param>
    public void ReplaceRelation(TypeRelations typeRelations, MonType type, TypeRelation relation)
    {
        foreach(SingleTypeRelation singleRelation in typeRelations.relationList)
        {
            if(singleRelation.type == type)
            {
                singleRelation.relation = relation;
                return;
            }
        }
    }
}
