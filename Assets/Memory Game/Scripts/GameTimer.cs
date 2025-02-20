using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 60f; // Set the timer duration (e.g., 60 seconds)
    private float timeRemaining;
    public TextMeshProUGUI timerText; 

    private bool isRunning = true;

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerUI();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (timeRemaining > 0 && isRunning)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            UpdateTimerUI();
        }

        if (timeRemaining <= 0)
        {
            TimerEnd();
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = "Time: " + timeRemaining.ToString("0");
    }

    private void TimerEnd()
    {
        isRunning = false;
        Debug.Log("Time's Up! Game Over.");
    }
}