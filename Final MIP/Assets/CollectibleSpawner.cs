using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int initialSpawnCount = 5;
    public int additionalSpawnCount = 3;

    private int collectedCount = 0;

    private void Start()
    {
        // Spawn initial collectibles
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnCollectible();
        }
    }

    public void OnCollectibleCollected()
    {
        collectedCount++;

        // Spawn additional collectibles after collecting a certain number of collectibles
        if (collectedCount % additionalSpawnCount == 0)
        {
            for (int i = 0; i < additionalSpawnCount; i++)
            {
                SpawnCollectible();
            }
        }
    }

    private void SpawnCollectible()
    {
        GameObject newCollectible = Instantiate(collectiblePrefab, GetRandomPositionAboveFloors(), Quaternion.identity);
        newCollectible.GetComponent<Collectible>().collectibleSpawner = this;
    }

    private Vector3 GetRandomPositionAboveFloors()
    {
        GameObject[] floorObjects = GameObject.FindGameObjectsWithTag("Floor");
        if (floorObjects.Length == 0)
        {
            Debug.LogError("No floor objects found with the 'Floor' tag.");
            return Vector3.zero;
        }

        GameObject randomFloor = floorObjects[Random.Range(0, floorObjects.Length)];
        Bounds floorBounds = GetWorldBounds(randomFloor);

        float minX = floorBounds.min.x;
        float maxX = floorBounds.max.x;
        float minZ = floorBounds.min.z;
        float maxZ = floorBounds.max.z;

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), floorBounds.max.y + 1f, Random.Range(minZ, maxZ));
        return randomPosition;
    }

    private Bounds GetWorldBounds(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds;
        }
        else
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                return collider.bounds;
            }
            else
            {
                Debug.LogWarning("No renderer or collider found on object: " + obj.name);
                return new Bounds(obj.transform.position, Vector3.zero);
            }
        }
    }
}
