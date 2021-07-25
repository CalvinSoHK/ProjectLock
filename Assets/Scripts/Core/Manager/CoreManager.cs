using Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Core manager singleton
    /// </summary>
    public class CoreManager : MonoBehaviour
    {
        private static CoreManager _instance;

        public static CoreManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(_instance);
            }
        }

        [Header("Player Objects")]
        [SerializeField]
        public PlayerController player;

        [SerializeField]
        public CameraManager camera;

        [Header("Managers")]
        [SerializeField]
        public MonDexManager dexManager;

        [SerializeField]
        public EncounterManager encounterManager;
    }
}
