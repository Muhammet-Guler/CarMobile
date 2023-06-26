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
        //SaveCarSpeed();
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
    //public void SaveCarSpeed()
    //{
    //    PlayerPrefs.SetFloat("CarSpeed", moveSpeed);
    //    PlayerPrefs.Save();
    //}
    //public void LoadCarSpeed()
    //{
    //    float CarSpeed = PlayerPrefs.GetFloat("CarSpeed");
    //    moveSpeed = CarSpeed;
    //}
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
}
