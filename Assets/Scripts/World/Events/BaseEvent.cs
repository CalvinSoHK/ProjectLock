using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace World.Event
{
    public class BaseEvent : MonoBehaviour
    {
        [SerializeField]
        protected UnityEvent OnBeforeEventFire;

        [SerializeField]
        protected UnityEvent OnEventFire;

        [SerializeField]
        protected UnityEvent OnAfterEventFire;
    }
}
