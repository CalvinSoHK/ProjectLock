using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Page
{
    /// <summary>
    /// Stacks up pages as they are used, allowing us to go back through them
    /// </summary>
    public class UIPageStack
    {
        public static string key = "PageInfo";

        private Stack<UIPageInfo> pageInfoStack = new Stack<UIPageInfo>();

        public void AddPageInfo(BaseControllerUI controller, Model model)
        {
            pageInfoStack.Push(new UIPageInfo(controller, model));
        }

        public void PopLastPageInfo()
        {
            UIPageInfo info = pageInfoStack.Pop();
            Core.CoreManager.Instance.messageQueueManager.TryQueueMessage(
                "UI", 
                key, 
                JsonUtility.ToJson(new UIPageStackMessage(info))
                );
        }
    }
}
