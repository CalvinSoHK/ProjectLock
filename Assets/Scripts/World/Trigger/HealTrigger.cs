using Mon.MonData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Trigger
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
            Party party = Core.CoreManager.Instance.playerParty.party;

            for(int i = 0; i < party.PartySize; i++)
            {
                if (party.IsValidIndex(i))
                {
                    MonIndObj member = party.GetPartyMember(i);
                    if (member != null)
                    {
                        member.FullReset();
                    }
                    else
                    {
                        throw new System.Exception("HealTrigger Error: Given player party index was valid but returned null: " + i);
                    }
                }
            }
        }
    }
}
