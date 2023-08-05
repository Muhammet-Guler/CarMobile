using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public float moveSpeed = 10f; // Nesnenin hareket h�z�
    public bool stop = false;
    public SpeedIndicator speedIndicator;
    public Timer timer;
    public Vector3 carPosition;
    public Vector3 PuzzlePosition;
    public GameManager ManagerGame;
    public GameObject RestartAndQuit;
    public Questions Questions;
    public GameObject PausePanel;
    public int sayac = 0;
    public Sound sound;
    public int deger;
    public GameObject Puzzle;
    int ses;
    void Start()
    {
        Time.timeScale = 1f;
        LoadCarPosition();
        sayac = 0;
        float savedZPosition = PlayerPrefs.GetFloat("K�pZPosition", PuzzlePosition.z);
        Vector3 newPosition = PuzzlePosition;
        newPosition.z = savedZPosition;
        PuzzlePosition = newPosition;
    }
    //araban�n konumu ve h�z� s�rekli g�ncellenerek tutuluyor
    //araban�n maksimimum h�z� 360 olarak ayarlan�yor
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        ManagerGame = GameObject.FindObjectOfType<GameManager>();
        if (ManagerGame.isFinished == false)
        {
            if (scene.buildIndex == 1)
            {
                moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                if (moveSpeed > 360)
                {
                    moveSpeed = 360;
                }
                carPosition = transform.position;
            }
        }
    }
    //finishe geldi�imizde restart ekran�m�z geliyor
    //h�z�m�z s�f�rlan�yor ve arka plandaki her �ey duruyor
    //rekor g�ncelleniyor
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "finish")
        {
            Time.timeScale = 0f;
            moveSpeed = 0f;
            RestartAndQuit.SetActive(true);
            Questions.DogruYanlis();
            //timer.StopTimer();
            if (moveSpeed > 60f)
            {
                moveSpeed = 0f;
            }

            ManagerGame.isFinished = true;

            CancelInvoke("QuestionsScene");
            timer.CheckHighScore();
            sound.PauseSound();
        }
        if (other.tag == "puzzle")
        {

            float geriSayimSure = PlayerPrefs.GetFloat("geriSayimSure");
            
            Vector3 newPosition = PuzzlePosition;
            newPosition.z += moveSpeed * (5-(geriSayimSure-1));
            PuzzlePosition = newPosition;

            PlayerPrefs.SetFloat("K�pZPosition", newPosition.z);
            SceneManager.LoadScene(2); ;
        }
    }

    void OnDestroy()
    {
        SaveCarPosition();
    }

    public void SaveCarPosition()
    {
        PlayerPrefs.SetFloat("CarPositionX", carPosition.x);
        PlayerPrefs.SetFloat("CarPositionY", carPosition.y);
        PlayerPrefs.SetFloat("CarPositionZ", carPosition.z);
        PlayerPrefs.Save();
    }

    public void LoadCarPosition()
    {
        float posX = PlayerPrefs.GetFloat("CarPositionX");
        float posY = PlayerPrefs.GetFloat("CarPositionY");
        float posZ = PlayerPrefs.GetFloat("CarPositionZ");
        carPosition = new Vector3(posX, posY, posZ);
        transform.position = carPosition;

    }


    void OnApplicationQuit()
    {
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        Questions.Dogru = 0;
        PlayerPrefs.SetInt("Dogru", Questions.Dogru);
        Questions.Yanlis = 0;
        PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
        Questions.Bos = 0;
        PlayerPrefs.SetInt("Bos", Questions.Bos);
        PuzzlePosition = new Vector3((float)-66.985, (float)1.507, (float)-268.3);
        PlayerPrefs.SetFloat("K�pZPosition", PuzzlePosition.z);
    }
    //Oyundaki her?ey s?f?rlanarak ba?lang?? ekran?na geri g?n?yoruz
    public void Restart()
    {
        RestartAndQuit.SetActive(false);
        SceneManager.LoadScene(0);
        Questions.Dogru = 0;
        PlayerPrefs.SetInt("Dogru", Questions.Dogru);
        Questions.Yanlis = 0;
        PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
        Questions.Bos = 0;
        PlayerPrefs.SetInt("Bos", Questions.Bos);
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        PuzzlePosition = new Vector3((float)-66.985, (float)1.507, (float)-268.3);
        PlayerPrefs.SetFloat("K�pZPosition", PuzzlePosition.z);
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        sayac = 1;
        ManagerGame.isFinished = false;
        //timer.ResetTimer();
    }
    public void Exit()
    {
        Application.Quit();
    }
    //aktif oldu�unda arka plan duruyor ve panelimiz aktif oluyor
    public void PauseButton()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }
    //panel kapan�p devam ediliyor
    public void Continue()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        AudioListener.volume = 1f;
    }
    //Her�ey s�f�rlanarak ba�lang�� ekran�na d�n�l�yor
    public void HomeMenu()
    {
        sayac = 1;
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        PuzzlePosition = new Vector3((float)-66.985, (float)1.507, (float)-268.3);
        PlayerPrefs.SetFloat("K�pZPosition", PuzzlePosition.z);
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        SceneManager.LoadScene(0);
        //timer.ResetTimer();
    }


}
