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
        //5 saniyede bir ekran de�i�imini sa�l�yoruz
        //rekor kontrol ediliyor
        //saniye ba�lat�l�yor


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
            // Devam eden bir sayac�m�z varsa, ge�mi� s�reyle ba�lat
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
                // Yeni bir sayac� ba�lat
                startTime = Time.time;
                gameManager.startTime = startTime;
            }

            gameManager.isTimerRunning = true;
        }
    }
    //sayac� durdurma fonksiyonu
    public void StopTimer()
    {
        if (gameManager != null)
        {
            gameManager.isTimerRunning = false;
        }
        CancelInvoke("QuestionsScene");
    }
    //saya� s�rekli g�ncelleniyor
    private void Update()
    {
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
    //timerstringi d�nd�r�yoruz
    public string GetTimerString()
    {
        return timerString;
    }
    //Hangi ekrana ge�i� sa�layaca��m�z fonksiyon
    private void ChangeScene()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(2);
    }

    //burada  saniyemizin rekordan k���k olup olmad���n� kontrol ediyoruz ve yazd�r�yoruz
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
    // sayac�n format fonksiyonu
    public string FormatTime(int minutes, int seconds)
    {
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    //sayac� s�f�rl�yoruz
    public void ResetTimer()
    {
        gameManager.isTimerRunning = false;
        elapsedTime = Time.time - gameManager.startTime;

        minutes = (int)(elapsedTime / 60);
        PlayerPrefs.SetInt("minutes", minutes);
        seconds = (int)(elapsedTime % 60);
        PlayerPrefs.SetInt("seconds", seconds);

        timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        TimerText.text = timerString;

    }
}
