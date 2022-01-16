using System.Collections;
using System.Collections.Generic;
using UI.Message;
using UnityEngine;

namespace UI.Page
{
    public class UIPageStackMessage : MessageObject
    {
        public UIPageInfo pageInfo;

        public UIPageStackMessage(UIPageInfo _pageInfo) : base()
        {
            pageInfo = _pageInfo;
        }
    }
}
