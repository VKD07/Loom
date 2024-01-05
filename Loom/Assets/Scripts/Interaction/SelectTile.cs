using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public LayerMask tileLayer;
    RaycastHit hit;
    public GameObject selectedtile;
    public float tileSelectionDistancelimit = 13f;
    public Transform currentPlayer;
    TurnBaseManager turnBaseManager;
    void Start()
    {
        turnBaseManager = GetComponent<TurnBaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePointerRaycast();
    }

    void MousePointerRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, tileLayer))
        {
            selectedtile = hit.transform.gameObject;
            TileGenerator tileGenerator = selectedtile.GetComponent<TileGenerator>();

            if(tileGenerator.GetDistanceToTarget(GetCurrentPlayerTransform()) > tileSelectionDistancelimit) { return; }

            TileSelection tileSelection = selectedtile.GetComponent<TileSelection>();
            tileSelection.EnableOutline();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tileSelection.SelectTile();
                this.enabled = false;
            }
        }
    }

    Transform GetCurrentPlayerTransform()
    {
        Transform currentPlayerTransform = turnBaseManager.currentPlayerPlaying.playerPathFinding.transform;
        currentPlayer = currentPlayerTransform;
        return currentPlayerTransform;
    }
}
