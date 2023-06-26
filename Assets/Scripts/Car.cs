using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
	public float moveSpeed=10f; // Nesnenin hareket hýzý
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
    //arabanýn konumu ve hýzý sürekli güncellenerek tutuluyor
    //arabanýn maksimimum hýzý 360 olarak ayarlanýyor
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
    //finishe geldiðimizde restart ekranýmýz geliyor
    //hýzýmýz sýfýrlanýyor ve arka plandaki her þey duruyor
    //rekor güncelleniyor
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
    //arabanýn konumunu kaydediyoruz
    public void SaveCarPosition()
    {
        PlayerPrefs.SetFloat("CarPositionX", carPosition.x);
        PlayerPrefs.SetFloat("CarPositionY", carPosition.y);
        PlayerPrefs.SetFloat("CarPositionZ", carPosition.z);
        PlayerPrefs.Save();
    }
    //arabanýn konumunu çekiyoruz
    public void LoadCarPosition()
    {
        float posX = PlayerPrefs.GetFloat("CarPositionX");
        float posY = PlayerPrefs.GetFloat("CarPositionY");
        float posZ = PlayerPrefs.GetFloat("CarPositionZ");
        carPosition = new Vector3(posX, posY, posZ);
        transform.position = carPosition;
    }
    //Oyundan çýkýþ yaptýðýmýzda araba baþlangýç konumuna geri dönüyor
    //hýzýmýz tekrardan 10f oluyor
    //doðru yanlýþ sayýlarý sýfýrlanýyor
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
    //Oyundaki herþey sýfýrlanarak baþlangýç ekranýna geri gönüyoruz
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
    //aktif olduðunda arka plan duruyor ve panelimiz aktif oluyor
    public void PauseButton()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    //panel kapanýp devam ediliyor
    public void Continue()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    //Herþey sýfýrlanarak baþlangýç ekranýna dönülüyor
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
