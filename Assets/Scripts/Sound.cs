using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public UnityEngine.UI.Button soundToggleButton;
    public Sprite soundOnSprite;   // Ses açýk simgesi
    public Sprite soundOffSprite;  // Ses kapalý simgesi
    private static bool isSoundOn = true;

    private void Start()
    {
        // Ses durumuna göre sesi baþlat veya duraklat
        if (isSoundOn)
            PlaySound();
        else
            PauseSound();

        // Ses kontrol butonunun baþlangýç durumunu ayarla
        UpdateSoundToggleButtonImage();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        if (isSoundOn)
            PlaySound();
        else
            PauseSound();

        // Ses kontrol butonunun görünümünü güncelle
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
    {// Ses durumuna göre ses kontrol butonunun görünümünü deðiþtir
        if (isSoundOn)
        {
            // Ses açýk simgesini göster
            soundToggleButton.image.sprite = soundOnSprite;
        }
        else
        {
            // Ses kapalý simgesini göster
            soundToggleButton.image.sprite = soundOffSprite;
        }
    }
}
