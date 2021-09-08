using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IUIElement
    {
        /// <summary>
        /// Enables this element
        /// </summary>
        public abstract void EnableElement();

        /// <summary>
        /// Disables this element
        /// </summary>
        public abstract void DisableElement();

        /// <summary>
        /// Refreshes this element
        /// </summary>
        public abstract void RefreshElement();
    }
}
