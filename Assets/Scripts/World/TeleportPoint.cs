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
            obj.transform.position = transform.position;
            OnTeleportEvent?.Invoke();
        }
    }
}
