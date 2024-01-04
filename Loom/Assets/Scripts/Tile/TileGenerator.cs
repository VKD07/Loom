using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] Transform[] rayPoints;
    [SerializeField] float rayDistance = 1.0f;
    [SerializeField] LayerMask tileLayer;
    [SerializeField] FloatReference numberOfTiles, maxTiles;
    [SerializeField] GameObject tile;
    [SerializeField] GameObjectList tileList;
    [SerializeField] Transform islandVisual;
    [SerializeField] FloatReference tileDistance;
    [SerializeField] Mesh[] typesOfMesh;
    [SerializeField] float[] randomRotations;
    [Header("Random Environment Props")]
    [SerializeField] float numberOfObjectsPlaced = 15;
    [SerializeField] List<Transform> objectPlacements;
    [SerializeField] GameObject[] objects;
    GameObject newTile;
    int objectPlaced;

    public List<TileGenerator> neighbours;
    RaycastHit hit;

    private void Awake()
    {
        RandomObjectPlacement(transform);
    }
    void Update()
    {
        InstantiateNewTileFromRayCast();
    }

    void InstantiateNewTileFromRayCast()
    {
        if (numberOfTiles.value < maxTiles.value)
        {
            foreach (Transform rayPoint in rayPoints)
            {
                Ray ray = new Ray(rayPoint.position, rayPoint.forward);
                if (!Physics.Raycast(ray, rayDistance, tileLayer) && numberOfTiles.value < maxTiles.value)
                {
                    newTile = Instantiate(tile, ray.GetPoint(tileDistance.value), Quaternion.identity);
                    RandomMesh(newTile.GetComponentInChildren<MeshFilter>());
                    islandVisual.localRotation = Quaternion.Euler(transform.localEulerAngles.x, GetRandomRotation(), transform.localEulerAngles.z);
                    newTile.GetComponent<TileGenerator>().RandomObjectPlacement(newTile.transform);
                    tileList.list.Add(newTile);
                    numberOfTiles.value++;
                }
            }
        }
        else
        {
            foreach (Transform rayPoint in rayPoints)
            {
                Ray ray = new Ray(rayPoint.position, rayPoint.forward);
                if (Physics.Raycast(ray, out hit, rayDistance, tileLayer))
                {
                    if (!neighbours.Contains(hit.transform.GetComponent<TileGenerator>()))
                    {
                        neighbours.Add(hit.transform.GetComponent<TileGenerator>());
                    }
                }
            }
        }
    }

    IEnumerator Spawn()
    {
        foreach (Transform rayPoint in rayPoints)
        {

            Ray ray = new Ray(rayPoint.position, rayPoint.forward);
            if (!Physics.Raycast(ray, rayDistance, tileLayer) && numberOfTiles.value < maxTiles.value)
            {
                newTile = Instantiate(tile, ray.GetPoint(tileDistance.value), Quaternion.identity);
                RandomMesh(newTile.GetComponentInChildren<MeshFilter>());
                islandVisual.localRotation = Quaternion.Euler(transform.localEulerAngles.x, GetRandomRotation(), transform.localEulerAngles.z);
                newTile.GetComponent<TileGenerator>().RandomObjectPlacement(newTile.transform);
                tileList.list.Add(newTile);
                numberOfTiles.value++;
                yield return new WaitForSeconds(spawnDelay);
            }
        }

    }


    void RandomMesh(MeshFilter objectMesh)
    {
        int randomMesh = Random.Range(0, typesOfMesh.Length);
        objectMesh.mesh = typesOfMesh[randomMesh];
    }

    float GetRandomRotation()
    {
        int randomRot = Random.Range(0, randomRotations.Length);
        return randomRotations[randomRot];
    }

    public void RandomObjectPlacement(Transform parent)
    {
        while (objectPlaced < numberOfObjectsPlaced && objectPlacements.Count > 0)
        {

            int randomPropIndex = Random.Range(0, objects.Length);
            int randomPosIndex = Random.Range(0, objectPlacements.Count);
            float randomYRotation = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0f, randomYRotation, 0f);
            GameObject prop = Instantiate(objects[randomPropIndex], objectPlacements[randomPosIndex].position, randomRotation);
            prop.transform.SetParent(parent);
            // objectPlacements.RemoveAt(randomPosIndex);
            objectPlaced++;
        }
    }

    public float GetDistanceToTarget(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }

    private void OnDrawGizmos()
    {
        if (rayPoints.Length > 0 && numberOfTiles.value < maxTiles.value)
        {
            foreach (Transform rayPoint in rayPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(rayPoint.position, rayPoint.transform.forward * rayDistance);
            }
        }
    }
}
