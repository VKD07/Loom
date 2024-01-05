using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TurnBaseManager))]
[RequireComponent(typeof(SelectTile))]
[RequireComponent(typeof(EntityHandler))]
public class ActionUIManager : MonoBehaviour
{
    public static ActionUIManager instance;

    [SerializeField] GameObject actionUIPanel;
    [SerializeField] TextMeshProUGUI currentPlayerPlayingTxt;
    [SerializeField] Button moveBtn, searchBtn, attackBtn, craftBtn, powerUpBtn, endTurnBtn;

    TurnBaseManager turnBaseManager;
    SelectTile selectTile;
    EntityHandler entityHandler;
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
        searchBtn.onClick.AddListener(SearchButton);
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
        SetActiveSearchButton(false);
        SetActivateMoveButton(true);
        SetActionPanelActive(false);
        SetActiveEndTurnButton(true);

        //Get the entity from tile
        TileGenerator currentTile = turnBaseManager.currentPlayerPlaying.playerObj.GetComponent<PathFinding>().currentTileStanding;
        TileRandomEntitySpawner tileRandomEntitySpawner = currentTile.GetComponent<TileRandomEntitySpawner>();

        //Set the entity to the entity handler script
        entityHandler.HandleEntity(tileRandomEntitySpawner.GetChosenEntity(), tileRandomEntitySpawner);
    }

    public void SetActiveSearchButton(bool value)
    {
        searchBtn.gameObject.SetActive(value);
    }
    #endregion

    #region End Turn Button

    void EndTurnButton()
    {
        turnBaseManager.NextPlayer();
        SetActiveEndTurnButton(false);
        SetActionPanelActive(true);
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
}
