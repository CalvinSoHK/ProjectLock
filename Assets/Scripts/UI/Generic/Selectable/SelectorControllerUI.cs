using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;

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
        protected float indexDelay = 0.25f;

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
        /// Inits UI elements for this UI
        /// </summary>
        public override void Init()
        {
            if (!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }

            model = new SelectorModelUI();
            selectorModel = (SelectorModelUI)model;

            model.Init();
        }

        public override void HandleOffState()
        {
            base.HandleOffState();
            //Delete?
            if(Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Navigation, InputEnums.InputAction.Down))
            {
                ChangeState(UIState.Printing);
            }
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
                selectorModel.InvokeModel(key);
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
                selectorModel.InvokeModel(key);
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
                    selectorModel.InvokeModel(key);
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
                    selectorModel.InvokeModel(key);
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
        }

        protected void IndexControl()
        {
            NavigateIndex();
            SelectIndex();
            ProcessIndexTimer();
        }
    }
}
