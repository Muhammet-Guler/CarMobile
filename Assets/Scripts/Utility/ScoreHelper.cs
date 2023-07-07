using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHelper
{

    public static string GetBestTime()
    {
        int highScoreMinutes = PlayerPrefs.GetInt("HighScoreMinutes");
        int highScoreSeconds = PlayerPrefs.GetInt("HighScoreSeconds");
        return string.Format("{0:00}:{1:00}", highScoreMinutes, highScoreSeconds);
    }

}
