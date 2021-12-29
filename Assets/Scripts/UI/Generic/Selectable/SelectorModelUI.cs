using UI.Base;


using System.Collections.Generic;
using UnityEngine;

namespace UI.Selector
{
    /// <summary>
    /// Model that represents what needs to be shown in the view for Selectors
    /// </summary>
    public class SelectorModelUI : Model
    {
        private int indexChange = -1;

        /// <summary>
        /// When set to 0, index moves 0 places
        /// </summary>
        public int IndexChange
        {
            get
            {
                return indexChange;
            }
        }

        public void SetIndexChange(int _indexChange)
        {
            indexChange = _indexChange;
        }


        private bool select = false;
        public void SetSelect(bool _state)
        {
            select = _state;
        }

        public bool Select
        {
            get
            {
                return select;
            }
        }

        public override void Init()
        {
            indexChange = 0;
        }

        public override void Reset()
        {
            indexChange = 0;
        }

        public delegate void SelectorModel(string key, SelectorModelUI model);
        public static SelectorModel ModelUpdate;

        protected override void InvokeSpecificModel(string _key)
        {
            ModelUpdate?.Invoke(_key, this);
        }
    }
}