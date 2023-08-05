using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    public Vector3 PuzzlePosition;
    public GameObject PuzzleCube;
    public Car car;
    void Start()
    {
        float savedZPosition = PlayerPrefs.GetFloat("KüpZPosition", transform.position.z);
        Vector3 newPosition = transform.position;
        newPosition.z = savedZPosition;
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "puzzle")
        {
            float geriSayimSure = PlayerPrefs.GetFloat("geriSayimSure");
            Vector3 newPosition = other.transform.position;
                    newPosition.z += car.moveSpeed * (5 - (geriSayimSure - 1));
                    other.transform.position = newPosition;

                   PlayerPrefs.SetFloat("KüpZPosition", newPosition.z);
                   SceneManager.LoadScene(2);;
        }
    }
    void OnApplicationQuit()
    {
        PuzzlePosition = new Vector3((float)-66.985, (float)1.507, (float)-268.3);
        PlayerPrefs.SetFloat("KüpZPosition", PuzzlePosition.z);
    }
}
