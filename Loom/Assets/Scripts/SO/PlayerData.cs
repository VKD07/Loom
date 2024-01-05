using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Player_", menuName = "SO/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Info")]
    public bool playersTurn;
    public int playerNumber;
    public float playerHealth;

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
