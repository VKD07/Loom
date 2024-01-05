using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHandler : MonoBehaviour
{
    TurnBaseManager turnBaseManager;
    TileRandomEntitySpawner tileEntitySpawner;
    private void Start()
    {
        turnBaseManager = GetComponent<TurnBaseManager>();
    }
    public void HandleEntity(EntityType entityType, TileRandomEntitySpawner tileEntitySpawner)
    {
        this.tileEntitySpawner = tileEntitySpawner;
        switch (entityType)
        {
            case EntityType.Material:
                SetPlayerDataMaterial(tileEntitySpawner.GetMaterial().materialType, tileEntitySpawner.GetMaterial().materialAmount);
                break;
        }
    }

    void SetPlayerDataMaterial(MaterialType materialType, int amount)
    {
        print("Material data is set");
        PlayerData playerData = turnBaseManager.currentPlayerPlaying;
        switch (materialType)
        {
            case MaterialType.Wood:
                playerData.wood += amount;
                break;
            case MaterialType.Iron:
                playerData.iron += amount;
                break;
            case MaterialType.Diamond:
                playerData.Diamond += amount;
                break;
        }
    }
}
