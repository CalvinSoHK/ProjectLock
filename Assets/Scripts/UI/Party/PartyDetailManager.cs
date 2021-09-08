using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PartyDetailManager : MonoBehaviour
    {

        [SerializeField]
        private string targetGroupKey, targetKey;

        private void OnEnable()
        {
            //DropdownUI.DropdownOptionFire += EnableUI;
        }

        private void OnDisable()
        {
            //DropdownUI.DropdownOptionFire -= EnableUI;
        }



        private void EnableUI(string _groupKey, string _key)
        {
            if (_groupKey.Equals(targetGroupKey) && _key.Equals(targetKey))
            {
                //Invoke Delegate
                Debug.Log("Enable Detail Screen");
            }
        }
    }

}
