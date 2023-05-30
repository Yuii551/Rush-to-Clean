using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        AddPoints,
        DeductPoints
    }

    public CollectibleType collectibleType;
    public int pointsValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.UpdateScore(pointsValue);
            Destroy(gameObject);
        }
    }
}
