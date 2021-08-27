using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput
{
    /// <summary>
    /// Player input map
    /// </summary>
    public class PlayerInputMap : MonoBehaviour
    {
        /// <summary>
        /// List is visible and editable in inspector.
        /// List is only used at runtime to populate the dict.
        /// </summary>
        [SerializeField]
        public List<InputMapping> inputMappings = new List<InputMapping>();

        /// <summary>
        /// Dictionary which lets one grab the input mappings out.
        /// Grab values using the helper function.
        /// </summary>
        private Dictionary<InputEnums.InputName, InputMapping> inputDict = new Dictionary<InputEnums.InputName, InputMapping>();

        private void Start()
        {
            PreloadData();
        }

        /// <summary>
        /// Preloads dictionary based on list inputs
        /// </summary>
        private void PreloadData()
        {
            foreach(InputMapping mapping in inputMappings)
            {
                inputDict.Add(mapping.inputName, mapping);
            }
        }

        /// <summary>
        /// Grabs the mapping from the dictionary
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        private InputMapping GetMapping(InputEnums.InputName inputName)
        {
            InputMapping value;
            if(inputDict.TryGetValue(inputName, out value))
            {
                return value;
            }
            throw new System.Exception(inputName + " is missing an input mapping in player input mamp.");
        }

        /// <summary>
        /// Grabs bool for given input and action.
        /// Ex: GetInput(Interact, Down) is Get interact key down
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="inputAction"></param>
        /// <returns></returns>
        public bool GetInput(InputEnums.InputName inputName, InputEnums.InputAction inputAction)
        {
            //Can assume it isn't null since it will throw an exception for us if it is.
            InputMapping mapping = GetMapping(inputName);

            switch (inputAction)
            {
                case InputEnums.InputAction.Any:
                    return (Input.GetKey(mapping.mainKey) || Input.GetKey(mapping.altKey));
                case InputEnums.InputAction.Down:
                    return (Input.GetKeyDown(mapping.mainKey) || Input.GetKey(mapping.altKey));
                case InputEnums.InputAction.Up:
                    return (Input.GetKeyUp(mapping.mainKey) || Input.GetKeyUp(mapping.altKey));
                default:
                    throw new System.Exception("InputAction: " + inputAction + " not implemented.");
            }
        }
    }
}

