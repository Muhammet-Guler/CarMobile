using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recor : MonoBehaviour
{
    // Start is called before the first frame update
    public int minutes;
    public int seconds;
    public UnityEngine.UI.Text Rekor;
    void Start()
    {
        Rekor.text = string.Format("{0:00}:{1:00}", PlayerPrefs.GetInt("HighMinutes").ToString() , PlayerPrefs.GetInt("HighSeconds").ToString());
    }

    // Update is called once per frame
    void Update()
    {
       minutes = PlayerPrefs.GetInt("minutes");
       seconds = PlayerPrefs.GetInt("seconds");
        if (minutes < PlayerPrefs.GetInt("HighMinutes"))
        {
            PlayerPrefs.SetInt("HighMinutes", minutes);
            PlayerPrefs.SetInt("HighSeconds", seconds);
            Rekor.text = string.Format("{0:00}:{1:00}", PlayerPrefs.GetInt("HighMinutes").ToString(), PlayerPrefs.GetInt("HighSeconds").ToString());
            
        }
    }
}
