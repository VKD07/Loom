using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;
    public float health;
    public float totalDamage;
    GameObject player;

    private void Awake()
    {
        //Storing player game object to player data
        player = gameObject;
        playerData.playerObj = gameObject;
        health = playerData.playerHealth;
        totalDamage = playerData.playerDamage;
    }
}
