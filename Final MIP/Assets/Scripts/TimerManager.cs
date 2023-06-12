using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    public ScoreManager scoreManager;
    public GameOverScreen gameOverScreen;

    private bool isGameOver;
    private bool isGameWon;

    private void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    }

    private void Update()
    {
        if (!isGameOver)
        {
            currentTime = countDown ? currentTime - Time.deltaTime : currentTime + Time.deltaTime;

            if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
            {
                currentTime = timerLimit;
                timerText.color = Color.red;
                isGameOver = true;
                gameOverScreen.GameOver();
            }

            SetTimerText();
        }
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWon()
    {
        return isGameWon;
    }

    public void SetGameWon()
    {
        isGameWon = true;
        gameOverScreen.GameWon();
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal
}