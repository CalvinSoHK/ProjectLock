using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mon.MonGeneration
{
    /// <summary>
    /// Base Mon class, does not yet have any generated components. Holds all the data from the json in one obj.
    /// </summary>
    [System.Serializable]
    public class BaseMon : IComparable
    {
        public string name;
        public int key;
        public MonStage stage;
        public int[] family;
        public MonType requiredType;
        public string[] tags;

        /// <summary>
        /// Constructor for a BaseMon
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_stage"></param>
        /// <param name="_family"></param>
        /// <param name="_requiredType"></param>
        /// <param name="_tags"></param>
        public BaseMon(string _name, int _key, MonStage _stage, int[] _family, MonType _requiredType, string[] _tags)
        {
            name = _name;
            key = _key;
            stage = _stage;
            family = new int[_family.Length];
            _family.CopyTo(family, 0);
            requiredType = _requiredType;
            tags = new string[_tags.Length];
            _tags.CopyTo(tags, 0);
        }

        public int CompareTo(object obj)
        {
            BaseMon monOther = (BaseMon)obj;
            return key.CompareTo(monOther.key);
        }
    }
}
