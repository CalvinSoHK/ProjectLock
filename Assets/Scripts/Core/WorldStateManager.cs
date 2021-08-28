using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class WorldStateManager : MonoBehaviour
    {
        private WorldState state = WorldState.Off;

        public WorldState State
        {
            get
            {
                return state;
            }
        }

        public void SetWorldState(WorldState _state)
        {
            state = _state;
        }
    }

    public enum WorldState
    {
        Off,
        Overworld,
        Battle
    }
}
