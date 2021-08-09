using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonster : MonoBehaviour
{

    [System.Serializable]
    public class TrainerMonster
    {
        public MonsterSO monsterObject;
        public string monName;
        public int monMaxHealth;
        public int monCurHealth;
        public int monAtt;
        public int monSpd;
        public int monDef;
        public int monLvl;
    }

    public List<TrainerMonster> playerMonster;

    // Start is called before the first frame update
    void Start()
    {
        //SetUpValues();
    }


    /// <summary>
    /// Sets up values(stats) of mon  according to the object
    /// </summary>
    public void SetUpValues()
    {
        for (int i = 0; i < playerMonster.Count; i++)
        {
            playerMonster[i].monName = playerMonster[i].monsterObject.name;
            playerMonster[i].monMaxHealth = playerMonster[i].monsterObject.maxHealth;
            playerMonster[i].monCurHealth = playerMonster[i].monsterObject.curHealth;
            playerMonster[i].monAtt = playerMonster[i].monsterObject.attack;
            playerMonster[i].monSpd = playerMonster[i].monsterObject.speed;
            playerMonster[i].monDef = playerMonster[i].monsterObject.defence;
            playerMonster[i].monLvl = playerMonster[i].monsterObject.level;
        }
    }
    

    /// <summary>
    /// Checks for the first available mon in inventory, if health > 0
    /// </summary>
    /// <returns> a player monster </returns>
    public TrainerMonster FirstAvailable()
    {
        for (int i = 0; i < playerMonster.Count; i++)
        {
            if (playerMonster[i].monCurHealth > 0)
            {
                return playerMonster[i];
            }
        }
            
        return null;
    }
}
