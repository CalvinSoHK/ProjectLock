using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    /// <summary>
    /// Allows us to trigger a heal on player party
    /// </summary>
    public class HealTrigger : MonoBehaviour
    {
        /// <summary>
        /// Heals player party
        /// </summary>
        public void HealPlayerParty()
        {
            PartyManager party = Core.CoreManager.Instance.playerParty;

            for(int i = 0; i < party.PartySize; i++)
            {
                if (party.IsValidIndex(i))
                {
                    MonIndObj member = party.GetPartyMember(i);
                    member.battleObj.monStats.hp = member.stats.hp;
                }
            }
        }
    }
}
