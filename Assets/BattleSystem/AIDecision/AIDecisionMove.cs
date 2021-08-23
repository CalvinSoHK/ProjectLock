using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMove : MonoBehaviour
{
    public BSstatemanager stateManager;

    int highestDamageMove;
    /// <summary>
    /// Ai makes decision on move
    /// If one of the moves is lethal (will kill playerMon), uses that move
    /// else random
    /// </summary>
    public void MoveSelection()
    { 
        if (isLethal())
        {
            stateManager.aiCurrentMove = highestDamageMove;
        }
        else
        {
            RandomMoveSelection(stateManager.aiCurMonster.moveSet.MoveCount);
        }
        //Gather all 4 movesets

        //Detect which is most effective
        //Does most damage
        //Inflicts a condition
    }

        
    bool isLethal()
    {
        //Debug.Log("Highest Damage Move: " + stateManager.damageManager.DamageCalculationAI(stateManager.aiCurMonster.moveSet.GetMove(HighestDamage())));
        if (stateManager.damageManager.DamageCalculationAI(stateManager.aiCurMonster.moveSet.GetMove(HighestDamage())) >= stateManager.healthManager.playerCurHP)
        {
            return true;
        }

        return false;
        //int remainder = Mathf.Abs(currentDamage) - stateManager.healthManager.playerCurHP;
        //save i to aicurrentMove
    }

    /// <summary>
    /// Finds highest damage skill
    /// </summary>
    /// <returns> highestDamageMove </returns>
    int HighestDamage()
    {
        int highestDamage = 0;
        for (int i = 0; i < stateManager.aiCurMonster.moveSet.MoveCount; i++)
        {
            if (stateManager.aiCurMonster.moveSet.GetMove(i) != null)
            {
                int currentDamage = stateManager.damageManager.DamageCalculationAI(stateManager.aiCurMonster.moveSet.GetMove(i));

                if (currentDamage > highestDamage)
                {
                    highestDamage = currentDamage;
                    highestDamageMove = i;
                }
                
            }

            //save i to aicurrentMove
        }
        return highestDamageMove;
    }
        /// <summary>
        /// Randomly chooses a move in a rage between (0,maxMoves)
        /// </summary>
        /// <param name="maxMoves"></param>
        /// <returns></returns>
        void RandomMoveSelection(int maxMoves)
        {
            stateManager.aiCurrentMove = Random.Range(0, maxMoves - 1);
        }

}
