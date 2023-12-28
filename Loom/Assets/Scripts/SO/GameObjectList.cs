using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "SO/GameObjectList")]
public class GameObjectList : ScriptableObject
{
    public List <GameObject> list;
    public bool clearListOnDisable = true;

    private void OnDisable()
    {
        if(clearListOnDisable)
        {
            list.Clear();
        }
    }
}
