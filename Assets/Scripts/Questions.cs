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
    decimal N1, N2, TransactionResult;
    public float Speed=0f;
    public Car car;
    public float moveSpeed;
    public int deger;
    // Start is called before the first frame update
    void Start()
    {

        Question();
        //Btn1.GetComponentInChildren<Text>().text =Random.Range(1, 10).ToString();
        //int.Parse(Btn1.GetComponentInChildren<Text>().text);

        //Btn2.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString();
        //int.Parse(Btn2.GetComponentInChildren<Text>().text);
        deger = PlayerPrefs.GetInt("deger");
        if (deger == 0)
        {
            Btn1.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString();
            decimal.Parse(Btn1.GetComponentInChildren<Text>().text);
            if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) > 5)
            {
                Btn1.GetComponentInChildren<Text>().text = TransactionResult.ToString();
                decimal.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn1.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString();
                decimal.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            Btn2.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString();
            decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
            {
                Btn2.GetComponentInChildren<Text>().text = TransactionResult.ToString();
                decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn2.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString();
                decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) == decimal.Parse(Btn2.GetComponentInChildren<Text>().text))
            {
                Btn2.GetComponentInChildren<Text>().text = Random.Range(10, 15).ToString();
                decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
        }


        if (deger == 1)
        {
            Btn1.GetComponentInChildren<Text>().text = Random.Range(10, 300).ToString();
            decimal.Parse(Btn1.GetComponentInChildren<Text>().text);
            if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) > 150)
            {
                Btn1.GetComponentInChildren<Text>().text = TransactionResult.ToString();
                decimal.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn1.GetComponentInChildren<Text>().text = Random.Range(10, 300).ToString();
                double.Parse(Btn1.GetComponentInChildren<Text>().text);
            }
            Btn2.GetComponentInChildren<Text>().text = Random.Range(10, 300).ToString();
            decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
            {
                Btn2.GetComponentInChildren<Text>().text = TransactionResult.ToString();
                decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            else
            {
                Btn2.GetComponentInChildren<Text>().text = Random.Range(10, 300).ToString();
                decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
            if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) == decimal.Parse(Btn2.GetComponentInChildren<Text>().text))
            {
                Btn2.GetComponentInChildren<Text>().text = Random.Range(100, 550).ToString();
                decimal.Parse(Btn2.GetComponentInChildren<Text>().text);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void GoBack()
    {
        SceneManager.LoadScene(1);
    }
    public void AnswerControl1()
    {
        if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) == TransactionResult)
        {
            
            Conclusion.text = "Doðru";
            Btn1.GetComponent<Image>().color = Color.green;
            moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            SceneManager.LoadScene(1);
        
        }
        if (decimal.Parse(Btn1.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanlýþ";
            Btn1.GetComponent<Image>().color = Color.red;
            SceneManager.LoadScene(1);
        }
        
    }
    public void AnswerControl2()
    {
        if (decimal.Parse(Btn2.GetComponentInChildren<Text>().text) == TransactionResult)
        {
            Conclusion.text = "Doðru";
            Btn2.GetComponent<Image>().color = Color.green;
            moveSpeed = PlayerPrefs.GetFloat("ArabaninHizi");
            moveSpeed += 5f;
            PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
            SceneManager.LoadScene(1);

        }
        if (decimal.Parse(Btn2.GetComponentInChildren<Text>().text) != TransactionResult)
        {
            Conclusion.text = "Yanlýþ";
            Btn2.GetComponent<Image>().color = Color.red;
            SceneManager.LoadScene(1);
        }
    }
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
                    break;
            }
            FirstNumber.text = N1 + "";
            SecondNumber.text = N2 + "";
        }
        //ConclusionBtn.text = "";
        
    }
    void OnApplicationQuit()
    {
        moveSpeed = 5f;
        PlayerPrefs.SetFloat("ArabaninHizi", moveSpeed);
    }
}
