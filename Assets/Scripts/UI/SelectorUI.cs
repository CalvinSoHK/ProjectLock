using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Implements a generic selector UI element
    /// </summary>
    public class SelectorUI : BaseUI
    {
        [Tooltip("Key of the group of selectable components.")]
        [SerializeField]
        private string groupKey = "";

        [Tooltip("Direction in which players interact with selectable pieces.")]
        [SerializeField]
        private SelectableDirEnum direction;

        [HideInInspector]
        [SerializeField]
        private List<int> selectableList = new List<int>();

        //Reference to player input mapping
        private PlayerInputMap input;

        //Which input name to use for increment and decrement
        private InputEnums.InputName incrementKey, decrementKey;

        //Index of which selectable we are on
        private int curIndex = 0;

        [Tooltip("Affects how long in between an index increment before the next one can happen.")]
        [SerializeField]
        private float indexDelay = 1f;

        /// <summary>
        /// Internal timer for processing index changes
        /// </summary>
        private float indexTimer = 0f;

        /// <summary>
        /// Whetehr the selection can be changed
        /// </summary>
        private bool selectionChangeable = true;

        //Count of how many selectables there are
        private int SelectableCount
        {
            get
            {
                return selectableList.Count;
            }
        }


        public delegate void ElementEvent(string groupKey, int index);
        public static ElementEvent SelectElement;
        public static ElementEvent HoverElement;

        public delegate void ElementCountEvent(string groupKey);
        public static ElementCountEvent CountElement;

        protected virtual void OnEnable()
        {
            SelectableUI.CountElement += CountSelectable;
        }

        protected virtual void OnDisable()
        {
            SelectableUI.CountElement -= CountSelectable;
        }

        private void Start()
        {
            input = Core.CoreManager.Instance.inputMap;
            ResetUI();
        }

        /// <summary>
        /// Resets UI elements for this UI
        /// </summary>
        public void ResetUI()
        {
            SetNavigation();
            CountSelectables();
            curIndex = 0;
            HoverElement?.Invoke(groupKey, curIndex);
        }

        /// <summary>
        /// Increments index respecting count
        /// </summary>
        private void IncrementIndex()
        {
            if (selectionChangeable)
            {
                StartIndexTimer();
                if (curIndex < SelectableCount - 1)
                {
                    curIndex++;
                }
                else
                {
                    curIndex = 0;
                }
            }           
        }

        /// <summary>
        /// Decrements index respecting count
        /// </summary>
        private void DecrementIndex()
        {
            if (selectionChangeable)
            {
                StartIndexTimer();
                if (curIndex > 0)
                {
                    curIndex--;
                }
                else
                {
                    curIndex = SelectableCount - 1;
                }
            }           
        }

        /// <summary>
        /// Sets keys for navigating this UI
        /// </summary>
        private void SetNavigation()
        {
            switch (direction)
            {
                case SelectableDirEnum.Vertical:
                    decrementKey = InputEnums.InputName.Down;
                    incrementKey = InputEnums.InputName.Up;
                    break;
                case SelectableDirEnum.Horizontal:
                    decrementKey = InputEnums.InputName.Left;
                    incrementKey = InputEnums.InputName.Right;
                    break;
                case SelectableDirEnum.Both:
                default:
                    throw new System.Exception("Not implemented exception.");
            }
        }

        /// <summary>
        /// Navigates indexes
        /// </summary>
        private void NavigateIndex()
        {
            if(input.GetInput(incrementKey, InputEnums.InputAction.Down))
            {
                IncrementIndex();
                HoverElement?.Invoke(groupKey, curIndex);
            }
            else if(input.GetInput(decrementKey, InputEnums.InputAction.Down))
            {
                DecrementIndex();
                HoverElement?.Invoke(groupKey, curIndex);
            }
        }

        /// <summary>
        /// Handles selecting an index
        /// </summary>
        private void SelectIndex()
        {
            if(input.GetInput(InputEnums.InputName.Interact, InputEnums.InputAction.Down))
            {
                SelectElement?.Invoke(groupKey, curIndex);
            }
        }

        /// <summary>
        /// Function that begins the process of counting how many selectables there are.
        /// Called on start, but can also be called externally if need be.
        /// </summary>
        public void CountSelectables()
        {
            selectableList.Clear();
            CountElement?.Invoke(groupKey);
        }

        /// <summary>
        /// Subscribed to when selectable UI calls back, adds the index of it to a list.
        /// Our selectable count is just that list's total count.
        /// </summary>
        /// <param name="index"></param>
        private void CountSelectable(int index)
        {
            if (!selectableList.Contains(index))
            {
                selectableList.Add(index);
            }
            else
            {
                Debug.LogError("From selectable UI : " + gameObject.name + " there are multiple selectables with index: " + index);
            }
        }

        /// <summary>
        /// Starts the index timer.
        /// </summary>
        private void StartIndexTimer()
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
        private void ProcessIndexTimer()
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

        /// <summary>
        /// Handles if exiting UI is pressed 
        /// </summary>
        protected override void HandleDisable()
        {
            if(input.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down))
            {
                ChangeState(UIState.Off);
            }
        }

        protected override void HandleOffState()
        {
            SetUIActive(false);
        }

        protected override void HandlePrintingState()
        {
            ChangeState(UIState.Displaying);
        }

        protected override void HandleDisplayState()
        {
            NavigateIndex();
            SelectIndex();
            ProcessIndexTimer();
            base.HandleDisplayState();
        }
    }
}
