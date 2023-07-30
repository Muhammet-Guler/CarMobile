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
    public float geriSayimSure = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartTime = Time.time;
        StartCoroutine(GeriSayimiBaslat());
    }

    // Soru ekranýmýz için süre baþlatýyoruz 10 saniye dolduðunda önceki ekrana geri dönüyoruz
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
            TimerText.text = "Süre Bitti!";
            Time.timeScale = 0f;
            Finnish();
            SceneManager.LoadScene(1);
            Questions.Bos = Questions.Bos + 1;
            PlayerPrefs.SetInt("Bos", Questions.Bos);
        }
    }
    public void Finnish()
    {
        finnished = true;
        TimerText.color = Color.red;
    }
}
