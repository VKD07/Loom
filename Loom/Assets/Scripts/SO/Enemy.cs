using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Create New Entity/Enemy")]

public class Enemy : ScriptableObject
{
    public Sprite enemyImage;
    public string EnemyName;
    public float health;
    public float damage;
}
