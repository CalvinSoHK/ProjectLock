using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class MonsterSO : ScriptableObject
{
    public string monsterName;

    public int health;
    public int attack;
    public int speed;
    public int defence;
    public int level;

    //Another SO?
    public string moveSet1;
    public string moveSet2;
    public string moveSet3;
    public string moveSet4;
}
