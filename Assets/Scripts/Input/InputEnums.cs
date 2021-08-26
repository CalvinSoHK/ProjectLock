using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput
{
    public class InputEnums
    {
        /// <summary>
        /// Describes the action we want to read for a given input.
        /// </summary>
        public enum InputAction
        {
            Any,
            Down,
            Up
        }

        /// <summary>
        /// Names for inputs that are mappable in the settings
        /// </summary>
        public enum InputName
        {
            Left,
            Right,
            Up,
            Down,
            Interact,
            Confirm,
            Deny
        }
    }
}
