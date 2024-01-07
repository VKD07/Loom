using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TurnBaseManager))]
[RequireComponent(typeof(SelectTile))]
[RequireComponent(typeof(EntityHandler))]
public class ActionUIManager : MonoBehaviour
{
    public static ActionUIManager instance;

    [SerializeField] GameObject actionUIPanel;
    [SerializeField] TextMeshProUGUI currentPlayerPlayingTxt;
    [SerializeField] Button moveBtn, searchBtn, attackBtn, battleMonsterBtn, fleeBtn, craftBtn, powerUpBtn, endTurnBtn;
    [SerializeField] Button rollDiceBtn;
    [SerializeField] UnityEvent OnMonsterBattle;

    //Game Managers
    TurnBaseManager turnBaseManager;
    SelectTile selectTile;
    EntityHandler entityHandler;
    DiceManager diceManager;

    TileGenerator currentTile;
    TileRandomEntitySpawner tileRandomEntitySpawner;
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
        endTurnBtn.gameObject.SetActive(false);

        diceManager = GetComponent<DiceManager>();
        turnBaseManager = GetComponent<TurnBaseManager>();
        selectTile = GetComponent<SelectTile>();
        entityHandler = GetComponent<EntityHandler>();

        selectTile.enabled = false;
        ButtonListeners();
    }

    private void Update()
    {
        UpdateTxt();
    }

    private void UpdateTxt()
    {
        currentPlayerPlayingTxt.SetText(turnBaseManager.currentPlayerPlaying.playerObj.name);
    }

    void ButtonListeners()
    {
        moveBtn.onClick.AddListener(MoveButton);
        attackBtn.onClick.AddListener(Attack);
        battleMonsterBtn.onClick.AddListener(BattleMonster);
        fleeBtn.onClick.AddListener(Flee);
        craftBtn.onClick.AddListener(Craft);
        powerUpBtn.onClick.AddListener(PowerUp);
        searchBtn.onClick.AddListener(SearchButton);

        rollDiceBtn.onClick.AddListener(RollDice);

        endTurnBtn.onClick.AddListener(EndTurnButton);
    }

    #region Move Button
    void MoveButton()
    {
        SetActionPanelActive(false);
        selectTile.enabled = true;
    }

    public void SetActivateMoveButton(bool value)
    {
        moveBtn.gameObject.SetActive(value);
    }
    #endregion

    #region Search Button
    void SearchButton()
    {
        //Get the current tile that the current player is currently standing on
        currentTile = turnBaseManager.currentPlayerPlaying.playerObj.GetComponent<PathFinding>().currentTileStanding;
        tileRandomEntitySpawner = currentTile.GetComponent<TileRandomEntitySpawner>();

        //If Player has found a monster on the tile
        if (tileRandomEntitySpawner.GetChosenEntity() == EntityType.Enemy)
        {
            ShowMonsterActionBtns();
            tileRandomEntitySpawner.SpawnEnemyOnTile();
        }
        else
        {
            SetActiveSearchButton(false);
            SetActivateMoveButton(true);
            SetActionPanelActive(false);
            SetActiveEndTurnButton(true);

            //Set the entity to the entity handler script
            entityHandler.HandleEntity(tileRandomEntitySpawner.GetChosenEntity(), tileRandomEntitySpawner);
        }
    }

    public void ShowMonsterActionBtns()
    {
        SetActiveSearchButton(false);
        SetActivateMoveButton(false);
        SetActiveAttackButton(false);
        SetActiveCraftButton(false);
        SetActivePowerUpButtton(false);

        SetActiveFleeButton(true);
        SetActiveBattleMonsterButton(true);
    }

    public void SetActiveSearchButton(bool value)
    {
        searchBtn.gameObject.SetActive(value);
    }

    #endregion

    #region Attack Button
    void Attack()
    {

    }
    public void SetActiveAttackButton(bool value)
    {
        attackBtn.gameObject.SetActive(value);
    }
    #endregion

    #region Battle Monster Button
    void BattleMonster()
    {
        OnMonsterBattle.Invoke();
        SetActiveRollDiceButton(true);
        SetActionPanelActive(false);
        //Set the entity to the entity handler script
        entityHandler.HandleEntity(tileRandomEntitySpawner.GetChosenEntity(), tileRandomEntitySpawner);
    }
    public void SetActiveBattleMonsterButton(bool value)
    {
        battleMonsterBtn.gameObject.SetActive(value);
    }
    #endregion

    #region Flee Button
    void Flee()
    {
        //lose -health to player
        SetActionPanelActive(false);

        SetActiveEndTurnButton(true);
    }
    public void SetActiveFleeButton(bool value)
    {
        fleeBtn.gameObject.SetActive(value);
    }
    #endregion

    #region Craft Button
    void Craft()
    {

    }
    public void SetActiveCraftButton(bool value)
    {
        craftBtn.gameObject.SetActive(value);
    }
    #endregion

    #region Power Up Button
    void PowerUp()
    {

    }

    public void SetActivePowerUpButtton(bool value)
    {
        powerUpBtn.gameObject.SetActive(value);
    }
    #endregion

    #region Roll Dice Button
    void SetActiveRollDiceButton(bool value)
    {
        rollDiceBtn.gameObject.SetActive(value);
    }

    void RollDice()
    {
        diceManager.RollDices();
        SetActiveRollDiceButton(false);
    }
    #endregion

    #region End Turn Button
    void EndTurnButton()
    {
        turnBaseManager.NextPlayer();
        SetActiveEndTurnButton(false);
        ShowDefaultButtons();
    }

    public void SetActiveEndTurnButton(bool value)
    {
        endTurnBtn.gameObject.SetActive(value);
    }
    #endregion

    public void SetActionPanelActive(bool value)
    {
        actionUIPanel.SetActive(value);
    }

    public void PlayerHasMoved()
    {
        SetActiveEndTurnButton(false);
        SetActionPanelActive(true);
    }

    public void ShowDefaultButtons()
    {
        SetActionPanelActive(true);

        SetActivateMoveButton(true);
        SetActiveAttackButton(true);
        SetActiveCraftButton(true);
        SetActivePowerUpButtton(true);

        SetActiveEndTurnButton(false);
        SetActiveBattleMonsterButton(false);
        SetActiveFleeButton(false);
    }
}
