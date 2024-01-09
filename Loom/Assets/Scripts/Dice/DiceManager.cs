using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] Dice[] dices;
    [SerializeField] float battleDisableDelay = 5f;
    public int blueDiceNum;
    public int redDiceNum;
    Vector3[] dicesInitPos;
    void Start()
    {
        dicesInitPos = new Vector3[dices.Length];
        SetInitPosFromDice();
        SetActiveDices(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetDicesNumber();
    }

    public void RollDices()
    {
        SetActiveDices(true);
        foreach (Dice dice in dices)
        {
            dice.RollDice();
        }
        StartCoroutine(DisableDiceDelay());
    }

    private void GetDicesNumber()
    {
        foreach (Dice dice in dices)
        {
            if (!dice.gameObject.activeSelf) { return; }
            //Get red dice number
            if (dice.redDice)
            {
                redDiceNum = dice.numberRolled;
            }
            //Get blue dice number
            if (!dice.redDice)
            {
                blueDiceNum = dice.numberRolled;
            }
        }
    }

    #region Enabling and Disable Dice
    IEnumerator DisableDiceDelay()
    {
        yield return new WaitForSeconds(battleDisableDelay);
        SetActiveDices(false);
        SetDiceToInitPos();
    }

    public void SetActiveDices(bool value)
    {
        foreach (Dice dice in dices)
        {
            dice.gameObject.SetActive(value);
        }
    }
    #endregion

    #region Dice Positions
    void SetInitPosFromDice()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dicesInitPos[i] = dices[i].transform.position;
        }
    }

    void SetDiceToInitPos()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].transform.position = dicesInitPos[i];
        }
    }
    #endregion
}
