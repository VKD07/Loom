using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerJoinedList", menuName = "SO/PlayerJoinedList")]
public class PlayerJoinedList : ScriptableObject
{
    public PlayerData[] playerData;

    private void OnDisable()
    {
        //playerData = null;
    }
}
