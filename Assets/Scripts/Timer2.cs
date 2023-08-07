using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer2 : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;
    public float StartTime;
    public bool finnished = false;
    public Questions Questions;
    public float geriSayimSure = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartTime = Time.time;
        StartCoroutine(GeriSayimiBaslat());
    }

    // Soru ekran�m�z i�in s�re ba�lat�yoruz 10 saniye doldu�unda �nceki ekrana geri d�n�yoruz
    void Update()
    {
    }
    System.Collections.IEnumerator GeriSayimiBaslat()
    {
        while (geriSayimSure > 0 && finnished == false)
        {
            TimerText.text = geriSayimSure.ToString();
            yield return new WaitForSeconds(1f);
            geriSayimSure--;
        }


        if (geriSayimSure == 0f)
        {
            TimerText.text = "S�re Bitti!";
            Finnish();
            Questions.Bos = Questions.Bos + 1;
            PlayerPrefs.SetInt("Bos", Questions.Bos);
            PlayerPrefs.SetFloat("geriSayimSure", geriSayimSure);
            SceneManager.LoadScene(1);
        }
    }
    public void Finnish()
    {
        finnished = true;
        TimerText.color = Color.red;
        StopCoroutine(GeriSayimiBaslat());
    }
}
