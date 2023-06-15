using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public Slider scoreSlider;
    public int scoreTarget = 100;

    private int currentScore = 0;
    private bool isGameWon = false;

    [SerializeField]
    private GameOverScreen gameOverScreen;

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
        scoreSlider.maxValue = scoreTarget;
        scoreSlider.value = currentScore;
    }

    public void UpdateScore(Collectible.CollectibleType collectibleType, int pointsValue)
    {
        if (collectibleType == Collectible.CollectibleType.AddPoints)
        {
            currentScore += pointsValue;
        }
        else if (collectibleType == Collectible.CollectibleType.DeductPoints)
        {
            currentScore -= pointsValue;
            currentScore = Mathf.Max(currentScore, 0); // Ensure the score won't go below zero
        }

        scoreSlider.value = currentScore;
        CheckScoreTarget();
    }

    private void CheckScoreTarget()
    {
        if (currentScore >= scoreTarget)
        {
            isGameWon = true;
            gameOverScreen.GameWon(); // Call the GameWon() method in GameOverScreen script
        }
    }
}
