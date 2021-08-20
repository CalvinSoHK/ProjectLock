using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace World
{
    /// <summary>
    /// Marks a given object as interactable.
    /// Can be extended for specific events.
    /// </summary>
    public class InteractableObject : MonoBehaviour
    {
        public UnityEvent OnInteract;

        public UnityEvent OnInteractComplete;

        public void Interact()
        {
            OnInteract?.Invoke();
        }

        public void InteractComplete()
        {
            OnInteractComplete?.Invoke();
        }
    }
}
