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
    public GameObject PausePanel;

    private void Start()
    {
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager.isFinished == false)
        {
            if (scene.buildIndex == 1 && Time.timeScale == 1f)
            {
                carSceneTimer += Time.unscaledDeltaTime;
                minutes = Mathf.FloorToInt(carSceneTimer / 60F);
                seconds = Mathf.FloorToInt(carSceneTimer - minutes * 60);
                TimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
            if (car.sayac == 1)
            {
                carSceneTimer = 0f;
            }
        }
    }
}