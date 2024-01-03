using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public LayerMask tileLayer;
    RaycastHit hit;
    public GameObject selectedtile;
    void Start()
    {

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
            selectedtile.GetComponent<TileOutlineSelection>().EnableOutline();
            if (Input.GetMouseButtonDown(0))
            {
            }
        }
    }
}
