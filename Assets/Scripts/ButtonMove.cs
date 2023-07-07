using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonMove : MonoBehaviour
{
    public Car car;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RightButton()
    {
        transform.Rotate(0, 200f * Time.fixedDeltaTime, 0);

    Vector3 movement = transform.forward * car.moveSpeed * Time.fixedDeltaTime;
    car.transform.Translate(movement, Space.World);
    }
    public void LeftButton()
    {
        car.transform.Translate(Vector3.left * 85f * Time.deltaTime);
    }
}
