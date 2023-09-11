using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Transactions;

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
    public int deger;
    public GameObject Road;
    public GameObject Road2;
    public Transform[] Roads;
    public Sound sound;
    public GameObject Puzzle;
    int ses;
    public Vector3 PuzzlePosition;
    public GameObject CubesAnswers;
    public bool DogruMu;
    public int geriSayimSure = 10;
    public float uzaklastir=0;
    public float yakinlastir = 0;
    public Material hedefMalzeme;
    public UnityEngine.UI.Image[] Hearts=new Image[4];
    public float index = 5;
    public float CanHakký;
    void Start()
    {
        Time.timeScale = 1f;
        LoadCarPosition();
        sayac = 0;
        Color bejRenk = new Color(200f / 255f, 191f / 255f, 149f / 255f);
        hedefMalzeme.color = bejRenk;
        GetComponent<Renderer>().material = hedefMalzeme;
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
        //AnimateWheels();
        if (moveSpeed<=0f||index==0)
        {
            SesDurdur();
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
            index = 5;
            PlayerPrefs.SetFloat("index", index);
        }
        if (!Hearts[0].gameObject.activeSelf)
        {
            SesDurdur();
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
            index = 5;
            PlayerPrefs.SetFloat("index", index);
        }
        if (moveSpeed<10)
        {
            moveSpeed = 10;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        }
        if (moveSpeed >= 100)
        {
            moveSpeed = 100;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
        }
    }
    //finishe geldiðimizde restart ekranýmýz geliyor
    //hýzýmýz sýfýrlanýyor ve arka plandaki her þey duruyor
    //rekor güncelleniyor
    private void OnTriggerEnter(Collider other)
    {

        //if (other.tag == "finish")
        //{
        //    RestartAndQuit.SetActive(true);
        //    Questions.DogruYanlis();
        //    Time.timeScale = 0f;
        //    moveSpeed = 0f;
        //    //timer.StopTimer();
        //    if (moveSpeed > 60f)
        //    {
        //        moveSpeed = 0f;
        //    }

        //    ManagerGame.isFinished = true;
        //    timer.CheckHighScore();
        //    SesDurdur();
        //}
        if (other.tag == "prefabs")
        {
            Roads[0].localPosition += new Vector3(0, 0, Roads[0].localScale.z + (float)225.5f);
            //Array.Reverse(Roads);
        }
        if (other.tag=="engel")
        {
            //CubesAnswers.transform.position += new Vector3(0, 0, 50f);
            //Questions.Start();
            PlayerPrefs.GetFloat("index");
            Questions.Bos= PlayerPrefs.GetInt("Bos");
            Questions.Bos = Questions.Bos + 1;
            PlayerPrefs.SetInt("Bos", Questions.Bos);
            Hearts[(int)index].gameObject.SetActive(false);
            index = index - (float)0.5;
            PlayerPrefs.SetFloat("index", index);
            StartCoroutine(BosAnim());
            
        }
        if (other.tag == "cevapbir")
        {
            if (Math.Round(float.Parse(Questions.Text1.text.ToString()),2) == Math.Round((float)Questions.TransactionResult,2))
            {
                CanHakký += (float)0.5;
                Questions.Dogru = Questions.Dogru + 1;
                PlayerPrefs.SetInt("Dogru", Questions.Dogru);
                StartCoroutine(DogruCevapAnim());
                //if (Bariyerler != null && Bariyerler.Length > 0)
                //{
                //    foreach (GameObject hedefGameObject in Bariyerler)
                //    {
                //        // Her bir GameObject üzerindeki Renderer bileþenini alýn.
                //        Renderer rend = hedefGameObject.GetComponent<Renderer>();

                //        // Renderer bulundu ve yeni renk ayarlandýysa, renkleri güncelleyin.
                //        if (rend != null)
                //        {
                //            rend.material.color = Color.green;
                //        }
                //    }
                //}
                
                if (Questions.TransactionResult > 0)
                {
                    moveSpeed = moveSpeed + (float)5 / 2;
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                    if (moveSpeed >= 100)
                    {
                        moveSpeed = 100;
                        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                    }

                }
                if (Questions.TransactionResult < 0)
                {
                    moveSpeed = moveSpeed - (float)5 / 2;
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                    if (moveSpeed >= 100)
                    {
                        moveSpeed = 100;
                        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                    }
                }
                if (CanHakký%3==0)
                {
                    PlayerPrefs.GetFloat("index");
                    index = index + (float)0.5;
                    Hearts[(int)index].gameObject.SetActive(true);
                    PlayerPrefs.SetFloat("index", index); 
                    
                }
                if (index >= 5)
                {
                    index = 5;
                    PlayerPrefs.SetFloat("index", index);
                }

            }
            if (Math.Round(float.Parse(Questions.Text1.text.ToString()), 2) != Math.Round((float)Questions.TransactionResult, 2))
            {
                CanHakký = 0; 
                PlayerPrefs.GetFloat("index");
                Questions.Yanlis = Questions.Yanlis + 1;
                PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
                StartCoroutine(YanlisCevapAnim());
                Hearts[(int)index].gameObject.SetActive(false);
                index = index - (float)0.5;
                PlayerPrefs.SetFloat("index", index);

                if (Questions.TransactionResult < 0)
                {
                    if (moveSpeed >= 10)
                    {
                        moveSpeed = moveSpeed + (float)5 / 2;
                        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                    }
                }
                if (Questions.TransactionResult > 0)
                {
                    if (moveSpeed>=10) {
                        moveSpeed = moveSpeed - (float)5 / 2;
                        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                    }
                    
                }

            }
        }
        if (other.tag == "cevapiki")
        {
            if (Math.Round(float.Parse(Questions.Text2.text.ToString()), 2) == Math.Round((float)Questions.TransactionResult, 2))
            {
                CanHakký += (float)0.5;
                Questions.Dogru = Questions.Dogru + 1;
                PlayerPrefs.SetInt("Dogru", Questions.Dogru);
                StartCoroutine(DogruCevapAnim());
                if (Questions.TransactionResult>0)
                {
                    if (moveSpeed >= 10)
                    {
                        moveSpeed = moveSpeed + (float)5 / 2;
                        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                        if (moveSpeed >= 100)
                        {
                            moveSpeed = 100;
                            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                        }
                    }
                }
                if (Questions.TransactionResult < 0)
                {
                    if (moveSpeed >= 10)
                    {
                        moveSpeed = moveSpeed - (float)5 / 2;
                        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                        if (moveSpeed >= 100)
                        {
                            moveSpeed = 100;
                            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                        }
                    }
                }
                if (CanHakký % 3 == 0)
                {
                    PlayerPrefs.GetFloat("index");
                    index = index + (float)0.5;
                    Hearts[(int)index].gameObject.SetActive(true);
                    PlayerPrefs.SetFloat("index", index);
                    
                }
                if (index >= 5)
                {
                    index = 5;
                    PlayerPrefs.SetFloat("index", index);
                }

            }
            if (Math.Round(float.Parse(Questions.Text2.text.ToString()), 2) != Math.Round((float)Questions.TransactionResult, 2))
            {
                CanHakký = 0;
                PlayerPrefs.GetFloat("index");
                Questions.Yanlis = Questions.Yanlis + 1;
                PlayerPrefs.SetInt("Yanlis", Questions.Yanlis);
                StartCoroutine(YanlisCevapAnim());
                Hearts[(int)index].gameObject.SetActive(false);
                index = index - (float)0.5;
                PlayerPrefs.SetFloat("index", index);
                if (Questions.TransactionResult<0)
                {
                    moveSpeed = moveSpeed + (float)5 / 2;
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);

                }
                if (Questions.TransactionResult > 0)
                {
                    moveSpeed = moveSpeed - (float)5 / 2;
                    PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
                }

            }
        }
        if(other.tag == "soru")
        {
            //if (DogruMu==true)
            //{
            //    uzaklastir = uzaklastir + 10f;
            //    geriSayimSure -= 1;
            //    if (geriSayimSure<=1)
            //    {
            //        geriSayimSure = 1;
            //    }
            //    StartCoroutine(GeriSayim());
            //    CubesAnswers.transform.position += new Vector3(0, 0, uzaklastir+50f);

            //}
            //else
            //{
            //    yakinlastir = yakinlastir + 10f;
            //    geriSayimSure = 5;
            //    StartCoroutine(GeriSayim());
            //    CubesAnswers.transform.position += new Vector3(0, 0, 50f-yakinlastir);
            //}
            CubesAnswers.transform.position += new Vector3(0, 0, 70f);
            Questions.Start();
        }
        if (other.tag=="bariyer")
        {
            transform.position = new Vector3(carPosition.x+4f, 0, carPosition.z);
            PlayerPrefs.GetFloat("index");
            Hearts[(int)index].gameObject.SetActive(false);
            index = index - (float)0.25;
            PlayerPrefs.SetFloat("index", index);
            if (moveSpeed>10)
            {
                moveSpeed -= (float)5 / 4f;
                PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            }
            
        }
        if (other.tag == "bariyer2")
        {
            transform.position = new Vector3(carPosition.x-4f, 0, carPosition.z);
            PlayerPrefs.GetFloat("index");
            Hearts[(int)index].gameObject.SetActive(false);
            index = index - (float)0.25;
            PlayerPrefs.SetFloat("index", index); 
            if (moveSpeed > 10)
            {
                moveSpeed -= (float)5 / 4f;
                PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            }
        }
    }
    public IEnumerator GeriSayim()
    {
        while (geriSayimSure > 0)
        {
            yield return new WaitForSeconds(1f);
            geriSayimSure--;
            Questions.Start();
            
        }

    }
    public void SesOynat()
    {
        sound.PlaySound();
    }
    public void SesDurdur()
    {
        sound.PauseSound();
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
        uzaklastir = 0;
        yakinlastir = 0;
        index = 5;
        PlayerPrefs.SetFloat("index", index);
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
        index=5;
        PlayerPrefs.SetFloat("index", index);
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
    //public enum ControlMode
    //{
    //    Buttons
    //};

    //public enum Axel
    //{
    //    Front,
    //    Rear
    //}
    //[Serializable]
    //public struct Wheel
    //{
    //    public GameObject whellModel;
    //    public WheelCollider whelCollider;
    //    public Axel axel;
    //}
    //public ControlMode control;
    //public float maxAcceleration = 30.0f;
    //public float brakeAcceleration = 50.0f;
    //public float turnSensitivity = 1.0f;
    //public float maxSteerAngle = 30.0f;

    //public List<Wheel> wheels;
    ////float moveInput;
    //float steerInput;
    //private Rigidbody carRb;
    //public Vector3 _centerOfMass;

    //public void SteerInput(float input)
    //{
    //    steerInput = input;
    //}

    //void Move()
    //{
    //    foreach (var wheel in wheels)
    //    {
    //        wheel.whelCollider.motorTorque = 30f;
    //    }
    //}
    //private void LateUpdate()
    //{
    //    Move();
    //    Steer();
    //}
    //void Steer()
    //{
    //    foreach(var wheel in wheels)
    //    {
    //        if (wheel.axel==Axel.Front)
    //        {
    //            var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
    //            wheel.whelCollider.steerAngle = Mathf.Lerp(wheel.whelCollider.steerAngle, _steerAngle, 2f);
    //        }
    //    }
    //}
    //void AnimateWheels()
    //{
    //    foreach( var wheel in wheels)
    //    {
    //        Quaternion rot;
    //        Vector3 pos;
    //        wheel.whelCollider.GetWorldPose(out pos, out rot);
    //        wheel.whellModel.transform.position = pos;
    //        wheel.whellModel.transform.rotation = rot;
    //    }
    //}
    //void cevapbir()
    //{
    //    moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
    //    if (Math.Round(double.Parse(Questions.Text1.text.ToString()), 2) == Math.Round(Questions.TransactionResult, 2))
    //    {
    //        moveSpeed += 5f;
    //        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
    //    }
    //    else
    //    {
    //        moveSpeed = moveSpeed - 5f;
    //        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);


    //    }
    //    CubesAnswers.transform.position += new Vector3(0, 0, 50f);
    //    Questions.Start();
    //}
    //void cevapiki()
    //{
    //    moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
    //    if (Math.Round(double.Parse(Questions.Text2.text.ToString()), 2) == Math.Round(Questions.TransactionResult, 2))
    //    {
    //        moveSpeed += 5f;
    //        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
    //    }
    //    else
    //    {
    //        moveSpeed = moveSpeed - 5f;
    //        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);


    //    }
    //    CubesAnswers.transform.position += new Vector3(0, 0, 50f);
    //    Questions.Start();
    //}
    public IEnumerator Right()
    {
        
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.015f);
            carPosition = new Vector3(carPosition.x + (i*(float)0.4), 0, carPosition.z);
            transform.position = carPosition;
        }
        for (int i = 0; i < 15; i++)
        {
            carPosition = new Vector3(carPosition.x, 0, carPosition.z + 0.2f);
            transform.position = carPosition;
        }
        //if (carPosition.x > (float)-63.29098)
        //{
        //    transform.position = new Vector3((float)-63.28098, 0, carPosition.z);
        //}

    }
    public IEnumerator Left()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.015f);
            carPosition = new Vector3(carPosition.x - (i * (float)0.4), 0, carPosition.z);
            transform.position = carPosition;
        }
        for (int i = 0; i < 15; i++)
        {
            carPosition = new Vector3(carPosition.x, 0, carPosition.z+0.2f);
            transform.position = carPosition;
        }
        //if (carPosition.x< (float)-71.29061)
        //{
        //    transform.position = new Vector3((float)-71.28061, 0, carPosition.z);
        //}
    }
    public void RightButton()
    {
        StartCoroutine(Right());
    }
    public void LeftButton()
    {
        StartCoroutine(Left());
    }
    public IEnumerator DogruCevapAnim()
    {
        
        while (geriSayimSure > 0)
        {
            yield return new WaitForSeconds(0.1f);
            geriSayimSure--;
            if (hedefMalzeme != null)
            {
                hedefMalzeme.SetColor("_Color", Color.green);
            }

            
        }
        geriSayimSure = 10;
        Color bejRenk = new Color(210f / 255f, 180f / 255f, 140f / 255f);
            hedefMalzeme.color = bejRenk;
            GetComponent<Renderer>().material = hedefMalzeme;
        
    }
    public IEnumerator YanlisCevapAnim()
    {
        
        while (geriSayimSure > 0)
        {
            yield return new WaitForSeconds(0.1f);
            geriSayimSure--;
            if (hedefMalzeme != null)
            {
                hedefMalzeme.SetColor("_Color", Color.red);
            }

        }
        geriSayimSure = 10;
            Color bejRenk = new Color(210f / 255f, 180f / 255f, 140f / 255f);
            hedefMalzeme.color = bejRenk;
            GetComponent<Renderer>().material = hedefMalzeme;
        
    }
    public IEnumerator BosAnim()
    {

        while (geriSayimSure > 0)
        {
            yield return new WaitForSeconds(0.1f);
            geriSayimSure--;
            if (hedefMalzeme != null)
            {
                hedefMalzeme.SetColor("_Color", Color.yellow);
            }

        }
        geriSayimSure = 10;
        Color bejRenk = new Color(210f / 255f, 180f / 255f, 140f / 255f);
        hedefMalzeme.color = bejRenk;
        GetComponent<Renderer>().material = hedefMalzeme;

    }
}
