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
        /// <summary>
        /// Array that represents this entity's party
        /// </summary>
        [SerializeField]
        private MonIndObj[] party = new MonIndObj[6];

        /// <summary>
        /// Max party size in the game.
        /// NOTE: May need to move this variable somewhere else.
        /// This is only getting from magic number set in this script.
        /// </summary>
        public int PartySize { get { return party.Length;} }

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
            LoadMons();
        }

        /// <summary>
        /// Loads mons into party by ID
        /// NOTE: Cannot load mons from a different GEN as of now.
        /// </summary>
        public void LoadMons()
        {
            for(int i = 0; i < party.Length; i++)
            {
                if (IsValidIndex(i))
                {
                    party[i] = new MonIndObj(
                        Core.CoreManager.Instance.dexManager.GetMonByID(party[i].baseMon.ID), party[i].stats.level);
                }
            }
        }

        /// <summary>
        /// Tells us if a given index is valid, as in a mon is in the slot
        /// Does this by checking if slot is null and if the ID is set to 0 (which is never valid)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsValidIndex(int index)
        {
            if(party[index] != null && party[index].baseMon.ID != 0 && party[index].baseMon.generationID != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tells us if a given index is valid for combat.
        /// Uses normal IsValidIndex and also checks health values
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsValidCombatantIndex(int index)
        {
            if (IsValidIndex(index) && party[index].battleObj.monStats.hp > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns individual mon based on index
        /// Null if not a valid index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MonIndObj GetPartyMember(int index)
        {
            if (IsValidIndex(index))
            {
                return party[index];
            }
            return null;
        }

        /// <summary>
        /// Returns the first valid combatant from the party
        /// </summary>
        /// <returns></returns>
        public MonIndObj GetFirstValidCombatant()
        {
            for(int i = 0; i < party.Length; i++)
            {
                if (IsValidCombatantIndex(i))
                {
                    return party[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Sets given index to the inputted MonIndObj
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool SetPartyMember(int index, MonIndObj member)
        {
            if (!IsValidIndex(index))
            {
                party[index] = member;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Swaps two members based on indices.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public bool SwapMembers(int index1, int index2)
        {
            if(IsValidIndex(index1) && IsValidIndex(index2))
            {
                MonIndObj temp = party[index1];
                party[index1] = party[index2];
                party[index2] = temp;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds new member to the party
        /// Returns true if valid, false if not possible.
        /// </summary>
        /// <param name="obj"></param>
        public bool AddMember(MonIndObj obj)
        {
            for(int i = 0; i < party.Length; i++)
            {
                //SetPartyMember attempts to set to index, if not valid it will return false.
                if(SetPartyMember(i, obj))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
