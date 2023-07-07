using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText; 
    private static float carSceneTimer = 0f;
    public string timerString;
    public Car car;
    public GameManager gameManager;
    public int minutes;
    public int seconds;
    public int highScoreMinutes = 99;
    public int highScoreSeconds = 99;
    public UnityEngine.UI.Text highScoreText;
    public GameObject PausePanel;

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager.isFinished == false)
        {
            if (scene.buildIndex == 1 &&Time.timeScale==1f)
            {
                carSceneTimer += Time.unscaledDeltaTime;
                //var ts = TimeSpan.FromSeconds(carSceneTimer);
                minutes = Mathf.FloorToInt(carSceneTimer / 60F);
                seconds = Mathf.FloorToInt(carSceneTimer - minutes * 60);
                TimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                highScoreMinutes = PlayerPrefs.GetInt("HighScoreMinutes");
                highScoreSeconds = PlayerPrefs.GetInt("HighScoreSeconds");
                highScoreText.text = string.Format("{0:0}:{1:00}", highScoreMinutes, highScoreSeconds);
                if (seconds % 5 == 0 && seconds != 0)
                {
                    carSceneTimer += 1f;
                    SceneManager.LoadScene(2);
                }
            }
            if (car.sayac==1)
            {
                carSceneTimer = 0f;
            }
        }
    }

    public void CheckHighScore()
    {

        if (highScoreMinutes == 0)
        {
            highScoreMinutes = 10;
        }
        if (highScoreSeconds == 0)
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
            highScoreText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}