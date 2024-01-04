using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    [SerializeField] Renderer[] tileRenderes;
    [SerializeField] float outlineSize = 1.11f;
    float maxOutlineTime = .05f;
    float currentOutlineTime;

    [Header("On Tile Spawn")]
    public MMFeedbacks onSpawned;
    [Header("On Tile Spawn")]
    public MMFeedbacks onSelection;
    [Header("On Tile Spawn")]
    public MMFeedbacks OnHoverFeedback;
    public MMFeedbacks UnHoverFeedback;
    
    private void OnEnable()
    {
        onSpawned?.PlayFeedbacks();
    }
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

    public void SelectTile()
    {
        OnHoverFeedback.enabled = false;
        onSelection?.PlayFeedbacks();
    }

    public void EnableOutline()
    {
        foreach (Renderer renderer in tileRenderes)
        {
            OnHoverFeedback?.PlayFeedbacks();
            Material tileMat = renderer.materials[0];
            tileMat.SetFloat("_Scale", outlineSize);
        }
        currentOutlineTime = 0;
    }

    public void DisableOutline()
    {
        foreach (Renderer renderer in tileRenderes)
        {
            UnHoverFeedback?.PlayFeedbacks();
            Material tileMat = renderer.materials[0];
            tileMat.SetFloat("_Scale", 0);
        }
        OnHoverFeedback.enabled = true;
    }
}
