using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Create New Entity/Enemy")]

public class Enemy : ScriptableObject
{
    public Sprite enemyImage;
    public GameObject enemyPrefab;
    public string EnemyName;
    public float health;
    public float damage;
    //initial Values
    float initHealth;
    private void OnEnable()
    {
        initHealth = health;
    }

    private void OnDisable()
    {
        health = initHealth;
    }
}
