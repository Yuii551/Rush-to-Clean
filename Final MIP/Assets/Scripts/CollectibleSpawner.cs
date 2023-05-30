using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public static CollectibleSpawner Instance { get; private set; }

    public GameObject addPointsCollectiblePrefab;
    public GameObject deductPointsCollectiblePrefab;
    public int initialCollectibleCount = 5;
    public int additionalCollectibleCount = 3;
    public int collectiblesBeforeAdditional = 3;
    public LayerMask floorLayer;

    private int collectibleCount;
    private int collectedCount = 0;
    private float minDistance = 0.5f; // Minimum distance between collectibles

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SpawnInitialCollectibles();
    }

    private void SpawnInitialCollectibles()
    {
        for (int i = 0; i < initialCollectibleCount; i++)
        {
            SpawnCollectible();
        }
    }

    public void SpawnAdditionalCollectibles()
    {
        for (int i = 0; i < additionalCollectibleCount; i++)
        {
            SpawnCollectible();
        }
    }

    private void SpawnCollectible()
    {
        GameObject collectiblePrefab = GetRandomCollectiblePrefab();
        Vector3 spawnPosition = GetRandomPositionAboveFloors();

        // Check distance to existing collectibles
        bool isValidSpawn = false;
        int maxAttempts = 10;
        int attempts = 0;

        while (!isValidSpawn && attempts < maxAttempts)
        {
            isValidSpawn = true;

            // Check distance to existing collectibles
            GameObject[] existingCollectibles = GameObject.FindGameObjectsWithTag("Collectible");
            foreach (GameObject existingCollectible in existingCollectibles)
            {
                float distance = Vector3.Distance(existingCollectible.transform.position, spawnPosition);
                if (distance < minDistance)
                {
                    isValidSpawn = false;
                    break;
                }
            }

            // Increment attempts count
            attempts++;

            // Find a new spawn position if not a valid spawn
            if (!isValidSpawn)
            {
                spawnPosition = GetRandomPositionAboveFloors();
            }
        }

        // Spawn the collectible if a valid spawn position is found
        if (isValidSpawn)
        {
            GameObject newCollectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
            collectibleCount++;
        }
        else
        {
            Debug.LogWarning("Failed to find a valid spawn position for the collectible.");
        }
    }

    private GameObject GetRandomCollectiblePrefab()
    {
        float randomValue = Random.value;
        if (randomValue <= 0.5f)
        {
            return addPointsCollectiblePrefab;
        }
        else
        {
            return deductPointsCollectiblePrefab;
        }
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
        Renderer floorRenderer = randomFloor.GetComponent<Renderer>();
        Vector3 floorPosition = floorRenderer.bounds.center;
        Vector3 floorExtents = floorRenderer.bounds.extents;

        float minX = floorPosition.x - floorExtents.x;
        float maxX = floorPosition.x + floorExtents.x;
        float minZ = floorPosition.z - floorExtents.z;
        float maxZ = floorPosition.z + floorExtents.z;

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), floorRenderer.bounds.max.y + 1f, Random.Range(minZ, maxZ));
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

    public void CollectibleDestroyed()
    {
        collectibleCount--;

        if (collectibleCount <= 0)
        {
            SpawnAdditionalCollectibles();
        }

        collectedCount++;

        if (collectedCount >= collectiblesBeforeAdditional)
        {
            SpawnAdditionalCollectibles();
            collectedCount = 0;
        }
    }


    public void OnCollectibleCollected()
    {
        collectedCount++;
    }
}
