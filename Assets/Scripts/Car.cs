using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class Car : MonoBehaviour
{
    public float moveSpeed = 10f; // Nesnenin hareket hýzý
    public bool stop = false;
    public SpeedIndicator speedIndicator;
    public Timer timer;
    public Vector3 carPosition;
    public GameManager ManagerGame;
    public GameObject RestartAndQuit;
    public Questions Questions;
    public GameObject PausePanel;
    public int sayac = 0;
    public AudioSource SesKaynak;
    public int deger;
    public GameObject Road;
    public GameObject Road2;
    public Transform[] Roads;
    public Sound sound;
    public GameObject Puzzle;
    int ses;
    public Vector3 PuzzlePosition;
    public GameObject CubesAnswers;
    void Start()
    {
        Time.timeScale = 1f;
        LoadCarPosition();
        sayac = 0;
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

        AnimateWheels();
        
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
            //timer.StopTimer();
            if (moveSpeed > 60f)
            {
                moveSpeed = 0f;
            }

            ManagerGame.isFinished = true;
            timer.CheckHighScore();
            SesDurdur();
        }
        if (other.tag == "prefabs")
        {
            Roads[0].localPosition += new Vector3(0, 0, Roads[0].localScale.z + (float)92.5f);
            Array.Reverse(Roads);
        }
        if (other.tag=="engel")
        {
            CubesAnswers.transform.position += new Vector3(0, 0, 50f);
            Questions.Start();
            Questions.Bos = Questions.Bos + 1;
            PlayerPrefs.SetInt("Bos", Questions.Bos);

        }
        if (other.tag == "cevapbir")
        {
            if (Math.Round(float.Parse(Questions.Text1.text.ToString()),2) == Math.Round(Questions.TransactionResult,2))
            {
                Questions.TransactionResult = PlayerPrefs.GetFloat("TransactionResult");
                moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                moveSpeed =moveSpeed+ float.Parse(Questions.Text1.text);
                PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                CubesAnswers.transform.position += new Vector3(0, 0, 50f);
                Questions.Start();
                Questions.Dogru = Questions.Dogru + 1;
                PlayerPrefs.SetInt("Dogru", Questions.Dogru);
            }
            else
            {
                if (Questions.TransactionResult < 0)
                {
                    Questions.TransactionResult = PlayerPrefs.GetFloat("TransactionResult");
                    moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                    moveSpeed = moveSpeed + float.Parse(Questions.Text1.text);
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                }
                if (Questions.TransactionResult > 0)
                {
                    Questions.TransactionResult = PlayerPrefs.GetFloat("TransactionResult");
                    moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                    moveSpeed = moveSpeed - float.Parse(Questions.Text1.text);
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                }
                Questions.Yanlis = Questions.Yanlis + 1;
                PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
                CubesAnswers.transform.position += new Vector3(0, 0, 50f);
                Questions.Start();
            }
            if (moveSpeed <= 0)
            {
                moveSpeed = 0f;
                PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                RestartAndQuit.SetActive(true);
                Questions.DogruYanlis();
                Time.timeScale = 0f;
                ManagerGame.isFinished = true;
                SesDurdur();
            }
        }
        if (other.tag == "cevapiki")
        {
            if (Math.Round(float.Parse(Questions.Text1.text.ToString()), 2) == Math.Round(Questions.TransactionResult, 2))
            {
                Questions.TransactionResult = PlayerPrefs.GetFloat("TransactionResult");
                moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                moveSpeed = moveSpeed + float.Parse(Questions.Text2.text);
                PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                CubesAnswers.transform.position += new Vector3(0, 0, 50f);
                Questions.Start();
                Questions.Dogru = Questions.Dogru + 1;
                PlayerPrefs.SetInt("Dogru", Questions.Dogru);

            }
            else
            {
                if (Questions.TransactionResult<0)
                {
                    Questions.TransactionResult = PlayerPrefs.GetFloat("TransactionResult");
                    moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                    moveSpeed = moveSpeed + float.Parse(Questions.Text2.text);
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                }
                if (Questions.TransactionResult>0) 
                {
                    Questions.TransactionResult = PlayerPrefs.GetFloat("TransactionResult");
                    moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
                    moveSpeed = moveSpeed - float.Parse(Questions.Text2.text);
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                }
                Questions.Yanlis = Questions.Yanlis + 1;
                PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
                CubesAnswers.transform.position += new Vector3(0, 0, 50f);
                Questions.Start();
            }
        }
        if (moveSpeed <= 0f)
        {
            moveSpeed = 0f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            RestartAndQuit.SetActive(true);
            Questions.DogruYanlis();
            Time.timeScale = 0f;
            ManagerGame.isFinished = true;
            SesDurdur();
        }
    }

    public void SesOynat()
    {
        SesKaynak.Play();
    }
    public void SesDurdur()
    {
        SesKaynak.Stop();
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
    //aktif olduðunda arka plan duruyor ve panelimiz aktif oluyor
    public void PauseButton()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }
    //panel kapanýp devam ediliyor
    public void Continue()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        AudioListener.volume = 1f;
    }
    //Herþey sýfýrlanarak baþlangýç ekranýna dönülüyor
    public void HomeMenu()
    {
        sayac = 1;
        carPosition = new Vector3((float)-67.28, 0, (float)-298.5);
        transform.position = carPosition;
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        SceneManager.LoadScene(0);
    }
    public void IlkBaslangic()
    {
        if (deger==0||deger==1)
        {
            carPosition = new Vector3((float)-67.28, 0, (float)-293.8);
            transform.position = carPosition;

        }
    }
    public enum ControlMode
    {
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear
    }
    [Serializable]
    public struct Wheel
    {
        public GameObject whellModel;
        public WheelCollider whelCollider;
        public Axel axel;
    }
    public ControlMode control;
    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public List<Wheel> wheels;
    //float moveInput;
    float steerInput;
    private Rigidbody carRb;
    public Vector3 _centerOfMass;

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.whelCollider.motorTorque = moveSpeed;
        }
    }
    private void LateUpdate()
    {
        Move();
        Steer();
    }
    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel==Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.whelCollider.steerAngle = Mathf.Lerp(wheel.whelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }
    void AnimateWheels()
    {
        foreach( var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.whelCollider.GetWorldPose(out pos, out rot);
            wheel.whellModel.transform.position = pos;
            wheel.whellModel.transform.rotation = rot;
        }
    }
    void cevapbir()
    {
        moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
        if (Math.Round(double.Parse(Questions.Text1.text.ToString()), 2) == Math.Round(Questions.TransactionResult, 2))
        {
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        }
        else
        {
            moveSpeed = moveSpeed - 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);


        }
        CubesAnswers.transform.position += new Vector3(0, 0, 50f);
        Questions.Start();
    }
    void cevapiki()
    {
        moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
        if (Math.Round(double.Parse(Questions.Text2.text.ToString()), 2) == Math.Round(Questions.TransactionResult, 2))
        {
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        }
        else
        {
            moveSpeed = moveSpeed - 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);


        }
        CubesAnswers.transform.position += new Vector3(0, 0, 50f);
        Questions.Start();
    }
}
