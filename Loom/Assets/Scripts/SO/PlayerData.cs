using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Player_", menuName = "SO/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int playerNumber;
    [Header("Items")]
    int wood;
}
