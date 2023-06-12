using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIndicator : MonoBehaviour
{

    public Slider slider;
    public Transform araba;
    private Vector3 baslangicPozisyonu;
    public Transform bitisNoktasi;
    

    private void Start()
    {
        baslangicPozisyonu = araba.position;
    }

    private void Update()
    {
        float tamamlanacakMesafe = bitisNoktasi.transform.position.z;
        // Arabanýn geçtiði mesafeyi hesapla ve Slider'ýn deðerini güncelle
        float mesafe = araba.position.z - baslangicPozisyonu.z;
        float tamamlanmaOrani = mesafe / tamamlanacakMesafe;
        slider.value = tamamlanmaOrani;
    }
}
