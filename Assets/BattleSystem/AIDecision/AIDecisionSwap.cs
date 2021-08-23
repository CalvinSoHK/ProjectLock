using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonData;
using Mon.Moves;
using World;

namespace AIdecision
{
    public class AIDecisionSwap : MonoBehaviour
    {
        public BSstatemanager stateManager;


        /// <summary>
        /// Finds best swap based on Average Damage of all skills
        /// </summary>
        /// <param name="party"></param>
        /// <param name="monster"></param>
        public void aiAverageSwap(Party party, MonIndObj monster)
        {
            int swapIndex = 0;
            float highestAverageDamage = 0;
            float currentAverageDamage;
            Debug.Log(party.GetPartyMember(0).Nickname);
            for (int i = 0; i < party.PartySize; i++)
            {
                if (party.GetPartyMember(i) != null && party.GetPartyMember(i).battleObj.monStats.hp > 0)
                {
                    List<MoveDamage> monMoves = party.GetPartyMember(i).moveSet.CalcMovePower(Core.CoreManager.Instance.typeRelationSO.GetSortedWeakness(monster));
                    currentAverageDamage = findAverageDamage(monMoves);
                    if (currentAverageDamage > highestAverageDamage)
                    {
                        highestAverageDamage = currentAverageDamage;
                        swapIndex = i;
                    }
                }
            }

            stateManager.swapManager.SwapToAI(swapIndex);

        }

        /// <summary>
        /// Gets all skills from a mon
        /// </summary>
        /// <param name="monType"></param>
        void aiSkillCheck(List<MoveDamage> monMoves)
        {
            findAverageDamage(monMoves);
        }

        /// <summary>
        /// Finds average (power * multiplier) of all the moves from MoveDamage list
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        float findAverageDamage(List<MoveDamage> move)
        {
            float averageDamage;
            float totalDamage = 0;
            float currentDamage;
            foreach (MoveDamage x in move)
            {
                currentDamage = x.power * x.typeMultiplier;
                totalDamage += currentDamage;
            }

            averageDamage = totalDamage / move.Count;


            return averageDamage;
        }

        /// <summary>
        /// Finds highest (power * multiplier) of all the moves from MoveDamage list
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        float findHighestDamage(List<MoveDamage> move)
        {
            float highestDamage = 0;
            float currentDamage;
            foreach (MoveDamage x in move)
            {
                currentDamage = x.power * x.typeMultiplier;
                if (currentDamage > highestDamage)
                {
                    highestDamage = currentDamage;
                }
            }

            return highestDamage;
        }
    }
}
