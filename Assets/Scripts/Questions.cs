using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class Questions : MonoBehaviour
{
    public UnityEngine.UI.Text FirstNumber, SecondNumber, Operator, Conclusion;
    public TMP_Text Text1, Text2;
    public UnityEngine.UI.Button Btn1, Btn2;
    int OperatorSign;
    public float N1, N2, TransactionResult;
    public float Speed = 0f;
    public Car car;
    public float moveSpeed;
    public int deger;
    public int Dogru = 0;
    public int Yanlis = 0;
    public int Bos = 0;
    public UnityEngine.UI.Text DogruSayisi;
    public UnityEngine.UI.Text YanlisSayisi;
    public UnityEngine.UI.Text BosSayisi;
    public GameObject Confetti;
    public GameObject Correct;
    public GameObject False;

    public const int KolayMinValue = 1;
    public const int KolayMaxValue = 10;
    public const int ZorMinValue = 1;
    public const int ZorMaxValue = 50;
    // Start is called before the first frame update
    public void Start()
    {
        // Dogru ve yanlis sayilari �a��r�l�yor
        Dogru = PlayerPrefs.GetInt("Dogru");
        Yanlis = PlayerPrefs.GetInt("Yanlis");
        Bos = PlayerPrefs.GetInt("Bos");

        Question();
        
        //zorluk seviyesi i�in degeri �ekiyoruz
        deger = PlayerPrefs.GetInt("deger");


        //butonlar�n i�indeki textlere rastgele de�erler ve rastgele gelen i�lemin sonucunun atamas�n� yap�yoruz. deger==0 kolay mod i�in
        //deger==1 zor mod i�in daha sonra atmas�n� yapt���m�z buton textlerini double t�r�ne �evirerek kontrol� sa�l�yoruz
        //("0.00") i�lemi virg�lden sonra 2 basamak yazd�rmam�z� sa�l�yor
        if (deger == 0)
        {
            Text1.text = UnityEngine.Random.Range(KolayMinValue, KolayMaxValue).ToString("0.00");
            float.Parse(Text1.text);
            if (float.Parse(Text1.text) > 4)
            {
                Text1.text = TransactionResult.ToString("0.00");
                float.Parse(Text1.text);
            }
            else
            {
                Text1.text = (TransactionResult - UnityEngine.Random.Range(1, 3)).ToString("0.00");
                float.Parse(Text1.text.ToString());

            }
            Text2.text = UnityEngine.Random.Range(KolayMinValue, KolayMaxValue).ToString("0.00");
            float.Parse(Text2.text.ToString());
            if (float.Parse(Text2.text.ToString()) != TransactionResult)
            {
                Text2.text = TransactionResult.ToString("0.00");
                float.Parse(Text2.text.ToString());
            }
            else
            {
                Text2.text = (TransactionResult + UnityEngine.Random.Range(1, 3)).ToString("0.00");
                float.Parse(Text2.text.ToString());
            }
            if (float.Parse(Text1.text.ToString()) == double.Parse(Text2.text.ToString()))
            {
                Text2.text = (TransactionResult + UnityEngine.Random.Range(1, 3)).ToString("0.00");
                float.Parse(Text2.text.ToString());
            }

            if (float.Parse(Text1.text) % 1 == 0)
            {
                Text1.text = float.Parse(Text1.text.ToString()).ToString("0");
            }
            if (float.Parse(Text2.text) % 1 == 0)
            {
                Text2.text = float.Parse(Text2.text.ToString()).ToString("0");
            }
            if (TransactionResult > 100)
            {
                Start();
            }
        }


        if (deger == 1)
        {
            Text1.text = UnityEngine.Random.Range(ZorMinValue, ZorMaxValue).ToString("0.00");
            float.Parse(Text1.text);
            if (float.Parse(Text1.text) > 25)
            {
                Text1.text = TransactionResult.ToString("0.00");
                float.Parse(Text1.text);
            }
            else
            {
                Text1.text = (TransactionResult - UnityEngine.Random.Range(1, 3)).ToString("0.00");
                float.Parse(Text1.text);
            }
            Text2.text = UnityEngine.Random.Range(ZorMinValue, ZorMaxValue).ToString("0.00");
            float.Parse(Text2.text);
            if (float.Parse(Text1.text) != TransactionResult)
            {
                Text2.text = TransactionResult.ToString("0.00");
                float.Parse(Text2.text);
            }
            else
            {
                Text2.text = (TransactionResult + UnityEngine.Random.Range(1, 3)).ToString("0.00");
                float.Parse(Text2.text);
            }
            if (float.Parse(Text1.text) == float.Parse(Text2.text))
            {
                Text2.text = (TransactionResult + UnityEngine.Random.Range(1, 3)).ToString("0.00");
                float.Parse(Text2.text);
            }
            if (float.Parse(Text1.text) % 1 == 0)
            {
                Text1.text = float.Parse(Text1.text).ToString("0");
            }
            if (float.Parse(Text2.text) % 1 == 0)
            {
                Text2.text = float.Parse(Text2.text).ToString("0");
            }
            if (TransactionResult > 500)
            {
                Start();
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

    //public void AnswerCheck(UnityEngine.UI.Button btn)
    //{

    //    if (Math.Round(double.Parse(btn.GetComponentInChildren<Text>().text), 2) == Math.Round(TransactionResult, 2))
    //    {
    //        Conclusion.text = "Do�ru";
    //        btn.GetComponent<Image>().color = Color.green;
    //        moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
    //        moveSpeed += 5f;
    //        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
    //        Dogru = Dogru + 1;
    //        PlayerPrefs.SetInt("Dogru", Dogru);
    //        Confetti.SetActive(true);
    //        Correct.SetActive(true);
    //        Timer.finnished = true;
    //        Btn1.interactable = false;
    //        Btn2.interactable = false;
    //        Invoke("GoBack", 3f);
    //        float geriSayimSure = Timer.geriSayimSure;
    //        PlayerPrefs.SetFloat("geriSayimSure", geriSayimSure);
    //        if (Timer.geriSayimSure <= 2)
    //        {
    //            Timer.Finnish();
    //        }
    //    }
    //    else
    //    {
    //        Conclusion.text = "Yanl��";
    //        btn.GetComponent<Image>().color = Color.red;
    //        Yanlis = Yanlis + 1;
    //        PlayerPrefs.SetInt("Yanlis", Yanlis);
    //        False.SetActive(true);
    //        Timer.finnished = true;
    //        Btn1.interactable = false;
    //        Btn2.interactable = false;
    //        Invoke("GoBack", 3f);
    //        float geriSayimSure = Timer.geriSayimSure;
    //        PlayerPrefs.SetFloat("geriSayimSure", geriSayimSure);
    //        if (Timer.geriSayimSure <= 2)
    //        {
    //            Timer.Finnish();
    //        }
    //    }
    //}

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
                    PlayerPrefs.SetFloat("TransactionResult", TransactionResult);
                    break;
                case 2:
                    Operator.text = "-";
                    TransactionResult = N1 - N2;
                    PlayerPrefs.SetFloat("TransactionResult", TransactionResult);
                    break;
                case 3:
                    Operator.text = "*";
                    TransactionResult = N1 * N2;
                    PlayerPrefs.SetFloat("TransactionResult", TransactionResult);
                    break;
                case 4:
                    Operator.text = "/";
                    TransactionResult = N1 / N2;
                    TransactionResult.ToString("0.00");
                    float.Parse(TransactionResult.ToString("0.00"));
                    PlayerPrefs.SetFloat("TransactionResult", TransactionResult);
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
                    float.Parse(TransactionResult.ToString("0.00"));
                    break;
            }
            FirstNumber.text = N1.ToString();
            SecondNumber.text = N2.ToString();
        }
        if (TransactionResult < 10)
        {
            Question();
        }
        if (TransactionResult > 50)
        {
            Question();
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
        Dogru = PlayerPrefs.GetInt("Dogru");
        Dogru = Dogru / 2;
        Yanlis = PlayerPrefs.GetInt("Yanlis");
        Yanlis = Yanlis / 2;
        Bos = PlayerPrefs.GetInt("Bos");
        Bos=Bos / 2;
        //if (Yanlis==0)
        //{
        //    Yanlis = 1;
        //}
        DogruSayisi.text = Dogru.ToString();
        YanlisSayisi.text = Yanlis.ToString();
        BosSayisi.text = Bos.ToString();
    }

}
