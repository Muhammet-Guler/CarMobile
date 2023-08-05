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
        ilkBaslangic();
    }
    public void Zor()
    {
        SceneManager.LoadScene(1);
        deger = 1;
        PlayerPrefs.SetInt("deger", deger);
        ilkBaslangic();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ilkBaslangic()
    {
        car.carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = car.carPosition;
        car.PuzzlePosition = new Vector3((float)-66.985, (float)1.507, (float)-268.3);
        PlayerPrefs.SetFloat("KüpZPosition", car.PuzzlePosition.z);
        car.moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", car.moveSpeed);

    }
}
