using Mon.Enums;

namespace Mon.MonData
{
    /// <summary>
    /// Represents a relationship to one type
    /// </summary>
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
}