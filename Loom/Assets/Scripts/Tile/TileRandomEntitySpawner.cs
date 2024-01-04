using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class TileRandomEntitySpawner : MonoBehaviour
{
    public EntityList[] listOfEntites;
    int randomEntityType;
    int randomEntityId;
    ScriptableObject entityChosen;

    /// <summary>
    /// Material properties
    /// </summary>
    public int materialAmount;
    public string materialName;

    private void Awake()
    {
        RandomizeEntity();
    }

     void RandomizeEntity()
    {
        randomEntityType = UnityEngine.Random.Range(0, listOfEntites.Length);
        randomEntityId = UnityEngine.Random.Range(0, listOfEntites[randomEntityType].entities.Length);
        entityChosen = listOfEntites[randomEntityType].entities[randomEntityId];

        Debug();
    }

    private void Debug()
    {
        Material material = entityChosen as Material;
        materialName = material.materialName;
        materialAmount = material.materialAmount;
    }

    public (MaterialType materialType, int materialAmount) GetChosenEntity()
    {
        Material material = entityChosen as Material;

        if (material != null)
        {
            return (material.materialType, material.materialAmount);
        }
        else
        {
            return (MaterialType.None, 0);
        }
    }
}
