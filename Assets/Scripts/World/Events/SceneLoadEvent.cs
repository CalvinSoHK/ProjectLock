using Core;
using Core.World;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace World.Event
{
    public class SceneLoadEvent : BaseEvent
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
        /// Loads scene, teleports you to the target point after scene is loaded before fade in
        /// </summary>
        public async void LoadScene()
        {
            OnBeforeEventFire?.Invoke();
            WorldManager.OnSceneStartLoad += FireOnLoadSceneStart;
            if (loadMode == LoadSceneMode.Single)
            {
                WorldManager.OnSceneLoadedBeforeFadeIn += Teleport;
            }

            bool result = await CoreManager.Instance.worldManager.LoadScene(targetSceneName, loadMode);
            WorldManager.OnSceneLoadedBeforeFadeIn -= Teleport;
            if (!result)
            {
                throw new System.Exception("Unable to load scene: " + gameObject.name);
            }
        }

        /// <summary>
        /// Calls CoreManager to teleport to the target point.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private Task Teleport(string sceneName)
        {
            if (!sceneName.Equals(targetSceneName))
            {
                throw new System.Exception(
                    "Teleport is not teleporting to the same target scene that was loaded. SceneName: "
                    + sceneName +
                    " Target Scene Name: "
                    + targetSceneName);
            }
            CoreManager.Instance.TeleportToPoint(targetSceneName, targetTeleportPointKey);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Fires on load scene start, unsubscribes self.
        /// Subscribes the finish event.
        /// </summary>
        private Task FireOnLoadSceneStart(string sceneName)
        {
            OnEventFire?.Invoke();
            WorldManager.OnSceneStartLoad -= FireOnLoadSceneStart;
            WorldManager.OnSceneLoadedAfterFadeIn += FireOnLoadSceneFinish;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Fires on load scene finish, unsubscribes self.
        /// </summary>
        private Task FireOnLoadSceneFinish(string sceneName)
        {
            OnAfterEventFire?.Invoke();
            WorldManager.OnSceneLoadedAfterFadeIn -= FireOnLoadSceneFinish;
            return Task.CompletedTask;
        }
    }
}
