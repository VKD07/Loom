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
    public ScriptableObject entityChosen;

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

    /// <summary>
    /// For debugging purpose on Inspector
    /// </summary>
    private void Debug()
    {
        Material material = entityChosen as Material;
        if (material != null)
        {
            materialName = material.materialName;
            materialAmount = material.materialAmount;
        }
    }

    public EntityType GetChosenEntity()
    {
        Material material = entityChosen as Material;
        Enemy enemy = entityChosen as Enemy;
        PowerUps powerUps = entityChosen as PowerUps;

        if(material != null)
        {
            print("Material Chosen");
            return EntityType.Material;
        }else if(enemy != null)
        {
            print("Enemy Chosen");

            return EntityType.Enemy;
        }else if(powerUps != null)
        {
            print("Power Up Chosen");

            return EntityType.PowerUp;
        }
        return EntityType.None;
    }

    /// <summary>
    /// Tupple Function; Returns two types either Material type and material amount
    /// </summary>
    public (MaterialType materialType, int materialAmount) GetMaterial()
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
