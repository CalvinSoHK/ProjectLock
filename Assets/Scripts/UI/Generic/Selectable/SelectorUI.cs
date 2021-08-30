using CustomInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Implements a generic selector UI element
    /// </summary>
    public class SelectorUI : BaseUI
    {
        [Header("Selector Options")]
        [Tooltip("Key of the group of selectable components.")]
        [SerializeField]
        protected string groupKey = "";

        [Tooltip("Direction in which players interact with selectable pieces.")]
        [SerializeField]
        protected SelectableDirEnum direction;

        [HideInInspector]
        [SerializeField]
        protected List<int> selectableList = new List<int>();

        //Reference to player input mapping
        protected PlayerInputMap input;

        //Which input name to use for increment and decrement
        protected InputEnums.InputName incrementKey, decrementKey;

        //Index of which selectable we are on
        protected int curIndex = 0;

        [Tooltip("Affects how long in between an index increment before the next one can happen.")]
        [SerializeField]
        protected float indexDelay = 1f;

        /// <summary>
        /// Internal timer for processing index changes
        /// </summary>
        protected float indexTimer = 0f;

        /// <summary>
        /// Whetehr the selection can be changed
        /// </summary>
        protected bool selectionChangeable = true;

        //Count of how many selectables there are
        protected int SelectableCount
        {
            get
            {
                return selectableList.Count;
            }
        }

        /// <summary>
        /// When true, selectable count will be counted at run time.
        /// </summary>
        [Tooltip("Must be on to count number of UI at awake. Off if you want to control it through code.")]
        [SerializeField]
        protected bool runtimeCount = true;



        public delegate void ElementIdentifierEvent(string groupKey, int index);
        public static ElementIdentifierEvent SelectorSelect;
        public static ElementIdentifierEvent SelectorHover;

        public delegate void ElementKeyEvent(string groupKey);
        public static ElementKeyEvent SelectorCount;

        public delegate void ElementUIActiveEvent(string groupKey, bool active);
        public static ElementUIActiveEvent SelectorUIActive;

        protected override void OnEnable()
        {
            base.OnEnable();
            SelectableUI.SelectableCount += CountSelectable;
            SelectableUI.SelectableSelected += SelectedElement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SelectableUI.SelectableCount -= CountSelectable;
            SelectableUI.SelectableSelected -= SelectedElement;
        }

        /// <summary>
        /// Inits UI elements for this UI
        /// Is called in ResetUI, though you can override that too if you need customization.
        /// </summary>
        protected override void Init()
        {
            if(!input)
            {
                input = Core.CoreManager.Instance.inputMap;
            }

            if (runtimeCount)
            {
                CountSelectables();
            }
            
            SetNavigation();
            curIndex = 0;

            SelectorHover?.Invoke(groupKey, curIndex);
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
        private void NavigateIndex()
        {
            if(input.GetInput(incrementKey, InputEnums.InputAction.Down))
            {
                IncrementIndex();
                SelectorHover?.Invoke(groupKey, curIndex);
            }
            else if(input.GetInput(decrementKey, InputEnums.InputAction.Down))
            {
                DecrementIndex();
                SelectorHover?.Invoke(groupKey, curIndex);
            }
        }

        /// <summary>
        /// Handles selecting an index
        /// </summary>    
        protected void SelectIndex()
        {
            if(input.GetInput(InputEnums.InputName.Interact, InputEnums.InputAction.Down))
            {
                SelectorSelect?.Invoke(groupKey, curIndex);
            }
        }

        /// <summary>
        /// Function that begins the process of counting how many selectables there are.
        /// Called on start, but can also be called externally if need be.
        /// </summary>
        public void CountSelectables()
        {
            selectableList.Clear();
            SelectorCount?.Invoke(groupKey);
        }

        /// <summary>
        /// Subscribed to when selectable UI calls back, adds the index of it to a list.
        /// Our selectable count is just that list's total count.
        /// </summary>
        /// <param name="index"></param>
        private void CountSelectable(string _groupKey, int _index)
        {
            if (groupKey.Equals(_groupKey))
            {
                if (!selectableList.Contains(_index))
                {
                    selectableList.Add(_index);
                }
                else
                {
                    Debug.LogWarning("From selectable UI : " + gameObject.name + " there are multiple selectables with index: " + _index);
                }
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
        /// Processes an element being selected.
        /// </summary>
        /// <param name="_groupKey"></param>
        /// <param name="_index"></param>
        private void SelectedElement(string _groupKey, int _index)
        {
            if (groupKey.Equals(_groupKey))
            {
                curIndex = _index;
                SelectorSelect?.Invoke(groupKey, curIndex);
            }
        }

        /// <summary>
        /// Handles if exiting UI is pressed 
        /// </summary>
        protected override void HandleDisable()
        {
            base.HandleDisable();
            if(input.GetInput(InputEnums.InputName.Return, InputEnums.InputAction.Down))
            {
                ChangeState(UIState.Off);
                Core.CoreManager.Instance.player.EnableInput();
                SelectorUIActive?.Invoke(groupKey, false);
            }
        }

        protected override void HandlePrintingState()
        {
            base.HandlePrintingState();
            ChangeState(UIState.Displaying);
            Core.CoreManager.Instance.player.DisableInput();
            SelectorUIActive?.Invoke(groupKey, true);
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
