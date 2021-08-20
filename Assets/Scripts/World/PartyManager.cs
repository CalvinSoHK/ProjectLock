using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    /// <summary>
    /// Script that manages an entity's party
    /// </summary>
    [RequireComponent(typeof(EntityInfo))]
    public class PartyManager : MonoBehaviour
    {
        [SerializeField]
        public Party party;

        private void Start()
        {
            StartCoroutine(LoadMonsWhenReady());
        }

        /// <summary>
        /// Coroutine that waits for the dex to complete.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadMonsWhenReady()
        {
            while (!Core.CoreManager.Instance.dexManager.DexReady)
            {
                yield return new WaitForEndOfFrame();
            }
            party.LoadMons();
        }
    }
}
