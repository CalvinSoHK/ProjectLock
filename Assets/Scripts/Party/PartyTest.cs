using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using Mon.MonData;

public class PartyTest : MonoBehaviour
{

    MonIndObj[] _playerMon = new MonIndObj[Core.CoreManager.Instance.playerParty.party.PartySize];

    public MonIndObj[] playerMon
    {
        get
        {
            return playerMon;
        }
        set
        {
            playerMon = value;
        }
    }


    /// <summary>
    /// On initialize
    /// </summary>
    void Start()
    {
        for (int i = 0; i < _playerMon.Length; i++)
        {
            MonInfo(i);
        }
    }


    //Sets Cu
    void MonInfo(int monNumber)
    {
        _playerMon[monNumber] = Core.CoreManager.Instance.playerParty.party.GetPartyMember(monNumber);
        if (_playerMon[monNumber] != null)
        {
            Debug.Log(_playerMon[monNumber].baseMon.name);
        }
    }


}
