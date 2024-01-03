using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileDisabler : MonoBehaviour
{
    public GameObjectList tileList;
    public int numOfTilesToDisable = 3;
    int startingTileRange;
    int tileIndex;
    int count;

    void Start()
    {
      
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        DisableRandomTiles();
    //    }
    //}

    public void DisableRandomTiles()
    {
        int numberOfDisabledTiles = GetNumberOfDisabledTiles();

        if (numberOfDisabledTiles < 18)
        {
            startingTileRange = 18;
        }
        else if (numberOfDisabledTiles >= 18 && numberOfDisabledTiles < 30)
        {
            startingTileRange = 6;
        }
        else if (numberOfDisabledTiles >= 30)
        {
            startingTileRange = 0;
        }

        while (count < numOfTilesToDisable)
        {
            tileIndex = Random.Range(startingTileRange, tileList.list.Count);
            while (!tileList.list[tileIndex].activeSelf)
            {
                tileIndex = Random.Range(startingTileRange, tileList.list.Count);
            }
            tileList.list[tileIndex].SetActive(false);
            count++;
        }
        count = 0;
    }

    int GetNumberOfDisabledTiles()
    {
        int numOfDisabledTiles = 0;
        foreach (GameObject tile in tileList.list)
        {
            if (!tile.activeSelf)
            {
                numOfDisabledTiles++;
            }
        }
        return numOfDisabledTiles;
    }
}
