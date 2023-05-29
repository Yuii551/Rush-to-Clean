using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public Slider scoreSlider;
    public int scoreTarget = 100;

    private int currentScore = 0;
    private bool missionCompleted = false;

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
        if (!missionCompleted)
        {
            if (collectibleType == Collectible.CollectibleType.AddPoints)
            {
                currentScore += pointsValue;
            }
            else if (collectibleType == Collectible.CollectibleType.DeductPoints)
            {
                currentScore -= pointsValue;
            }

            scoreSlider.value = currentScore;
            CheckScoreTarget();
        }
    }

    private void CheckScoreTarget()
    {
        if (currentScore >= scoreTarget)
        {
            missionCompleted = true;
            // Do something when the score target is reached
        }
    }
}
