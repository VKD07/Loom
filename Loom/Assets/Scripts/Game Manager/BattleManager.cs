using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [SerializeField] float attackDelay = 5f;
    [SerializeField] float transitionBackDelay = 3f;
    public EntityHandler entityHandler;
    TurnBaseManager turnBaseManager;
    public MonsterDataManager currentMonsterData;
    public GameObject currentPlayerPlaying;
    DiceManager diceManager;
    void Awake()
    {
        Singleton();
        diceManager = GetComponent<DiceManager>();
        entityHandler = GetComponent<EntityHandler>();
        turnBaseManager = GetComponent<TurnBaseManager>();
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

    private void Update()
    {
        HandleBattle();
    }

    private void HandleBattle()
    {
        currentPlayerPlaying = turnBaseManager.currentPlayerPlaying.playerObj;
        currentMonsterData = entityHandler.currentEnemy;
    }

    private void CheckTheHealth()
    {
        if (currentMonsterData == null) { return; }
        if (currentMonsterData.health <= 0)
        {
            BattleEnds();
        }
    }

    public void StartBattle()
    {
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        PlayerAttack player1 = currentPlayerPlaying.GetComponent<PlayerAttack>();
        EnemyAttack enemy = currentMonsterData.GetComponent<EnemyAttack>();
        yield return new WaitForSeconds(attackDelay);
        player1.AddDamage(diceManager.blueDiceNum);
        player1.JumpAttack();

        yield return new WaitForSeconds(attackDelay);

        if (currentMonsterData.health <= 0)
        {
            BattleEnds();
        }
        else
        {
            enemy.AddDamage(diceManager.redDiceNum);
            enemy.JumpAttack();
            yield return new WaitForSeconds(3);
            ActionUIManager.instance.SetActiveRollDiceButton(true);
        }
    }

    void BattleEnds()
    {
        //setting the player transform back to the hex tile map
        StopAllCoroutines();
        currentPlayerPlaying.transform.SetParent(null);
        currentPlayerPlaying.transform.position = entityHandler.initPlayerPos;
        entityHandler.tileEntitySpawner.DestroyEnemy();
        entityHandler.DisableAllBattleGrounds();
        ActionUIManager.instance.SetActiveEndTurnButton(true);
        diceManager.SetActiveDices(false);
        this.enabled = false;
        //Reset the tile entity?
    }
}
