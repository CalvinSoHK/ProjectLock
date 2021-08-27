using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput
{
    [System.Serializable]
    /// <summary>
    /// Mapping for two keys to the same input action
    /// </summary>
    public class InputMapping
    {
        public InputEnums.InputName inputName;
        public KeyCode mainKey = KeyCode.None;
        public KeyCode altKey = KeyCode.None;

        public InputMapping(InputEnums.InputName _inputName, KeyCode _mainKey, KeyCode _altkey)
        {
            inputName = _inputName;
            mainKey = _mainKey;
            altKey = _altkey;
        }
    }
}

