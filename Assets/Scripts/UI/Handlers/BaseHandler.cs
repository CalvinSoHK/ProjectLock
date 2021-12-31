using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Handler
{
    /// <summary>
    /// Handler is external to the MVC model
    /// Allows us to create sequences of UI requirements and takes those inputs to do a specific action
    /// </summary>
    public abstract class BaseHandler
    {
        public BaseHandler()
        {
            Init();
        }
        protected abstract void Init();

        ~BaseHandler()
        {
            CleanUp();
        }

        protected abstract void CleanUp();
    }
}
