using System.Collections.Generic;
using UI.Base;

namespace UI.Page
{
    /// <summary>
    /// Stacks up pages as they are used, allowing us to go back through them.
    /// Can add new ones and pop out old ones.
    /// </summary>
    public class UIPageStack
    {
        private Stack<UIPageInfo> pageInfoStack = new Stack<UIPageInfo>();

        /// <summary>
        /// How many pages are currently stored.
        /// </summary>
        public int StackCount
        {
            get
            {
                return pageInfoStack.Count;
            }
        }

        public void AddPageInfo(UIPageInfo pageInfo)
        {
            pageInfoStack.Push(pageInfo);
        }

        public UIPageInfo PopLastPageInfo()
        {
            return pageInfoStack.Pop();
        }
    }
}
