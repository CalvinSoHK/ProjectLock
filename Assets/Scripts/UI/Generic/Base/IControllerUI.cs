using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IControllerUI
    {
        public abstract void SetupController(string _key);
        public abstract void HandleState();
    }
}
