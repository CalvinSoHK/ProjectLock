using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mon.MonData
{


    /// <summary>
    /// Data for Move data.
    /// </summary>
    [CreateAssetMenu(fileName = "TypeRelationSO",
    menuName = "MonGeneration/TypeRelationSO", order = 5)]
    public class TypeRelationSO : ScriptableObject
    {
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
                return TypeRelations;
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
        private void UpdateDict()
        {
            typeDict.Clear();

            foreach (TypeRelations relations in typeRelations)
            {
                typeDict.Add(relations.type, relations);
            }
        }

        /*
        private void OnValidate()
        {
            ValidateTypes();
        }

        /// <summary>
        /// Validate types so it doesn't break
        /// </summary>
        private void ValidateTypes()
        {
            //For every type relations class in our list
            foreach (TypeRelations typeRelation in typeRelations)
            {
                //For every single type relation class in that relation
                foreach(SingleTypeRelation relation1 in typeRelation.relationList)
                {
                    //Grab the mirror relation to this type
                    TypeRelations mirrorRelation = GetRelationOf(relation1.type);

                    SingleTypeRelation relation2;
                    if(mirrorRelation.relationDict.TryGetValue(typeRelation.type, out relation2))
                    {
                        if(relation1.relation != relation2.relation)
                        {
                            throw new TypeEffectivenessSOException("Relation of type : " + typeRelation.type + " and :" + mirrorRelation.type + " don't have a consistent relationship.");
                        }
                    }
                    else
                    {
                        throw new TypeEffectivenessSOException("Attempted to find mirror relation of type: " + relation1.type);
                    }           
                }
            }
        }
        */

        /// <summary>
        /// Returns the TypeRelations class of given type
        /// Null if it can't find it
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TypeRelations GetRelationOf(MonType type)
        {
            TypeRelations value;
            if (typeDict.TryGetValue(type, out value))
            {
                return value;
            }
            throw new TypeEffectivenessSOException("Attempted to get relation of type : " + type + " but it wasn't in dict.");
        }
    
        /// <summary>
        /// Function that updates the relation between two serialized types
        /// </summary>
        public void UpdateRelation()
        {
            TypeRelations typeRelation1 = GetRelationOf(targetType1);
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
            SingleTypeRelation relation = relations.GetRelation(type2);
            return relation.relation;
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
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TypeRelationSO))]
    public class TypeEffectivenessSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TypeRelationSO obj = (TypeRelationSO)target;

            obj.displayRelations = obj.GetRelationOf(obj.targetType1);

            if (GUILayout.Button("Update Type Relation"))
            {
                
                obj.UpdateRelation();
            }
        }
    }
#endif

    [System.Serializable]
    public class SingleTypeRelation
    {
        //The type in relation to
        public MonType type;

        //Relationship between the two types
        public TypeRelation relation;

        public SingleTypeRelation(MonType _type)
        {
            type = _type;
        }
    }

    [System.Serializable]
    public class TypeRelations
    {
        //Type we are relating to all others
        public MonType type;

        //List of single type relation classes
        public List<SingleTypeRelation> relationList;

        //Dictionary of single type relations, key is montype
        public Dictionary<MonType, SingleTypeRelation> relationDict = new Dictionary<MonType, SingleTypeRelation>();

        public TypeRelations(MonType _type)
        {
            type = _type;

            //Get list of all montypes
            List<MonType> types = Enum.GetValues(typeof(MonType))
                                .Cast<MonType>()
                                .ToList();

            relationList = new List<SingleTypeRelation>();

            //Add every type into type list
            foreach (MonType type in types)
            {
                if (type != MonType.None)
                {
                    relationList.Add(new SingleTypeRelation(type));
                }
            }

            //Clear and populate dict
            relationDict.Clear();

            foreach(SingleTypeRelation relation in relationList)
            {
                relationDict.Add(relation.type, relation);
            }
        }
    
        /// <summary>
        /// Grabs the single type relation of a type.
        /// Returns null if it fails
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public SingleTypeRelation GetRelation(MonType type)
        {
            SingleTypeRelation value;
            if(relationDict.TryGetValue(type, out value))
            {
                return value;
            }
            return null;
        }
    }

    public class TypeEffectivenessSOException : Exception
    {
        public TypeEffectivenessSOException(string msg) : base(msg)
        {

        }
    }
}