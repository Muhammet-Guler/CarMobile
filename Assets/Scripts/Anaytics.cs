using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anaytics : MonoBehaviour
{

    public GoogleAnalyticsV4 analiytics;//import etti�imiz dosyay� referans etti�imiz analytics de�i�keni
    void Start()
    {
        analiytics.StartSession();//analytics ba�latma
        analiytics.LogScreen("CarScene");//analytics ba�lad��� zaman analytics sayfas�nda ne isimle g�z�ks�n
    } //buraya sahne ismi yaz�l�rsa misal kullan�c� o sahneye ka� kere girmi�
     //gibi bilgilere eri�ilebilinir

    // Update is called once per frame
    void Update()
    {
        
    }
}
