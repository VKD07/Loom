using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDataManager))]
public class PathFinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask tileLayer;
    [SerializeField] float rayCastLength = 10f;
    public List<Transform> path;
    /*[HideInInspector]*/ public TileGenerator currentTileStanding;
    int currentIndex = 0;
    bool hasMoved;
    RaycastHit hit;

    PlayerDataManager playerDataManager;
    void Start()
    {
        playerDataManager = GetComponent<PlayerDataManager>();
    }

    void Update()
    {
        if (playerDataManager.playerData.playersTurn) //Player can move if it is turn to play
        {
            MoveThroughPathPoints();
        }
    }
    public void FindPath(Transform targetPos)
    {
        CheckTileOnGround();

        path.Clear();

        //if target pos has TileGenerator script
        if (!targetPos.TryGetComponent(out TileGenerator targetTile))
        {
            Debug.LogError("Target position doesn't have TileGenerator component.");
            return;
        }

        //initial current tile is tile chosing in the beginning of the game
        TileGenerator currentTile = currentTileStanding;

        int maxIterations = 1000;
        int iterations = 0;

        while (currentTile != null && iterations < maxIterations)
        {
            if (currentTile == targetTile) //if current tile script is equal to the target tile
            {
                return;
            }

            TileGenerator nextTile = null;
            float lowestDistance = float.MaxValue; //initial lowest distance always at max val

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

            path.Add(nextTile.transform.Find("IslandVisual").transform); //creating a path if lowest distance is found
            currentTile = nextTile; //setting the current tile to the lowest distance tile
            iterations++;
        }

        if (iterations >= maxIterations)
        {
            Debug.LogError("Max iterations reached. Pathfinding aborted.");
        }
    }

    private void CheckTileOnGround()
    {
        Ray ray = new Ray(rayOrigin.position, -rayOrigin.transform.up);
        if (Physics.Raycast(ray, out hit, rayCastLength, tileLayer))
        {
            currentTileStanding = hit.transform.GetComponent<TileGenerator>();
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

            hasMoved = true;
            ActionUIManager.instance?.SetActionPanelActive(false);
        }
        else
        {
            ///Enabling Search Button and Disabling Move Button after Player has moved
            if (hasMoved)
            {
                hasMoved = false;
                ActionUIManager.instance?.SetActivateMoveButton(false);
                ActionUIManager.instance?.SetActiveSearchButton(true);
                ActionUIManager.instance?.SetActionPanelActive(true);

                //to store new tile that player is current standing on (I do this for the search btn)
                CheckTileOnGround();
            }

            currentIndex = 0;
            path.Clear();
        }
    }

    void RotateTowardsDirection(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(rayOrigin.position, -rayOrigin.transform.up * rayCastLength);
    //}
}
