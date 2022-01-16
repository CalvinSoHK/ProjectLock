using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Handler
{
    /// <summary>
    /// Handler is external to the MVC model
    /// Allows us to create sequences of UI requirements and takes those inputs to do a specific action
    /// </summary>
    public class BaseHandler
    {
        public BaseHandler()
        {
            Init();
        }
        protected virtual void Init()
        {
            MessageQueue.MessageEvent += HandleMessage;
        }

        ~BaseHandler()
        {
            CleanUp();
        }

        protected virtual void CleanUp()
        {
            MessageQueue.MessageEvent -= HandleMessage;
        }

        protected virtual void HandleMessage(string id, FormattedMessage fMsg)
        {

        }
    }
}
