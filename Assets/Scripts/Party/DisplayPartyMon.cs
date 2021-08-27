using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;

namespace UI
{
    public class DisplayPartyMon : MonoBehaviour
    {
        [SerializeField]
        private List<PartyMonUI> playerMonUI;

        private void OnEnable()
        {
            SetUpPartyMonUI();
        }

        /// <summary>
        /// Sets Up the whole Party Mon UI
        /// </summary>
        void SetUpPartyMonUI()
        {
            for (int i = 0; i < Core.CoreManager.Instance.playerParty.party.PartySize; i++)
            {
                if (Core.CoreManager.Instance.playerParty.party.GetPartyMember(i) != null)
                {
                    SetUpIndMonUI(playerMonUI[i], Core.CoreManager.Instance.playerParty.party.GetPartyMember(i));
                    playerMonUI[i].gameObject.SetActive(true);
                }
                else
                {
                    playerMonUI[i].gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Sets Up the UI for individual Mons
        /// </summary>
        /// <param name="playerMon"></param>
        /// <param name="monster"></param>
        void SetUpIndMonUI(PartyMonUI playerMon, MonIndObj monster)
        {
            playerMon.monName.text = monster.baseMon.name;
            playerMon.monHealth.text = $"{monster.battleObj.monStats.hp} / {monster.stats.hp}";
            playerMon.monLevel.text = monster.stats.level.ToString();
        }
    }
}