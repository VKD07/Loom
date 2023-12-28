using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "FloatReference", menuName = "SO/FloatReference")]
public class FloatReference : ScriptableObject
{
    public float value;
    public bool resetValueOnDisable = true;

    private void OnDisable()
    {
        if(resetValueOnDisable)
        {
            value = 0f;
        }
    }

}
