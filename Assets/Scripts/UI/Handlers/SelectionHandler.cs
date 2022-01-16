using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
using UI.Selector;
using UnityEngine;

namespace UI.Handler
{
    public class SelectionHandler : BaseHandler
    {
        private int requiredNumber = 0;
        public List<int> selectedIndexes = new List<int>();

        public static string HANDLERKEY = "/SelectionHandler";
        private string targetID;
        private string targetKey;
        private string outputKey;

        /// <summary>
        /// Makes a handler that is looking for the given number of mons to be selected
        /// </summary>
        /// <param name="numberOfMons"></param>
        public SelectionHandler(string id, string key, int numberOfMons) : base()
        {
            targetID = id;
            targetKey = key;
            outputKey = targetKey + HANDLERKEY;
            requiredNumber = numberOfMons;
        }

        protected override void HandleMessage(string id, FormattedMessage fMsg)
        {
            base.HandleMessage(id, fMsg);
            if (id.Equals(targetID))
            {
                if (fMsg.key.Equals(targetKey))
                {
                    SelectorMessageObject message = JsonUtility.FromJson<SelectorMessageObject>(fMsg.message);
                    //If we don't have the selected index yet
                    if (!selectedIndexes.Contains(message.index))
                    {
                        //Add to list
                        selectedIndexes.Add(message.index);

                        //If the list is still less than the required number
                        if (selectedIndexes.Count < requiredNumber)
                        {
                            //Message that the select was successful
                            Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
                                targetID,
                                outputKey,
                                JsonUtility.ToJson(new SelectionHandlerMessageObject(SelectionState.SelectSuccess, selectedIndexes)));
                        }
                        else//If the list has reached the required number
                        {
                            //Message that the selection is all completed
                            Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
                                targetID,
                                outputKey,
                                JsonUtility.ToJson(new SelectionHandlerMessageObject(SelectionState.AllSelected, selectedIndexes)));
                        }
                    }
                    else
                    {
                        //Message that the selection failed
                        Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
                            targetID,
                            outputKey,
                            JsonUtility.ToJson(new SelectionHandlerMessageObject(SelectionState.SelectFail, selectedIndexes)));
                    }
                }
            }
        }
    
        /// <summary>
        /// Overrides the required amount and sets it to a new value
        /// </summary>
        /// <param name="required"></param>
        public void SetRequired(int required)
        {
            requiredNumber = required;
        }

        /// <summary>
        /// Removes the latest selected index
        /// </summary>
        public void RemoveLatest()
        {
            selectedIndexes.RemoveAt(selectedIndexes.Count - 1);
        }
    }
}
