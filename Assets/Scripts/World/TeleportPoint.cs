using Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace World
{
    /// <summary>
    /// Teleport point, contains key id for this point.
    /// </summary>
    public class TeleportPoint : MonoBehaviour
    {
        [SerializeField]
        public int key = -1;

        [Tooltip("Event fires when something is teleported here.")]
        public UnityEvent OnTeleportEvent;

        public void TeleportHere(GameObject obj)
        {
            //Get player feet
            PlayerController controller = obj.GetComponent<PlayerController>();
            Transform playerFeet;
            if (controller)
            {
                playerFeet = controller.PlayerFeet;
            }
            else
            {
                throw new System.Exception("TeleportPoint Error: Attempted to teleport object that does not have PlayerController: " + obj.name);
            }
            obj.transform.position = transform.position + (obj.transform.position - playerFeet.position);
            OnTeleportEvent?.Invoke();
        }
    }
}
