using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using Core.MessageQueue;
using System.Threading.Tasks;

namespace UI.Selector
{
    public class SelectorControllerUI : BaseControllerUI
    {
        [Header("Selector Options")]

        //Reference to player input mapping
        protected PlayerInputMap input;

        //Which input name to use for increment and decrement
        protected InputEnums.InputName incrementKey, decrementKey;

        /// <summary>
        /// How long there is a delay per input to scroll through selection
        /// </summary>
        protected float indexDelay = 0.1f;

        /// <summary>
        /// Internal timer for processing index changes
        /// </summary>
        protected float indexTimer = 0f;

        /// <summary>
        /// Whether the selection can be changed
        /// </summary>
        protected bool selectionChangeable = true;

        protected SelectorModelUI selectorModel;

        /// <summary>
        /// Adding the need to grab the input map if it is not set up.
        /// </summary>
        protected override void InitGeneral()
        {
            base.InitGeneral();
            if (!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }
        }

        /// <summary>
        /// Making a new SelectorModelUI and setting it where it is needed
        /// </summary>
        protected override void InitFresh()
        {
            model = new SelectorModelUI();
            selectorModel = (SelectorModelUI)model;
        }

        /// <summary>
        /// Setting the selector model as well
        /// </summary>
        /// <param name="_model"></param>
        protected override void InitSet(string _JSONmodel)
        {
            selectorModel = JsonUtility.FromJson<SelectorModelUI>(_JSONmodel);
            model = (Model)selectorModel;
        }

        /// <summary>
        /// Increments index respecting count
        /// </summary>
        protected void IncrementIndex()
        {
            if (selectionChangeable)
            {
                StartIndexTimer();
                selectorModel.SetIndexChange(1);
                Refresh();
            }
        }

        /// <summary>
        /// Decrements index respecting count
        /// </summary>
        protected void DecrementIndex()
        {
            if (selectionChangeable)
            {
                StartIndexTimer();
                selectorModel.SetIndexChange(-1);
                Refresh();
            }
        }

        /// <summary>
        /// Sets keys for navigating this UI
        /// </summary>
        public void SetNavigation(SelectableDirEnum direction)
        {
            switch (direction)
            {
                case SelectableDirEnum.Vertical:
                    decrementKey = InputEnums.InputName.Down;
                    incrementKey = InputEnums.InputName.Up;
                    break;
                case SelectableDirEnum.VerticalFlipped:
                    decrementKey = InputEnums.InputName.Up;
                    incrementKey = InputEnums.InputName.Down;
                    break;
                case SelectableDirEnum.Horizontal:
                    decrementKey = InputEnums.InputName.Left;
                    incrementKey = InputEnums.InputName.Right;
                    break;
                case SelectableDirEnum.HorizontalFlipped:
                    decrementKey = InputEnums.InputName.Right;
                    incrementKey = InputEnums.InputName.Left;
                    break;
                case SelectableDirEnum.Both:
                default:
                    throw new System.Exception("Not implemented exception.");
            }
        }

        /// <summary>
        /// Navigates indexes
        /// </summary>
        protected void NavigateIndex()
        {
            if (!selectorModel.Locked)
            {
                if (input.GetInput(incrementKey, InputEnums.InputAction.Down))
                {
                    IncrementIndex();
                }
                else if (input.GetInput(decrementKey, InputEnums.InputAction.Down))
                {
                    DecrementIndex();
                }
                else if (selectorModel.IndexChange != 0) //If the last model we pushed had an index change, change to zero and update.
                {
                    selectorModel.SetIndexChange(0);
                }
            }
        }

        protected void SelectIndex()
        {
            if (selectionChangeable)
            {
                if (input.GetInput(InputEnums.InputName.Interact, InputEnums.InputAction.Down))
                {
                    StartIndexTimer();
                    selectorModel.SetSelect(true);
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Starts the index timer.
        /// </summary>
        protected void StartIndexTimer()
        {
            if (selectionChangeable)
            {
                selectionChangeable = false;
                indexTimer = indexDelay;
            }
        }

        /// <summary>
        /// Processes the index timer
        /// </summary>
        protected void ProcessIndexTimer()
        {
            if (!selectionChangeable)
            {
                if (indexTimer > 0)
                {
                    indexTimer -= Time.deltaTime;
                }
                else
                {
                    selectionChangeable = true;
                }
            }
        }

        public override void HandleDisplayState()
        {
            base.HandleDisplayState();
            IndexControl();
        }

        protected void IndexControl()
        {
            NavigateIndex();
            SelectIndex();
            ProcessIndexTimer();
        }

        public override void HandlePrintingState()
        {
            base.HandlePrintingState();
            selectorModel.ResetSelectIndex();
        }

        public override void HandleHidingState()
        {
            base.HandleHidingState();
        }

        /// <summary>
        /// Listens for messages
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_selectedIndex"></param>
        protected override void HandleMessage(string id, FormattedMessage fMsg)
        {
            base.HandleMessage(id, fMsg);
            //Listen for UI messages with our key.
            if (id.Equals(MessageQueueManager.UI_KEY))
            {
                if (key.Equals(fMsg.key))
                {
                    SelectorMessageObject message = JsonUtility.FromJson<SelectorMessageObject>(fMsg.message);
                    selectorModel.SetSelect(true);
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Add to reset selected index on return key
        /// </summary>
        protected override void OnReturnKey()
        {
            selectorModel.UnselectAll();
            base.OnReturnKey();
        }
    }
}
