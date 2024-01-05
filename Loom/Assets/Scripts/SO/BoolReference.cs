using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="BoolReference", menuName = "SO/BoolReference")]
public class BoolReference : ScriptableObject
{
    public bool value;
    [Header("Bool Settings")]
    public bool SetToFalseWhenDisable = true;

    private void OnDisable()
    {
        if (SetToFalseWhenDisable)
        {
            value = false;
        }
    }
}
