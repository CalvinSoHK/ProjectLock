using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
using UI.Message;
using UnityEngine;

namespace UI.Handler
{
    public class SelectionHandlerMessageObject : MessageObject
    {
        public SelectionState state = SelectionState.None;

        public List<int> selectedIndexes = new List<int>();

        public SelectionHandlerMessageObject(SelectionState _state, List<int> _indexes) : base() 
        {
            state = _state;
            selectedIndexes = _indexes;
        }
    }

    public enum SelectionState
    {
        None,
        SelectSuccess, //Current selected was a success and added to list
        SelectFail, //Current selected was a failure and not added to list
        AllSelected //All required number of mons was selected
    }
}
