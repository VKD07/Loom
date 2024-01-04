using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask tileLayer;
    [SerializeField] float rayCastLength = 10f;
    RaycastHit hit;
    public TileGenerator initTile;
    public List<Transform> path;
    int currentIndex = 0;
    void Start()
    {

    }

    void Update()
    {
        MoveThroughPathPoints();
    }

    private void CheckTileOnGround()
    {
        Ray ray = new Ray(rayOrigin.position, -rayOrigin.transform.up);
        if (Physics.Raycast(ray, out hit, rayCastLength, tileLayer))
        {
            initTile = hit.transform.GetComponent<TileGenerator>();
        }
    }

    public void FindPath(Transform targetPos)
    {
        CheckTileOnGround();

        path.Clear();

        if (!targetPos.TryGetComponent(out TileGenerator targetTile))
        {
            Debug.LogError("Target position doesn't have TileGenerator component.");
            return;
        }

        TileGenerator currentTile = initTile;

        int maxIterations = 1000;
        int iterations = 0;

        while (currentTile != null && iterations < maxIterations)
        {
            if (currentTile == targetTile)
            {
                Debug.Log("PathFound");
                return;
            }

            TileGenerator nextTile = null;
            float lowestDistance = float.MaxValue;

            // Find the neighbor with the lowest distance to the target
            foreach (TileGenerator neighbor in currentTile.neighbours)
            {
                float distanceToTarget = neighbor.GetDistanceToTarget(targetPos);
                if (distanceToTarget < lowestDistance)
                {
                    lowestDistance = distanceToTarget;
                    nextTile = neighbor;
                }
            }

            if (nextTile == null)
            {
                Debug.LogError("No valid neighbor found.");
                break;
            }

            path.Add(nextTile.transform.Find("IslandVisual").transform);
            currentTile = nextTile;
            iterations++;
        }

        if (iterations >= maxIterations)
        {
            Debug.LogError("Max iterations reached. Pathfinding aborted.");
        }
    }

    void MoveThroughPathPoints()
    {
        if (path.Count <= 0) { return; }
        if (currentIndex < path.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentIndex].position, moveSpeed * Time.deltaTime);
            RotateTowardsDirection(path[currentIndex].position);
            if (Vector3.Distance(transform.position, path[currentIndex].position) <= 0f && currentIndex < path.Count)
            {
                currentIndex++;
            }
        }
        else
        {
            currentIndex = 0;
            path.Clear();
        }
    }

    void RotateTowardsDirection(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion rotation =  Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, -rayOrigin.transform.up * rayCastLength);
    }
}
