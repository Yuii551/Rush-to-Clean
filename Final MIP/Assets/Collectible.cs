using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 1;
    public GameObject collectiblePrefab; // the collectible prefab to be spawned
    public int numberOfCollectibles = 10; // the number of collectibles to spawn
    public float spawnRange = 10f; // the maximum distance from the center of the spawn area
    public float levelHeightMin = 0f; // the minimum height of the level
    public float levelHeightMax = 10f; // the maximum height of the level
    public float spawnHeight = 1f; // the height above the floors where collectibles spawn

    public LayerMask floorLayerMask; // The layer mask representing the floors

    private void Start()
    {
        // Define the floorLayerMask using the LayerMask.GetMask function
        floorLayerMask = LayerMask.GetMask("whatIsGround");
        SpawnCollectibles();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.ChangeScore(scoreValue);
            Destroy(gameObject);
        }
    }

    private void SpawnCollectibles()
    {
        for (int i = 0; i < numberOfCollectibles; i++)
        {
            // Generate a random position within the specified horizontal range
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0f, Random.Range(-spawnRange, spawnRange));

            // Raycast downwards from the spawn position to find the floor
            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * levelHeightMax, Vector3.down, out hit, Mathf.Infinity, floorLayerMask))
            {
                // Get the position of the hit point on the floor
                Vector3 floorPosition = hit.point;

                // Set the Y position of the spawn position to be directly above the floor
                spawnPosition.y = floorPosition.y + spawnHeight;

                // Instantiate a new collectible prefab at the random position
                GameObject newCollectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
                newCollectible.layer = LayerMask.NameToLayer("Collectibles"); // set the layer to "Collectibles"
            }
        }
    }
}
