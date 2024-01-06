using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBattleManager : MonoBehaviour
{
    public EntityHandler entityHandler;
    TurnBaseManager turnBaseManager;
    [SerializeField] public MonsterDataManager currentMonsterData;
    [SerializeField] GameObject currentPlayerPlaying;
    void Awake()
    {
        entityHandler = GetComponent<EntityHandler>();
        turnBaseManager = GetComponent<TurnBaseManager>();
    }

    private void Update()
    {
        HandleBattle();
    }

    private void HandleBattle()
    {
        currentPlayerPlaying = turnBaseManager.currentPlayerPlaying.playerObj;
        currentMonsterData = entityHandler.currentEnemy;

        if (currentMonsterData == null) { return; }
        if (currentMonsterData.health <= 0)
        {
            BattleEnds();
        }
    }

    void BattleEnds()
    {
        //setting the player transform back to the hex tile map
        currentPlayerPlaying.transform.SetParent(null);
        currentPlayerPlaying.transform.position = entityHandler.initPlayerPos;
        entityHandler.tileEntitySpawner.DestroyEnemy();
        entityHandler.DisableAllBattleGrounds();
        ActionUIManager.instance.SetActiveEndTurnButton(true);
        this.enabled = false;
        //Reset the tile?
    }
}
