using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    /// <summary>
    /// Models represent data in a UI object.
    /// </summary>
    [System.Serializable]
    public class Model
    {
        [SerializeField]
        private bool active = false;

        /// <summary>
        /// Controls if the view is turning off or turning on
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
        }

        /// <summary>
        /// Whether or not we need to refresh this model's view
        /// </summary>
        [SerializeField]
        private bool refresh = false;

        /// <summary>
        /// Refreshes the view.
        /// </summary>
        protected virtual void Refresh()
        {
            refresh = true;
        }

        /// <summary>
        /// If true, we need to refresh this view.
        /// Automatically changes refresh in model to false.
        /// </summary>
        /// <returns></returns>
        public bool CheckRefresh()
        {
            if (refresh)
            {
                refresh = false;
                return true;
            }
            return false;
        }

        public void SetActive(bool _state)
        {
            active = _state;
        }

        [SerializeField]
        private bool locked = false;
        public bool Locked
        {
            get
            {
                return locked;
            }
        }

        public void SetLocked(bool _state)
        {
            locked = _state;
        }

        /// <summary>
        /// Initializes the model data
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// Resets the model data.
        /// </summary>
        public virtual void Reset()
        {

        }

        /// <summary>
        /// Invokes the model event
        /// Make sure to call refresh in the implementation
        /// </summary>
        public void InvokeModel(string _key)
        {
            Refresh();
            InvokeSpecificModel(_key);
        }

        protected virtual void InvokeSpecificModel(string _key)
        {

        }
    }
}
