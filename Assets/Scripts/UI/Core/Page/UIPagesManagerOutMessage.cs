using System.Collections;
using System.Collections.Generic;
using UI.Message;
using UnityEngine;

namespace UI.Page
{
    /// <summary>
    /// Message for processing other requests from the UIPagesManager
    /// </summary>
    public class UIPagesManagerOutMessage : MessageObject
    {
        /// <summary>
        /// Whether or not the return key was pressed
        /// </summary>
        public bool returnKeyPressed = false;
    }
}
