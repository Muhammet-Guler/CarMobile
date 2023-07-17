using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Questions : MonoBehaviour
{
    public UnityEngine.UI.Text FirstNumber, SecondNumber, Operator, Conclusion;
    public UnityEngine.UI.Button Btn1, Btn2;
    int OperatorSign;
    double N1, N2, TransactionResult;
    public float Speed=0f;
    public Car car;
    public float moveSpeed;
    public int deger;
    public int Dogru=0;
    public int Yanlis=0;
    public UnityEngine.UI.Text DogruSayisi;
    public UnityEngine.UI.Text YanlisSayisi;
    public GameObject Confetti;
    public GameObject Correct;
    public GameObject False;
    public Timer2 Timer;
    public GoogleAnalyticsAndroidV4 googleAnalytics;

    public const int minValue1 = 1;
    public const int maxValue1 = 10;
    public const int minValue2 = 10;
    public const int maxValue2 = 300;
    // Start is called before the first frame update
    void Start()
    {
        // Dogru ve yanlis sayilari �a��r�l�yor
        Dogru=PlayerPrefs.GetInt("Dogru");
        Yanlis=PlayerPrefs.GetInt("Yanlis");

        Question();
        //zorluk seviyesi i�in degeri �ekiyoruz
        deger = PlayerPrefs.GetInt("deger");


        //butonlar�n i�indeki textlere rastgele de�erler ve rastgele gelen i�lemin sonucunun atamas�n� yap�yoruz. deger==0 kolay mod i�in
        //deger==1 zor mod i�in daha sonra atmas�n� yapt���m�z buton textlerini double t�r�ne �evirerek kontrol� sa�l�yoruz
        //("0.00") i�lemi virg�lden sonra 2 basamak yazd�rmam�z� sa�l�yor
        if (deger == 0)
        {
            Btn1.GetComponentInChildren<Text>().text = UnityEngine.Random.Range(minValue1, maxValue1).ToString("0.00");
            double.Parse(Btn1.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) > 5)
            {
                Btn1.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn1.GetComponentInChildren<Text>().text = (TransactionResult - UnityEngine.Random.Range(minValue1, 5)).ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);

            }
            Btn2.GetComponentInChildren<Text>().text = UnityEngine.Random.Range(minValue1, maxValue1).ToString("0.00");
            double.Parse(Btn2.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
            {
                Btn2.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + UnityEngine.Random.Range(1, 5)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) == double.Parse(Btn2.GetComponentInChildren<Text>().text))
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + UnityEngine.Random.Range(1, 5)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }

            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) % 1 == 0)
            {
                Btn1.GetComponentInChildren<Text>().text = double.Parse(Btn1.GetComponentInChildren<Text>().text).ToString("0");
            }
            if (double.Parse(Btn2.GetComponentInChildren<Text>().text) % 1 == 0)
            {
                Btn2.GetComponentInChildren<Text>().text = double.Parse(Btn2.GetComponentInChildren<Text>().text).ToString("0");
            }
        }


        if (deger == 1)
        {
            Btn1.GetComponentInChildren<Text>().text = UnityEngine.Random.Range(minValue2, maxValue2).ToString("0.00");
            double.Parse(Btn1.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) > 150)
            {
                Btn1.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn1.GetComponentInChildren<Text>().text = (TransactionResult - UnityEngine.Random.Range(minValue1, maxValue1)).ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            Btn2.GetComponentInChildren<Text>().text = UnityEngine.Random.Range(minValue2, maxValue2).ToString("0.00");
            double.Parse(Btn2.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
            {
                Btn2.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + UnityEngine.Random.Range(minValue1, maxValue1)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) == double.Parse(Btn2.GetComponentInChildren<Text>().text))
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + UnityEngine.Random.Range(minValue1, maxValue1)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) % 1 == 0)
            {
                Btn1.GetComponentInChildren<Text>().text = double.Parse(Btn1.GetComponentInChildren<Text>().text).ToString("0");
            }
            if (double.Parse(Btn2.GetComponentInChildren<Text>().text) % 1 == 0)
            {
                Btn2.GetComponentInChildren<Text>().text = double.Parse(Btn2.GetComponentInChildren<Text>().text).ToString("0");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
    //Geri d�n�� fonksiyonu
    public void GoBack()
    {
        SceneManager.LoadScene("CarScene");
    }
    //Cevaplar�n kontrol�n� sa�lad���m�z fonksiyonlar
    //burada do�ru olan� bularak arabay� h�zland�rma ve do�ru yanl�� say�lar�n� sayd�rma i�lemlerini yapt�r�yoruz

    public void AnswerCheck(UnityEngine.UI.Button btn)
    {

        if (Math.Round(double.Parse(btn.GetComponentInChildren<Text>().text), 2) == Math.Round(TransactionResult, 2))
        {
            Conclusion.text = "Do�ru";
            btn.GetComponent<Image>().color = Color.green;
            moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            Dogru = Dogru + 1;
            PlayerPrefs.SetInt("Dogru", Dogru);
            Confetti.SetActive(true);
            Correct.SetActive(true);
            Timer.finnished = true;
            Btn1.interactable = false;
            Btn2.interactable = false;
            Invoke("GoBack", 3f);
        }
        else
        {
            Conclusion.text = "Yanl��";
            btn.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            False.SetActive(true);
            Timer.finnished = true;
            Btn1.interactable = false;
            Btn2.interactable = false;
            Invoke("GoBack", 3f);
        }
    }

    public void AnswerControl1()
    {
        if (double.Parse(Btn1.GetComponentInChildren<Text>().text) == TransactionResult)
        {
            
            Conclusion.text = "Do�ru";
            Btn1.GetComponent<Image>().color = Color.green;
            moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            Dogru = Dogru + 1;
            PlayerPrefs.SetInt("Dogru", Dogru);
            
        
        }
        if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanl��";
            Btn1.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            
        }
        
    }
    public void AnswerControl2()
    {
        if (double.Parse(Btn2.GetComponentInChildren<Text>().text) == TransactionResult)
        {
            Conclusion.text = "Do�ru";
            Btn2.GetComponent<Image>().color = Color.green;
            moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            Dogru = Dogru + 1;
            PlayerPrefs.SetInt("Dogru", Dogru);
            Confetti.SetActive(true);
            Correct.SetActive(true);
            Timer.finnished = true;
            Invoke("GoBack", 3f);

        }
        if (double.Parse(Btn2.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanl��";
            Btn2.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            False.SetActive(true);
            Timer.finnished = true;
            Invoke("GoBack", 3f);
        }
    }
    //Random say� ve random operator olu�turarak her seferinde farkl� sorular olu�turuyoruz
    public void Question()
    {
        deger = PlayerPrefs.GetInt("deger");
        if (deger == 0)
        {
            N1 = UnityEngine.Random.Range(1, 10);
            N2 = UnityEngine.Random.Range(1, 10);
            OperatorSign = UnityEngine.Random.Range(1, 5);

            switch (OperatorSign)
            {
                case 1:
                    Operator.text = "+";
                    TransactionResult = N1 + N2;
                    break;
                case 2:
                    Operator.text = "-";
                    TransactionResult = N1 - N2;
                    break;
                case 3:
                    Operator.text = "*";
                    TransactionResult = N1 * N2;
                    break;
                case 4:
                    Operator.text = "/";
                    TransactionResult = N1 / N2;
                    TransactionResult.ToString("0.00");
                    double.Parse(TransactionResult.ToString("0.00"));
                    break;
            }
            FirstNumber.text = N1 + "";
            SecondNumber.text = N2 + "";
        }
        if (deger == 1)
        {
            N1 = UnityEngine.Random.Range(10, 100);
            N2 = UnityEngine.Random.Range(10, 100);
            OperatorSign = UnityEngine.Random.Range(1, 5);

            switch (OperatorSign)
            {
                case 1:
                    Operator.text = "+";
                    TransactionResult = N1 + N2;
                    break;
                case 2:
                    Operator.text = "-";
                    TransactionResult = N1 - N2;
                    break;
                case 3:
                    Operator.text = "*";
                    TransactionResult = N1 * N2;
                    break;
                case 4:
                    Operator.text = "/";
                    TransactionResult = N1 / N2;
                    TransactionResult.ToString("0.00");
                    double.Parse(TransactionResult.ToString("0.00"));
                    break;
            }
            FirstNumber.text = N1.ToString();
            SecondNumber.text = N2.ToString();
        }

    }
    //��k�� yapt���m�zda h�z� 10f e sabitliyoruz.
    void OnApplicationQuit()
    {
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
    }
    //Do�ru yanl�� say�lar�n� tutup yazd�rd���m�z fonksiyon
    public void DogruYanlis()
    {
        Dogru= PlayerPrefs.GetInt("Dogru");
        Yanlis = PlayerPrefs.GetInt("Yanlis");
        DogruSayisi.text=Dogru.ToString();
        YanlisSayisi.text=Yanlis.ToString(); 
    }
}
