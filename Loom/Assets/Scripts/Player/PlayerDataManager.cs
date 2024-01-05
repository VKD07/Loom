using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;
    GameObject player;

    private void Awake()
    {
        //Storing player game object to player data
        player = gameObject;
        playerData.playerObj = gameObject;
    }
}
