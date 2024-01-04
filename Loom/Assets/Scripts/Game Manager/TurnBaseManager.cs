using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    public static TurnBaseManager instance;

    [SerializeField] PlayerJoinedList playerJoinedList;
    public int currentPlayerPlaying;

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
    public void SetPlayerDataMaterial(MaterialType materialType, int amount)
    {
        switch (materialType)
        {
            case MaterialType.Wood:
                playerJoinedList.playerData[currentPlayerPlaying].wood += amount;
                break;
            case MaterialType.Iron:
                playerJoinedList.playerData[currentPlayerPlaying].iron += amount;
                break;
            case MaterialType.Diamond:
                playerJoinedList.playerData[currentPlayerPlaying].Diamond += amount;
                break;
        }
    }
}
