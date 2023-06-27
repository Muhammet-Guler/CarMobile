using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anaytics : MonoBehaviour
{

    public GoogleAnalyticsV4 analiytics;//import ettiðimiz dosyayý referans ettiðimiz analytics deðiþkeni
    void Start()
    {
        analiytics.StartSession();//analytics baþlatma
        analiytics.LogScreen("CarScene");//analytics baþladýðý zaman analytics sayfasýnda ne isimle gözüksün
    } //buraya sahne ismi yazýlýrsa misal kullanýcý o sahneye kaç kere girmiþ
     //gibi bilgilere eriþilebilinir

    // Update is called once per frame
    void Update()
    {
        
    }
}
