using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Page
{
    /// <summary>
    /// Stores info for a page of UI controllers and models being active
    /// </summary>
    public class UIPageInfo
    {
        public List<UIControllerInfo> activeControllerInfoList = new List<UIControllerInfo>();
        
        public UIPageInfo(List<UIControllerInfo> _activeControllerInfoList)
        {
            foreach(UIControllerInfo info in _activeControllerInfoList)
            {
                activeControllerInfoList.Add(info);
            }
        }
    }

    /// <summary>
    /// Contains controller and model for that controller
    /// </summary>
    public class UIControllerInfo
    {
        private BaseControllerUI activeController;
        public BaseControllerUI ActiveController
        {
            get
            {
                return activeController;
            }
        }

        private string savedModelStateJSON;
        public string SavedModelStateJSON
        {
            get
            {
                return savedModelStateJSON;
            }
        }

        /// <summary>
        /// Stores the active controller and converts the passed model into a json
        /// </summary>
        /// <param name="_activeController"></param>
        /// <param name="_savedModelState"></param>
        public UIControllerInfo(BaseControllerUI _activeController, Model _savedModelState)
        {
            activeController = _activeController;
            savedModelStateJSON = JsonUtility.ToJson(_savedModelState);          
        }
    }
}
