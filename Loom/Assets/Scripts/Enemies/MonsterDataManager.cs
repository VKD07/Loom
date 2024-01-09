using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : MonoBehaviour
{
    [SerializeField] Enemy monsterData;
    public float health;
    public float totalDamage;

    private void Awake()
    {
        InitMonsterData();
    }

    private void InitMonsterData()
    {
        health = monsterData.health;
        totalDamage = monsterData.damage;
    }

    public void ReduceHealth(float damage)
    {
        health -= damage;
    }
}
