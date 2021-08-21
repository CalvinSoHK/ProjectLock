using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;
using System.Linq;


namespace Mon.MonData
{
    /// <summary>
    /// Data for Move data.
    /// </summary>
    [CreateAssetMenu(fileName = "TypeRelationSO",
    menuName = "MonGeneration/TypeRelationSO", order = 5)]
    public class TypeRelationSO : ScriptableObject
    {
        [SerializeField]
        private List<TypeRelations> typeRelations = new List<TypeRelations>();

        private Dictionary<MonType, TypeRelations> typeDict = new Dictionary<MonType, TypeRelations>();

        public TypeRelationSO()
        {
            //Get list of all montypes
            List<MonType> types = Enum.GetValues(typeof(MonType))
                                .Cast<MonType>()
                                .ToList();

            //Add every type into type list
            foreach (MonType type in types)
            {
                if (type != MonType.None)
                {
                    typeRelations.Add(new TypeRelations(type));
                }
            }

            UpdateDict();
        }

        [HideInInspector]
        public List<TypeRelations> TypeRelations
        {
            get
            {
                return typeRelations;
            }
            set
            {
                typeRelations = value;
            }
        }

        [SerializeField]
        public TypeRelations displayRelations;

        [SerializeField]
        public MonType targetType1, targetType2;

        [SerializeField]
        public TypeRelation targetRelation;

        /// <summary>
        /// Updates dict to reflect the list
        /// </summary>
        public void UpdateDict()
        {
            typeDict.Clear();

            foreach (TypeRelations relations in typeRelations)
            {
                typeDict.Add(relations.type, relations);
            }
        }

        /// <summary>
        /// Returns the TypeRelations class of given type
        /// Null if it can't find it
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TypeRelations GetRelationOf(MonType type)
        {
            TypeRelations value;
            if(type != MonType.None)
            {
                if (typeDict.TryGetValue(type, out value))
                {
                    return value;
                }
                throw new TypeEffectivenessSOException("Attempted to get relation of type : " + type + " but it wasn't in dict.");
            }
            return null;
        }
    
        /// <summary>
        /// Function that updates the relation between two serialized types
        /// </summary>
        public void UpdateRelation()
        {
            TypeRelations typeRelation1 = GetRelationOf(targetType1);

            if(typeRelation1 != null)
            {
                SingleTypeRelation relation1 = typeRelation1.GetRelation(targetType2);
                if (relation1 != null)
                {
                    relation1.relation = targetRelation;
                    UpdateDict();
                }
                else
                {
                    throw new TypeEffectivenessSOException("Failed to update relation because retrieving relation of type failed: " + targetType2);
                }
            }               
        }
    
        /// <summary>
        /// Gives the relationship between type 1 and type 2.
        /// NOTE: Order matters. 
        /// Just because type 1 is resistant to type 2 does not mean type 2 is weak to type 1.
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public TypeRelation GetRelationBetween(MonType type1, MonType type2)
        {
            TypeRelations relations = GetRelationOf(type1);
            if(relations != null)
            {
                SingleTypeRelation relation = relations.GetRelation(type2);
                return relation.relation;
            }
            else
            {
                return TypeRelation.Neutral;
            }
        }

        /// <summary>
        /// Gives the multiplier between a mon and a move.
        /// Tells you the multiplier to apply to the move of that type when used on that mon.
        /// </summary>
        /// <param name="mon1"></param>
        /// <param name="moveType"></param>
        /// <returns></returns>
        public float GetMultiplier(MonIndObj mon1, MonType moveType)
        {
            float multiplier = 1;
            TypeRelation relation1 = GetRelationBetween(mon1.baseMon.primaryType, moveType);
            multiplier *= GetRelationMultiplier(relation1);
            
            if(mon1.baseMon.secondaryType != MonType.None)
            {
                TypeRelation relation2 = GetRelationBetween(mon1.baseMon.secondaryType, moveType);
                multiplier *= GetRelationMultiplier(relation2);
            }

            return multiplier;
        }

        /// <summary>
        /// Get relation multiplier from relation
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        private float GetRelationMultiplier(TypeRelation relation)
        {
            switch (relation)
            {
                case TypeRelation.Neutral:
                    return 1;
                case TypeRelation.ImmuneTo:
                    return 0;
                case TypeRelation.ResistantTo:
                    return 0.5f;
                case TypeRelation.WeakTo:
                    return 2f;
                default:
                    return 1f;
            }
        }

        /// <summary>
        /// Returns a list of decreasing effective moves on this mon.
        /// First type is strongest, last is weakest.
        /// </summary>
        /// <param name="mon"></param>
        /// <returns></returns>
        public List<TypeMultiplier> GetSortedWeakness(MonIndObj mon)
        {
            //Get list of all montypes
            List<MonType> types = Enum.GetValues(typeof(MonType))
                                .Cast<MonType>()
                                .ToList();

            List<TypeMultiplier> multiplierList = new List<TypeMultiplier>();

            //Add every type into type list
            foreach (MonType type in types)
            {
                if (type != MonType.None)
                {
                    multiplierList.Add(new TypeMultiplier(type, GetMultiplier(mon, type)));
                }
            }

            multiplierList.Sort();
            multiplierList.Reverse();

            return multiplierList;
        }
    }

    public class TypeEffectivenessSOException : Exception
    {
        public TypeEffectivenessSOException(string msg) : base(msg)
        {

        }
    }
}