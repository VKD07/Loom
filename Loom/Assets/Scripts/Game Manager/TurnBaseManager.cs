using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    public static TurnBaseManager instance;

    [SerializeField] PlayerJoinedList playerJoinedList;
    int currentPlayerIndex;
    public PlayerData currentPlayerPlaying;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentPlayerPlaying = playerJoinedList.playerData[0];
        currentPlayerPlaying.playersTurn = true;
    }

    public void NextPlayer()
    {
        if (currentPlayerIndex < playerJoinedList.playerData.Length -1)
        {
            currentPlayerIndex++;
        }
        else
        {
            currentPlayerIndex = 0;
        }
        EnableSpecificPlayerToPlay();
    }

    void EnableSpecificPlayerToPlay()
    {
        foreach (PlayerData player in playerJoinedList.playerData)
        {
            player.playersTurn = false;
        }

        currentPlayerPlaying = playerJoinedList.playerData[currentPlayerIndex];
        currentPlayerPlaying.playersTurn = true;
    }

    public PathFinding GetCurrentPlayerPathFindingScript()
    {
        return currentPlayerPlaying.playerPathFinding;
    }

    public void SetPlayerDataMaterial(MaterialType materialType, int amount)
    {
        switch (materialType)
        {
            case MaterialType.Wood:
                playerJoinedList.playerData[currentPlayerIndex].wood += amount;
                break;
            case MaterialType.Iron:
                playerJoinedList.playerData[currentPlayerIndex].iron += amount;
                break;
            case MaterialType.Diamond:
                playerJoinedList.playerData[currentPlayerIndex].Diamond += amount;
                break;
        }
    }
}
