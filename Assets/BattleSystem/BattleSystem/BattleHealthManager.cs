using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHealthManager : MonoBehaviour
{

    public GameObject health;
    
    public void SetHP(float monHealth)
    {
        health.transform.localScale = new Vector3(monHealth, 1f);
    }
}
