using Mon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mon.MonData
{
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

            UpdateDict();
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
            if (relationDict.TryGetValue(type, out value))
            {
                return value;
            }
            return null;
        }

        public void UpdateDict()
        {
            //Clear and populate dict
            relationDict.Clear();

            foreach (SingleTypeRelation relation in relationList)
            {
                relationDict.Add(relation.type, relation);
            }
        }
    }
}