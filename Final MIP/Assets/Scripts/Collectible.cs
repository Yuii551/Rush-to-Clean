using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        AddPoints,
        DeductPoints
    }

    public CollectibleType collectibleType;
    public int pointsValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.UpdateScore(collectibleType, pointsValue);
            CollectibleSpawner.Instance.CollectibleDestroyed();
            Destroy(gameObject);
        }
    }
}
