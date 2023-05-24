using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 1;
    public CollectibleSpawner collectibleSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.ChangeScore(scoreValue);
            collectibleSpawner.OnCollectibleCollected();
            Destroy(gameObject);
        }
    }
}
