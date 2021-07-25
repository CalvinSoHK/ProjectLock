using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonster : MonoBehaviour
{

    public MonsterSO monster1;

    [Header("Monster 1")]
    public string mon1name;
    public int mon1health;
    public int mon1att;
    public int mon1spd;
    public int mon1def;
    public int mon1lvl;

    // Start is called before the first frame update
    void Start()
    {
        mon1name = monster1.monsterName;
        mon1health = monster1.health;
        mon1att = monster1.attack;
        mon1spd = monster1.speed;
        mon1def = monster1.defence;
        mon1lvl = monster1.level;
    }

}
