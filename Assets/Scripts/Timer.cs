using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText; /*{ get; private set; }*/
    public float startTime;
    public bool timerRunning;

    public string timerString;
    public GameManager gameManager;
    public int minutes;
    public int seconds;
    public int highScoreMinutes=99;
    public int highScoreSeconds=99;
    public UnityEngine.UI.Text highScoreText;
    public float elapsedTime;
    public Car car;

    public void Start()
    {
        Time.timeScale = 1f;
        timerRunning = false;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        StartTimer();
        InvokeRepeating("ChangeScene", 5f, 5f);
        highScoreMinutes = PlayerPrefs.GetInt("HighScoreMinutes");
        highScoreSeconds = PlayerPrefs.GetInt("HighScoreSeconds");
        highScoreText.text = FormatTime(highScoreMinutes, highScoreSeconds);
        if (car.carPosition.z>3200)
        {
            CancelInvoke("ChangeScene");
        }
    }

    public void InitializeTimer(UnityEngine.UI.Text textComponent)
    {
        TimerText = textComponent;
    }

    public void StartTimer()
    {
        if (gameManager != null)
        {
            // Devam eden bir sayacýmýz varsa, geçmiþ süreyle baþlat
            if (gameManager.isTimerRunning)
            {
                elapsedTime = Time.time - gameManager.startTime;

                minutes = (int)(elapsedTime / 60);
                seconds = (int)(elapsedTime % 60);

                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

                TimerText.text = timerString;
            }
            else
            {
                // Yeni bir sayacý baþlat
                startTime = Time.time;
                gameManager.startTime = startTime;
            }

            gameManager.isTimerRunning = true;
        }
    }
    public void StopTimer()
    {
        if (gameManager != null)
        {
            gameManager.isTimerRunning = false;
        }
        CancelInvoke("QuestionsScene");
    }

    private void Update()
    {
        //CheckHighScore();
        if (gameManager != null && gameManager.isTimerRunning)
        {
            elapsedTime = Time.time - gameManager.startTime;

            minutes = (int)(elapsedTime / 60);
            PlayerPrefs.SetInt("minutes", minutes);
            seconds = (int)(elapsedTime % 60);
            PlayerPrefs.SetInt("seconds", seconds);

            timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            TimerText.text = timerString;

        }
    }
    public string GetTimerString()
    {
        return timerString;
    }
    private void ChangeScene()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(2);
    }
    public void CheckHighScore()
    {
        
        
        if (highScoreMinutes==0)
        {
            highScoreMinutes = 10;
        }
        if (highScoreSeconds==0)
        {
            highScoreSeconds = 99;
        }
        if (minutes < highScoreMinutes || (minutes == highScoreMinutes && seconds < highScoreSeconds))
        {
            highScoreMinutes = minutes;
            highScoreSeconds = seconds;

            PlayerPrefs.SetInt("HighScoreMinutes", highScoreMinutes);
            PlayerPrefs.SetInt("HighScoreSeconds", highScoreSeconds);
            PlayerPrefs.Save();

            highScoreText.text = FormatTime(highScoreMinutes, highScoreSeconds);
        }
    }
    public string FormatTime(int minutes, int seconds)
    {
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    //private void LoadHighScore()
    //{
    //    if (PlayerPrefs.HasKey("HighScoreMinutes"))
    //    {
    //        highScoreMinutes = PlayerPrefs.GetInt("HighScoreMinutes");
    //    }
    //    if (PlayerPrefs.HasKey("HighScoreSeconds"))
    //    {
    //        highScoreSeconds = PlayerPrefs.GetInt("HighScoreSeconds");
    //    }
    //}
    //public void ResetHighScore()
    //{
    //    highScoreMinutes = 10;
    //    highScoreSeconds = 99;

    //    PlayerPrefs.DeleteKey("HighScoreMinutes");
    //    PlayerPrefs.DeleteKey("HighScoreSeconds");
    //    PlayerPrefs.Save();

    //    highScoreText.text = FormatTime(highScoreMinutes, highScoreSeconds);
    //}
    public void ResetTimer()
    {
        elapsedTime = Time.time - gameManager.startTime;

        minutes = (int)(elapsedTime / 60);
        PlayerPrefs.SetInt("minutes", minutes);
        seconds = (int)(elapsedTime % 60);
        PlayerPrefs.SetInt("seconds", seconds);

        timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        TimerText.text = timerString;

    }
}
