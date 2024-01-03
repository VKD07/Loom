using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutlineSelection : MonoBehaviour
{
    [SerializeField] Renderer[] tileRenderes;
    [SerializeField] float outlineSize = 1.11f;
    float maxOutlineTime = .1f;
    float currentOutlineTime;
    private void Update()
    {
        OutlineTimer();
    }
    void OutlineTimer()
    {
        if (currentOutlineTime < maxOutlineTime)
        {
            currentOutlineTime += Time.deltaTime;
        }
        else
        {
            DisableOutline();
        }
    }

    public void EnableOutline()
    {
        foreach (Renderer renderer in tileRenderes)
        {
            Material tileMat = renderer.materials[0];
            tileMat.SetFloat("_Scale", outlineSize);
        }
        currentOutlineTime = 0;
    }

    public void DisableOutline()
    {
        foreach (Renderer renderer in tileRenderes)
        {
            Material tileMat = renderer.materials[0];
            tileMat.SetFloat("_Scale", 0);
        }
    }
}
