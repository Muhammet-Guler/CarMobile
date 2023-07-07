using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpeningScene : MonoBehaviour
{
    public GameManager managerGame;
    public int deger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //acilis ekranýmýz modlara göre geçiþ yapýyor ve sorular scriptinde kolay zor sorular için degere 1 ve 0 degerlerinin atamasýný yapýyorum.
    public void Kolay()
    {
        SceneManager.LoadScene(1);
        deger = 0;
        PlayerPrefs.SetInt("deger",deger);
    }
    public void Zor()
    {
        SceneManager.LoadScene(1);
        deger = 1;
        PlayerPrefs.SetInt("deger", deger);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
