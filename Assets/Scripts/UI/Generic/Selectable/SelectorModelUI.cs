using UI.Base;


using System.Collections.Generic;
using UnityEngine;

namespace UI.Selector
{
    /// <summary>
    /// Model that represents what needs to be shown in the view for Selectors
    /// </summary>
    [System.Serializable]
    public class SelectorModelUI : Model
    {
        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
        private bool resetSelectIndex = false;
        
        /// <summary>
        /// Resets SelectIndex
        /// </summary>
        public void ResetSelectIndex()
        {
            resetSelectIndex = true;
        }

        /// <summary>
        /// Checks if we wanted to reset select index
        /// </summary>
        /// <returns></returns>
        public bool CheckResetSelectIndex()
        {
            if (resetSelectIndex)
            {
                resetSelectIndex = false;
                return true;
            }
            return false;
        }

        [SerializeField]
        private bool unselectAll = false;

        /// <summary>
        /// Resets SelectIndex
        /// </summary>
        public void UnselectAll()
        {
            unselectAll = true;
        }

        /// <summary>
        /// Checks if we wanted to reset select index
        /// </summary>
        /// <returns></returns>
        public bool CheckUnselectAll()
        {
            if (unselectAll)
            {
                unselectAll = false;
                return true;
            }
            return false;
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