using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer2 : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;
    public float StartTime;
    public bool finnished = false;
    public Car car;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (finnished)
        {

            return;
        }
        float t = Time.time - StartTime;
        string Minutes = ((int)t / 60).ToString();
        string Seconds = (t % 60).ToString("f2");
        TimerText.text = Minutes + ":" + Seconds;
        if (TimerText.text=="0:10,00")
        {
            Time.timeScale = 0f;
            Finnish();
            SceneManager.LoadScene(1);
        }
    }
    public void Finnish()
    {
        finnished = true;
        TimerText.color = Color.red;
    }
}
