using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using UnityEngine.Events;

namespace World
{
    /// <summary>
    /// Triggers scene load on entering this trigger collider
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerSceneLoad : MonoBehaviour
    {
        /// <summary>
        /// Scene name of target scene to load
        /// </summary>
        public string targetSceneName;

        /// <summary>
        /// Target teleport point's key. -1 is invalid.
        /// </summary>
        [Tooltip("The key ID of the teleport point we want to arrive at if this scene load moves us to a new scene.")]
        public int targetTeleportPointKey = -1;

        /// <summary>
        /// Load scene mode. Additive or single.
        /// </summary>
        [Tooltip("Additive scene load expands the map, single means we are moving into a separate map.")]
        public LoadSceneMode loadMode;

        /// <summary>
        /// Events to hook into
        /// </summary>
        public UnityEvent OnLoadSceneStart, OnLoadSceneFinish;

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
                LoadScene();
            }
        }

        /// <summary>
        /// Loads scene
        /// </summary>
        private async void LoadScene()
        {
            WorldManager.OnSceneStartLoad += FireOnLoadSceneStart;
            bool result = await CoreManager.Instance.worldManager.LoadScene(targetSceneName, loadMode);
            if (!result)
            {
                throw new System.Exception("Unable to load scene: " + gameObject.name);
            }
            else
            {
                if(loadMode == LoadSceneMode.Single)
                {
                    CoreManager.Instance.TeleportToPoint(targetSceneName, targetTeleportPointKey);
                }
            }
        }

        /// <summary>
        /// Fires on load scene start, unsubscribes self.
        /// Subscribes the finish event.
        /// </summary>
        private void FireOnLoadSceneStart()
        {
            OnLoadSceneStart?.Invoke();
            WorldManager.OnSceneStartLoad -= FireOnLoadSceneStart;
            WorldManager.OnSceneLoaded += FireOnLoadSceneFinish;
        }

        /// <summary>
        /// Fires on load scene finish, unsubscribes self.
        /// </summary>
        private void FireOnLoadSceneFinish()
        {
            OnLoadSceneFinish?.Invoke();
            WorldManager.OnSceneLoaded -= FireOnLoadSceneFinish;
        }
    }
}
