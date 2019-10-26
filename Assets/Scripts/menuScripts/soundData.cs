using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class soundData 
{
    public bool music;
    public bool sound;

    public soundData(bool musicPref, bool soundPref)
    {
        music = musicPref;
        sound = soundPref;
    }
}
