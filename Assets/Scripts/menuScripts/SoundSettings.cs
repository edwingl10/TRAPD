using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SoundSettings : MonoBehaviour
{
    public SoundManager soundMan;
    public AudioSource musicSource;

    public GameObject musicIcon;
    public GameObject soundIcon;

    private bool soundEnabled;
    private bool musicEnabled;


    private void Awake()
    {

        try
        {
            soundData data = saveSystem.LoadSoundPref();
            musicEnabled = data.music;
            soundEnabled = data.sound;
        }
        catch (Exception e)
        {
            musicEnabled = true;
            soundEnabled = true;
        }

        if (!musicEnabled)
            musicIcon.GetComponent<Image>().color = new Color32(168, 117, 68, 255);
        
        if(!soundEnabled)
            soundIcon.GetComponent<Image>().color = new Color32(168, 117, 68, 255);
    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        if (soundEnabled)
        {
            soundIcon.GetComponent<Image>().color = Color.white;
        }
        else
        {
            soundIcon.GetComponent<Image>().color = new Color32(168, 117, 68, 255);
        }
        soundMan.ToggleSounds(soundEnabled);
        saveSystem.SaveSoundPref(musicEnabled, soundEnabled);
    }

    public void ToggleMusicFromGame()
    {
        musicEnabled = !musicEnabled;
        if (musicEnabled)
        {
            musicSource.Play();
            musicIcon.GetComponent<Image>().color = Color.white;
        }
        else
        {
            musicSource.Stop();
            musicIcon.GetComponent<Image>().color = new Color32(168, 117, 68, 255);
        }
        saveSystem.SaveSoundPref(musicEnabled, soundEnabled);
    }

}
