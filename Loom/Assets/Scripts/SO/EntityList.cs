using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EntityList_", menuName = "Create New Entity/ListOfEntity")]
public class EntityList : ScriptableObject
{
    public ScriptableObject [] entities;
}
