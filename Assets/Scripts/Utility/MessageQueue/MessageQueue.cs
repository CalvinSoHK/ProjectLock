using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.MessageQueue
{
    /// <summary>
    /// Message queue
    /// </summary>
    public class MessageQueue
    {
        /// <summary>
        /// Constructor for new message queue.
        /// Designates ID for this queue
        /// </summary>
        /// <param name="_id"></param>
        public MessageQueue(string _id)
        {
            id = _id;
        }

        private string id;
        public string ID
        {
            get
            {
                return id;
            }
        }

        private Queue<string> messageQueue = new Queue<string>();

        public delegate void MessageQueueEvent(string id, string msg);
        public static MessageQueueEvent MessageEvent;

        /// <summary>
        /// Queues a new message on to this queue
        /// </summary>
        /// <param name="message"></param>
        public void QueueMessage(string message)
        {
            messageQueue.Enqueue(message);
        }

        /// <summary>
        /// Updates the queue
        /// If there is a message it will dequeue it and invoke a message using id and message.
        /// </summary>
        /// <returns></returns>
        public void UpdateQueue()
        {
            if(messageQueue.Count > 0)
            {
                string msg = messageQueue.Dequeue();
                MessageEvent?.Invoke(id, msg);
            }         
        }
    }
}