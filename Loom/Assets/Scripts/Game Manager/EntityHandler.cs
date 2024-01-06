using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityHandler : MonoBehaviour
{
    [SerializeField] GameObject[] battleGrounds;
    [SerializeField] string playerSpawnPointName = "PlayerSpawnPoint";
    [SerializeField] string enemySpawnPointName = "EnemySpawnPoint";
    TurnBaseManager turnBaseManager;
    public TileRandomEntitySpawner tileEntitySpawner;
    [HideInInspector] public Enemy currentEnemy;
    [HideInInspector] public Vector3 initPlayerPos;
    private void Start()
    {
        turnBaseManager = GetComponent<TurnBaseManager>();
        DisableAllBattleGrounds();
    }
    public void HandleEntity(EntityType entityType, TileRandomEntitySpawner tileEntitySpawner)
    {
        this.tileEntitySpawner = tileEntitySpawner;
        switch (entityType)
        {
            case EntityType.Material:
                SetPlayerDataMaterial(tileEntitySpawner.GetMaterial().materialType, tileEntitySpawner.GetMaterial().materialAmount);
                //GetAllMaterialsGathered(tileEntitySpawner);
                break;
            case EntityType.Enemy:
                currentEnemy = tileEntitySpawner.GetEnemy();
                EnableBattleGroundWithEnemy(tileEntitySpawner.GetEnemy());
                break;
        }
    }

    void GetAllMaterialsGathered(TileRandomEntitySpawner tileEntitySpawner)
    {
        EntityList entityMaterials = tileEntitySpawner.GetMaterials();
        foreach(ScriptableObject materials in entityMaterials.entities)
        {
            Material material = materials as Material;
            SetPlayerDataMaterial(material.materialType, material.materialAmount);
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

    void EnableBattleGroundWithEnemy(Enemy enemy)
    {
        GameObject currentPlayerPlaying = turnBaseManager.currentPlayerPlaying.playerObj;
        initPlayerPos = currentPlayerPlaying.transform.position;
        battleGrounds[0].SetActive(true);

        //Transfer Player to the battle ground
        currentPlayerPlaying.transform.position = battleGrounds[0].transform.Find(playerSpawnPointName).transform.position;
        currentPlayerPlaying.transform.forward = -Vector3.forward;

        //Transfer Monster to the battleGround
        tileEntitySpawner.monsterSpawned.transform.position = battleGrounds[0].transform.Find(enemySpawnPointName).transform.position;
    }
    
    public void DisableAllBattleGrounds()
    {
        foreach(GameObject battleGround in battleGrounds)
        {
            battleGround.SetActive(false);
        }
    }
}
