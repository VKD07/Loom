using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;

    PathFinding pathFinding;

    private void Awake()
    {
        pathFinding = GetComponent<PathFinding>();
        playerData.playerPathFinding = pathFinding;
    }
}
