using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public UnityEngine.UI.Button soundToggleButton;
    public Sprite soundOnSprite;   // Ses a��k simgesi
    public Sprite soundOffSprite;  // Ses kapal� simgesi
    private static bool isSoundOn = true;

    private void Start()
    {
        // Ses durumuna g�re sesi ba�lat veya duraklat
        if (isSoundOn)
            PlaySound();
        else
            PauseSound();

        // Ses kontrol butonunun ba�lang�� durumunu ayarla
        UpdateSoundToggleButtonImage();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        if (isSoundOn)
            PlaySound();
        else
            PauseSound();

        // Ses kontrol butonunun g�r�n�m�n� g�ncelle
        UpdateSoundToggleButtonImage();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    private void UpdateSoundToggleButtonImage()
    {// Ses durumuna g�re ses kontrol butonunun g�r�n�m�n� de�i�tir
        if (isSoundOn)
        {
            // Ses a��k simgesini g�ster
            soundToggleButton.image.sprite = soundOnSprite;
        }
        else
        {
            // Ses kapal� simgesini g�ster
            soundToggleButton.image.sprite = soundOffSprite;
        }
    }
}
