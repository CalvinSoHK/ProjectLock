using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Page
{
    /// <summary>
    /// A message that is sent from the BaseController through the queue to PagesManager.
    /// UIPagesManager counts the number of messages it gets to know if it has finished counting.
    /// </summary>
    public class UIPagesManagerInMessage
    {
        public bool counted = true;
        public UIPagesManagerInMessage()
        {

        }
    }
}
