using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    /// <summary>
    /// Models represent data in a UI object.
    /// </summary>
    public abstract class Model
    {
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
        private bool refresh = false;

        /// <summary>
        /// Refreshes the view.
        /// </summary>
        public void Refresh()
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
        public abstract void Init();

        /// <summary>
        /// Resets the model data.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Invokes the model event
        /// </summary>
        public abstract void InvokeModel(string _key);
    }
}
