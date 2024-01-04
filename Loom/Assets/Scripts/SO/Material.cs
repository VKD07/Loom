using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material_", menuName = "Create New Entity/Material")]
public class Material : ScriptableObject
{
    public MaterialType materialType;
    public string materialName;
    public int materialAmount;
}