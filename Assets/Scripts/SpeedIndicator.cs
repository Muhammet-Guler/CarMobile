using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    public Car car;
    private const float MIN_SPEED_ANG = 195.0f;
    private const float MAX_SPEED_ANG = -85.0f;

    private Transform ibreTransform;

    public float speed; //anl�k h�z
    public float topSpeed; //max h�z

    public GameObject ibre;

    private void Awake()
    {
        ibreTransform = ibre.transform;
    }
    //ibremizi arab�m�z�n ald��� h�za g�re ibesini a�� vererek d�nd�r�yoruz.
    private void FixedUpdate()
    {

        speed = car.moveSpeed*2;

        if (speed > topSpeed) speed = topSpeed;

        ibreTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
        if (car.stop==true)
        {
            speed = 0f;
        }
    }

    private float GetSpeedRotation()
    {
        float toplamDonusAc�s� = MIN_SPEED_ANG - MAX_SPEED_ANG;  //toplamDonusAc�s� = 195.0 - (-85.0)
        float speedNormalized = speed / topSpeed; //speedNormalized = 1 / 360

        return MIN_SPEED_ANG - speedNormalized * toplamDonusAc�s�; // return de�eri = 195 - (1 / 360) * 270
    }
}
