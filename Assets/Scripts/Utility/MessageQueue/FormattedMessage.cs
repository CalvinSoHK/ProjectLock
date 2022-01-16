using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.MessageQueue
{
    /// <summary>
    /// Formats message to contain key data
    /// </summary>
    public class FormattedMessage
    {
        //Key this message is signed with
        public string key;

        //Message content
        public string message;

        public FormattedMessage(string _key, string _message)
        {
            key = _key;
            message = _message;
        }
    }
}
