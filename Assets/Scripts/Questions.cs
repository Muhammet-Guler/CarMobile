using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GoogleAnalyticsAndroidV4 googleAnalytics;
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
            Btn1.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString("0.00");
            double.Parse(Btn1.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) > 5)
            {
                Btn1.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn1.GetComponentInChildren<Text>().text = (TransactionResult - Random.Range(1, 5)).ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            Btn2.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString("0.00");
            double.Parse(Btn2.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
            {
                Btn2.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + Random.Range(1, 5)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) == double.Parse(Btn2.GetComponentInChildren<Text>().text))
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + Random.Range(1, 5)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
        }


        if (deger == 1)
        {
            Btn1.GetComponentInChildren<Text>().text = Random.Range(10, 300).ToString("0.00");
            double.Parse(Btn1.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) > 150)
            {
                Btn1.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn1.GetComponentInChildren<Text>().text = (TransactionResult - Random.Range(1, 10)).ToString("0.00");
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            Btn2.GetComponentInChildren<Text>().text = Random.Range(10, 300).ToString("0.00");
            double.Parse(Btn2.GetComponentInChildren<Text>().text);
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
            {
                Btn2.GetComponentInChildren<Text>().text = TransactionResult.ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + Random.Range(1, 10)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (double.Parse(Btn1.GetComponentInChildren<Text>().text) == double.Parse(Btn2.GetComponentInChildren<Text>().text))
            {
                Btn2.GetComponentInChildren<Text>().text = (TransactionResult + Random.Range(1, 10)).ToString("0.00");
                double.Parse(Btn2.GetComponentInChildren<Text>().text);
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
        SceneManager.LoadScene(1);
    }
    //Cevaplarýn kontrolünü saðladýðýmýz fonksiyonlar
    //burada doðru olaný bularak arabayý hýzlandýrma ve doðru yanlýþ sayýlarýný saydýrma iþlemlerini yaptýrýyoruz
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
            GoBack();
        
        }
        if (double.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanlýþ";
            Btn1.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            GoBack();
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
            GoBack();

        }
        if (double.Parse(Btn2.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanlýþ";
            Btn2.GetComponent<Image>().color = Color.red;
            Yanlis = Yanlis + 1;
            PlayerPrefs.SetInt("Yanlis", Yanlis);
            GoBack();
        }
    }
    //Random sayý ve random operator oluþturarak her seferinde farklý sorular oluþturuyoruz
    public void Question()
    {
        deger = PlayerPrefs.GetInt("deger");
        if (deger == 0)
        {
            N1 = Random.Range(1, 10);
            N2 = Random.Range(1, 10);
            OperatorSign = Random.Range(1, 5);

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
        if (deger==1)
        {
            N1 = Random.Range(10,100);
            N2 = Random.Range(10,100);
            OperatorSign = Random.Range(1, 5);

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
