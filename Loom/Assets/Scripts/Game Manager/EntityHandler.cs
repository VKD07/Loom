using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityHandler : MonoBehaviour
{
    public static EntityHandler instance;

    public GameObject[] battleGrounds;
    public string playerSpawnPointName = "PlayerSpawnPoint";
    public string enemySpawnPointName = "EnemySpawnPoint";
    public int battleGroundIndex = 0;

    TurnBaseManager turnBaseManager;
    public TileRandomEntitySpawner tileEntitySpawner;
    [HideInInspector] public MonsterDataManager currentEnemy;
    [HideInInspector] public Vector3 initPlayerPos;
    private void Start()
    {
        Singleton();
        turnBaseManager = GetComponent<TurnBaseManager>();
        DisableAllBattleGrounds();
    }

    void Singleton()
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
                currentEnemy = tileEntitySpawner.GetSpawnedMonster().GetComponent<MonsterDataManager>();
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
        battleGroundIndex = Random.Range(0, battleGrounds.Length - 1);

        GameObject currentPlayerPlaying = turnBaseManager.currentPlayerPlaying.playerObj;
        initPlayerPos = currentPlayerPlaying.transform.position;
        battleGrounds[0].SetActive(true);

        //Transfer Player to the battle ground
        currentPlayerPlaying.transform.position = battleGrounds[battleGroundIndex].transform.Find(playerSpawnPointName).transform.position;
        currentPlayerPlaying.transform.forward = -Vector3.forward;

        //Transfer Monster to the battleGround
        currentEnemy.transform.position = battleGrounds[battleGroundIndex].transform.Find(enemySpawnPointName).transform.position;
    }

    public Transform GetBattleGround()
    {
        return battleGrounds[battleGroundIndex].transform;
    }
    
    public void DisableAllBattleGrounds()
    {
        foreach(GameObject battleGround in battleGrounds)
        {
            battleGround.SetActive(false);
        }
    }
}
