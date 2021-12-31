using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.MessageQueue
{
    /// <summary>
    /// Manages multiple queues running at a time
    /// </summary>
    public class MessageQueueManager
    {
        /// <summary>
        /// All the queues currently running in the system
        /// </summary>
        private Dictionary<string,MessageQueue> queueDict = new Dictionary<string,MessageQueue>();

        /// <summary>
        /// Tries to make a new queue given string id.
        /// Returns false if there already exists a queue with that id.
        /// True if valid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TryMakeNewQueue(string id)
        {
            if (queueDict.ContainsKey(id))
            {
                return false;
            }

            queueDict.Add(id, new MessageQueue(id));
            return true;
        }

        /// <summary>
        /// Tries to queue a message given id and msg.
        /// If there is no queue with that ID it will return false.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool TryQueueMessage(string id, string msg)
        {
            MessageQueue queue;
            if (queueDict.TryGetValue(id, out queue))
            {
                queue.QueueMessage(msg);
                return true;
            }
            return false;
        }

        public void UpdateMessageQueues()
        {
            MessageQueue queue;
            foreach (string key in queueDict.Keys)
            {
                queueDict.TryGetValue(key, out queue);
                queue.UpdateQueue();
            }
        }
    }
}
