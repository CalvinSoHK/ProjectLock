using Core.MessageQueue;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Base;
using UnityEngine;

namespace UI.Page
{
    /// <summary>
    /// Manages the UIPageStack
    /// </summary>
    public class UIPagesManager
    {
        private UIPageStack pageStack = new UIPageStack();

        public static string PAGESTACK_OUT_KEY = "Pagestack/Out";

        public static string PAGESTACK_IN_KEY = "Pagestack/In";

        public static string PAGESTACK_BACK_KEY = "Pagestack/Back";

        /// <summary>
        /// List of controller info that will be used to make a new page info
        /// </summary>
        private List<UIControllerInfo> activeControllerList = new List<UIControllerInfo>();

        /// <summary>
        /// List of controllers that don't need to be 
        /// </summary>
        private List<BaseControllerUI> ignoreList = new List<BaseControllerUI>();

        public delegate void OnSaveComplete(List<BaseControllerUI> _ignoreList);
        public static OnSaveComplete OnSaveCompleteEvent;

        public void Update()
        {
            //If we get the return key
            if(Core.CoreManager.Instance.inputMap.GetInput(CustomInput.InputEnums.InputName.Return, CustomInput.InputEnums.InputAction.Down))
            {
                ReturnKeyProcess();
                //If we are not return locked and we have pages to go to, pop the last page.
                if (pageStack.StackCount > 0)
                {
                    PopLastPage();
                }
            }
        }

        /// <summary>
        /// Adds given UIPageInfo object into stack
        /// </summary>
        /// <param name="pageInfo"></param>
        private void AddPage(UIPageInfo pageInfo)
        {
            pageStack.AddPageInfo(pageInfo);
        }

        /// <summary>
        /// Adds a new controller and it's model to be saved
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        public void AddController(BaseControllerUI controller, Model model)
        {
            activeControllerList.Add(new UIControllerInfo(controller, model));
        }

        /// <summary>
        /// Removes a given controller from ActiveControllers
        /// If in ignoreList it will also be removed from there
        /// </summary>
        /// <param name="controller"></param>
        public void RemoveController(BaseControllerUI controller)
        {
            //Whether we can remove this from active controllers
            bool removable = false;

            //Index of target to remove from active controllers if we find it
            int index = -1;
            for(index = 0; index < activeControllerList.Count; index++)
            {
                if (activeControllerList[index].ActiveController.key.Equals(controller.key))
                {
                    //Remove from ignore list as well if it contains the controller
                    if (ignoreList.Contains(controller))
                    {
                        ignoreList.Remove(controller);
                    }

                    //Set to true
                    removable = true;
                    break;
                }
            }

            //If removable remove at that index and return
            if (removable)
            {
                activeControllerList.RemoveAt(index);
                return;
            }

            //Otherwise throw a warning about failing to remove it
            Debug.LogWarning("UIPagesManager Error : Attempted to remove controller with key: " + controller.key + " but it wasn't active in the first place. Please make sure the controller is active before attempting to save.");
        }

        /// <summary>
        /// Adds a controller to be ignored by the normal reset that happens when we save a page.
        /// All other controllers are disabled
        /// </summary>
        /// <param name="controller"></param>
        public void AddIgnoreController(BaseControllerUI controller)
        {
            foreach (UIControllerInfo info in activeControllerList)
            {
                if (info.ActiveController.key.Equals(controller.key))
                {
                    ignoreList.Add(controller);
                    return;
                }
            }

            Debug.LogWarning("UIPagesManager Error : Attempted to save controller with key: " + controller.key + " but it wasn't active in the first place. Please make sure the controller is active before attempting to save.");
        }

        /// <summary>
        /// Removes a given BaseControllerUI from the list of ignorable controllers
        /// </summary>
        /// <param name="controller"></param>
        public void RemoveIgnoreController(BaseControllerUI controller)
        {
            //Check to see if we can remove that controller.
            //Loop through with index, if it is removable we can use that index.
            bool removable = false;
            int index = -1;
            for(index = 0; index < ignoreList.Count; index++)
            {
                if (ignoreList[index].key.Equals(controller.key))
                {
                    removable = true;
                    break;
                }
            }

            //If removable remove at that index and return
            if (removable)
            {
                ignoreList.RemoveAt(index);
                return;
            }

            //Otherwise throw a warning about failing to remove it
            Debug.LogWarning("UIPagesManager Error : Attempted to remove controller with key: " + controller.key + " but it wasn't in the ignore list in the first place. Please make sure the controller is active before attempting to save.");
        }

        /// <summary>
        /// Purges our activeControllerList of all controllers,
        /// except for those that are in the ignore list.
        /// </summary>
        /// <param name="_ignoreList"></param>
        private void PurgeActiveController(List<BaseControllerUI> _ignoreList)
        {
            List<UIControllerInfo> newActiveControllerList = new List<UIControllerInfo>(); 
            foreach(UIControllerInfo info in activeControllerList)
            {
                string checkKey = info.ActiveController.key;
                //Check to see if we should ignore this controller.
                bool ignore = false;
                foreach(BaseControllerUI controller in _ignoreList)
                {
                    if (controller.key.Equals(checkKey))
                    {
                        ignore = true;
                    }
                }

                if (ignore)
                {
                    newActiveControllerList.Add(info);
                }
            }
            activeControllerList.Clear();
            activeControllerList = newActiveControllerList;
        }

        /// <summary>
        /// Pops the last page in the stack and broadcoasts the page info.
        /// If the controller matches the BaseControllerUI will set its model to the given one and either refresh or display.
        /// </summary>
        public void PopLastPage()
        {
            UIPageInfo info = pageStack.PopLastPageInfo();
            activeControllerList = info.activeControllerInfoList;
            foreach(UIControllerInfo controllerInfo in activeControllerList)
            {
                controllerInfo.ActiveController.SetModelAndEnable(controllerInfo.SavedModelStateJSON);
            }
        }

        /// <summary>
        /// Sends out a message to all controllers letting them the return key was pressed.
        /// Only processed on the BaseControllerUI side if they are already on.
        /// </summary>
        public void ReturnKeyProcess()
        {
            UIPagesManagerOutMessage msg = new UIPagesManagerOutMessage();
            msg.returnKeyPressed = true;
            Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
               MessageQueueManager.UI_KEY,
               PAGESTACK_OUT_KEY,
               JsonUtility.ToJson(msg)
               );
        }

        /// <summary>
        /// Sends out a message to all controllers requesting save data if needed
        /// </summary>
        public void SavePage()
        {
            //If it is still zero throw an error, we shouldn't be saving
            if(activeControllerList.Count == 0)
            {
                Debug.LogWarning("UIPagesManager Exception: Attempting to save a page but no controllers were found valid.");
            }
            else
            {
                //Save new page
                AddPage(new UIPageInfo(activeControllerList));

                //Call OnSaveCompleteEvent
                OnSaveCompleteEvent?.Invoke(ignoreList);

                //Purge list of active controllers
                PurgeActiveController(ignoreList);
            }
        }

    }
}
