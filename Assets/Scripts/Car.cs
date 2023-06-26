using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
	public float moveSpeed=10f; // Nesnenin hareket h�z�
	public bool stop= false;
	public SpeedIndicator speedIndicator;
	public Timer timer;
    public Vector3 carPosition;
    public GameManager ManagerGame;
    public GameObject RestartAndQuit;
    public Questions Questions;
    public GameObject PausePanel;
    void Start()
	{
		Time.timeScale = 1f;
        LoadCarPosition();
    }
    //araban�n konumu ve h�z� s�rekli g�ncellenerek tutuluyor
    //araban�n maksimimum h�z� 360 olarak ayarlan�yor
	void Update()
	{
        moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		if (moveSpeed > 360)
        {
			moveSpeed = 360;
        }
        carPosition = transform.position;
    }
    //finishe geldi�imizde restart ekran�m�z geliyor
    //h�z�m�z s�f�rlan�yor ve arka plandaki her �ey duruyor
    //rekor g�ncelleniyor
    private void OnTriggerEnter(Collider other)
    {
			if (other.tag == "finish")
		{

            RestartAndQuit.SetActive(true);
            Questions.DogruYanlis();
            Time.timeScale = 0f;
			moveSpeed = 0f;
			timer.StopTimer();
            if (moveSpeed>60f)
            {
                moveSpeed = 0f;
            }

            CancelInvoke("QuestionsScene");
            timer.CheckHighScore();
        }
    }
    void OnDestroy()
    {
        SaveCarPosition();
    }
    //araban�n konumunu kaydediyoruz
    public void SaveCarPosition()
    {
        PlayerPrefs.SetFloat("CarPositionX", carPosition.x);
        PlayerPrefs.SetFloat("CarPositionY", carPosition.y);
        PlayerPrefs.SetFloat("CarPositionZ", carPosition.z);
        PlayerPrefs.Save();
    }
    //araban�n konumunu �ekiyoruz
    public void LoadCarPosition()
    {
        float posX = PlayerPrefs.GetFloat("CarPositionX");
        float posY = PlayerPrefs.GetFloat("CarPositionY");
        float posZ = PlayerPrefs.GetFloat("CarPositionZ");
        carPosition = new Vector3(posX, posY, posZ);
        transform.position = carPosition;
    }
    //Oyundan ��k�� yapt���m�zda araba ba�lang�� konumuna geri d�n�yor
    //h�z�m�z tekrardan 10f oluyor
    //do�ru yanl�� say�lar� s�f�rlan�yor
    void OnApplicationQuit()
    {
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi",moveSpeed);
        Questions.Dogru = 0;
        PlayerPrefs.SetInt("Dogru",Questions.Dogru);
        Questions.Yanlis = 0;
        PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
    }
    //Oyundaki her�ey s�f�rlanarak ba�lang�� ekran�na geri g�n�yoruz
    public void Restart()
    {
        RestartAndQuit.SetActive(false);
        SceneManager.LoadScene(0);
        Questions.Dogru = 0;
        PlayerPrefs.SetInt("Dogru", Questions.Dogru);
        Questions.Yanlis = 0;
        PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        timer.ResetTimer();
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
    }
    //panel kapan�p devam ediliyor
    public void Continue()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    //Her�ey s�f�rlanarak ba�lang�� ekran�na d�n�l�yor
    public void HomeMenu()
    {
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        SceneManager.LoadScene(0);
        timer.ResetTimer();
    }
}
