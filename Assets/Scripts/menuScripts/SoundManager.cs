using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    private bool soundEnabled;

    private void Awake()
    {
        try
        {
            soundData data = saveSystem.LoadSoundPref();
            soundEnabled = data.sound;
        }catch(Exception e)
        {
            soundEnabled = true;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.mute = !soundEnabled;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void ToggleSounds(bool sound)
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = !sound;
        }
    }
}
