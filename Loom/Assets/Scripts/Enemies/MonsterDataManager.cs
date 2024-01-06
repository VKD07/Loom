using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : MonoBehaviour
{
    [SerializeField] Enemy monsterData;
    public float health;

    private void Awake()
    {
        InitMonsterData();
    }

    private void InitMonsterData()
    {
        health = monsterData.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float damage)
    {
        health -= damage;
    }
}
