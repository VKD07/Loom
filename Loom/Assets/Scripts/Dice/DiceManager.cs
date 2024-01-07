using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] Dice[] dices;
    [SerializeField] int blueDiceNum;
    [SerializeField] int redDiceNum;
    void Start()
    {
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
    }

    private void GetDicesNumber()
    {
        foreach (Dice dice in dices)
        {
            if (!dice.gameObject.activeSelf) { return; }
            print("Dices has rolled");
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

    public void SetActiveDices(bool value)
    {
        foreach (Dice dice in dices)
        {
            dice.gameObject.SetActive(value);
        }
    }
}
