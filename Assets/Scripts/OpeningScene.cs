using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpeningScene : MonoBehaviour
{
    public GameManager managerGame;
    public int deger;
    public int ses;
    public UnityEngine.UI.Text SoundInfo;
    public Car car;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
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
        PlayerPrefs.SetInt("deger", deger);
        ilkBaslangic();
        car.sayac = 1;
    }
    public void Zor()
    {
        SceneManager.LoadScene(1);
        deger = 1;
        PlayerPrefs.SetInt("deger", deger);
        ilkBaslangic();
        car.sayac = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ilkBaslangic()
    {
        car.carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = car.carPosition;
        car.moveSpeed = 15f;
        PlayerPrefs.SetFloat("ArabaninHizi", car.moveSpeed);

    }
}
