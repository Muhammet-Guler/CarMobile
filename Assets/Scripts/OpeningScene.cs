using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpeningScene : MonoBehaviour
{
    public GameManager managerGame;
    public int deger;
    public int ses;
    public UnityEngine.UI.Text SoundInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //acilis ekran�m�z modlara g�re ge�i� yap�yor ve sorular scriptinde kolay zor sorular i�in degere 1 ve 0 degerlerinin atamas�n� yap�yorum.
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
    public void SesAc()
    {
        ses = 1;
        PlayerPrefs.SetInt("ses", ses);
        SoundInfo.text = "Ses A��k";
    }
    public void SesKapa()
    {
        ses = 0;
        PlayerPrefs.SetInt("ses", ses);
        SoundInfo.text = "Ses Kapal�";
    }
}
