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

        [SerializeField]
        private List<SelectableUI> selectableList = new List<SelectableUI>();

        //Reference to player input mapping
        private PlayerInputMap input;

        //Which input name to use for increment and decrement
        private InputEnums.InputName incrementKey, decrementKey;

        //Count of how many selectables there are
        private int SelectableCount
        {
            get
            {
                return indexList.Count;
            }
        }

        //Index of which selectable we are on
        private int curIndex = 0;

        public delegate void ElementEvent(string groupKey, int index);
        public static ElementEvent SelectElement;

        public delegate void ElementCountEvent(string groupKey);
        public static ElementCountEvent CountElement;

        //Used to count indexes
        private List<int> indexList = new List<int>();

        private void OnEnable()
        {
            SelectableUI.CountElement += CountSelectable;
        }

        private void OnDisable()
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
        /// Function that begins the process of counting how many selectables there are.
        /// Called on start, but can also be called externally if need be.
        /// </summary>
        public void CountSelectables()
        {
            indexList.Clear();
            CountElement?.Invoke(groupKey);
        }

        /// <summary>
        /// Subscribed to when selectable UI calls back, adds the index of it to a list.
        /// Our selectable count is just that list's total count.
        /// </summary>
        /// <param name="index"></param>
        private void CountSelectable(int index)
        {
            if (!indexList.Contains(index))
            {
                indexList.Add(index);
            }
            else
            {
                Debug.LogError("From selectable UI : " + gameObject.name + " there are multiple selectables with index: " + index);
            }
        }
     
        /// <summary>
        /// Navigates indexes
        /// </summary>
        private void NavigateIndex()
        {
            
        }

        /// <summary>
        /// Handles if exiting UI is pressed 
        /// </summary>
        private void HandleDisable()
        {
            
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
        }
    }
}
