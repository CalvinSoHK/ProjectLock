using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using UnityEngine.Events;
using System.Threading.Tasks;
using Core.World;

namespace World.Event
{
    /// <summary>
    /// Triggers scene load on entering this trigger collider
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(SceneLoadEvent))]
    public class TriggerSceneLoad : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_EDITOR
            if (!GetComponent<Collider>().isTrigger)
            {
                Debug.LogError(gameObject.name + " is supposed to have collider isTrigger set to true.");
            }
#endif
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<SceneLoadEvent>().LoadScene();
            }
        }
    }
}
