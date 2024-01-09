using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Player_", menuName = "SO/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats/Info")]
    public bool playersTurn;
    public int playerNumber;
    public float playerHealth;
    public float playerDamage = 4f;


    [HideInInspector] public GameObject playerObj;

    [Header("Materials")]
    public int wood;
    public int iron;
    public int Diamond;

    private void OnDisable()
    {
        playersTurn = false;
        wood = 0;
        iron = 0;
        Diamond = 0;
    }
}
