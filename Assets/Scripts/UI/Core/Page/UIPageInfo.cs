using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Page
{
    /// <summary>
    /// Stores info for a particular UI controller and it's respective model so it can be restored if we back to that UI page
    /// </summary>
    public class UIPageInfo
    {
        private BaseControllerUI controller;
        public BaseControllerUI Controller
        {
            get
            {
                return controller;
            }
        }

        private Model savedModelState;
        public Model SavedModelState
        {
            get
            {
                return savedModelState;
            }
        }
    
        public UIPageInfo(BaseControllerUI _controller, Model _savedModelState)
        {
            controller = _controller;
            savedModelState = _savedModelState;
        }
    }
}
