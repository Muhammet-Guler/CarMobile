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
        // Dogru ve yanlis sayilari çaðýrýlýyor
        Dogru=PlayerPrefs.GetInt("Dogru");
        Yanlis=PlayerPrefs.GetInt("Yanlis");

        Question();
        //zorluk seviyesi için degeri çekiyoruz
        deger = PlayerPrefs.GetInt("deger");


        //butonlarýn içindeki textlere rastgele deðerler ve rastgele gelen iþlemin sonucunun atamasýný yapýyoruz. deger==0 kolay mod için
        //deger==1 zor mod için daha sonra atmasýný yaptýðýmýz buton textlerini double türüne çevirerek kontrolü saðlýyoruz
        //("0.00") iþlemi virgülden sonra 2 basamak yazdýrmamýzý saðlýyor
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
    //Geri dönüþ fonksiyonu
    public void GoBack()
    {
        SceneManager.LoadScene("CarScene");
    }
    //Cevaplarýn kontrolünü saðladýðýmýz fonksiyonlar
    //burada doðru olaný bularak arabayý hýzlandýrma ve doðru yanlýþ sayýlarýný saydýrma iþlemlerini yaptýrýyoruz

    public void AnswerCheck(UnityEngine.UI.Button btn)
    {

        if (Math.Round(double.Parse(btn.GetComponentInChildren<Text>().text), 2) == Math.Round(TransactionResult, 2))
        {
            Conclusion.text = "Doðru";
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
            Conclusion.text = "Yanlýþ";
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
            
            Conclusion.text = "Doðru";
            Btn1.GetComponent<Image>().color = Color.green;
            moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            Dogru = Dogru + 1;
            PlayerPrefs.SetInt("Dogru", Dogru);
            
        
        }
        if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanlýþ";
            Btn1.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            
        }
        
    }
    public void AnswerControl2()
    {
        if (double.Parse(Btn2.GetComponentInChildren<Text>().text) == TransactionResult)
        {
            Conclusion.text = "Doðru";
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
            Conclusion.text = "Yanlýþ";
            Btn2.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            False.SetActive(true);
            Timer.finnished = true;
            Invoke("GoBack", 3f);
        }
    }
    //Random sayý ve random operator oluþturarak her seferinde farklý sorular oluþturuyoruz
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
    //Çýkýþ yaptýðýmýzda hýzý 10f e sabitliyoruz.
    void OnApplicationQuit()
    {
        moveSpeed = 10f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
    }
    //Doðru yanlýþ sayýlarýný tutup yazdýrdýðýmýz fonksiyon
    public void DogruYanlis()
    {
        Dogru= PlayerPrefs.GetInt("Dogru");
        Yanlis = PlayerPrefs.GetInt("Yanlis");
        DogruSayisi.text=Dogru.ToString();
        YanlisSayisi.text=Yanlis.ToString(); 
    }
}
