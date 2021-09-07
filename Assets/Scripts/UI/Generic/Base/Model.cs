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
        public bool Active
        {
            get
            {
                return active;
            }
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
        /// Refreshes the model data.
        /// </summary>
        public abstract void Refresh();

        /// <summary>
        /// Invokes the model event
        /// </summary>
        public abstract void InvokeModel(string _key);
    }
}
