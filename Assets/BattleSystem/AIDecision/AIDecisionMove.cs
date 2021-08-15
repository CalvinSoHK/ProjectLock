using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMove : MonoBehaviour
{
    public BSstatemanager stateManager;

    public void MoveSelection()
    {
        //Gather all 4 movesets

        //Detect which is most effective
        //Does most damage
        //Inflicts a condition
    }


    /// <summary>
    /// Randomly chooses a move in a rage between (0,maxMoves)
    /// </summary>
    /// <param name="maxMoves"></param>
    /// <returns></returns>
    public int RandomMoveSelection(int maxMoves)
    {
        stateManager.aiCurrentMove = Random.Range(0, maxMoves);

        return stateManager.aiCurrentMove;
    }
}
