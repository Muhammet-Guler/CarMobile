using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCarLocation()
    {
        PlayerPrefs.SetFloat("CarX", transform.position.x);
        PlayerPrefs.SetFloat("CarY", transform.position.y);
        PlayerPrefs.SetFloat("CarZ", transform.position.z);
        PlayerPrefs.Save();
    }
    public void LoadCarLocation()
    {
        float CarX = PlayerPrefs.GetFloat("CarX");
        float CarY = PlayerPrefs.GetFloat("CarY");
        float CarZ = PlayerPrefs.GetFloat("CarZ");
        transform.position = new Vector3(CarX, CarY, CarZ);
    }
}
