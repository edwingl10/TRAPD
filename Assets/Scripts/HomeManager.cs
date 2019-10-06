using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    //background lights
    public Animator llight;
    public Animator mlight;
    public Animator rlight;

    //settings
    public Animator settings;
    private bool showSettings;
    public GameObject soundIcon;
    private bool soundEnabled;
    public GameObject musicIcon;
    private bool musicEnabled;

    void Start()
    {
        showSettings = false;
        soundEnabled = true;
        musicEnabled = true;
        StartCoroutine(startSunlightAnimation());
    }

    IEnumerator startSunlightAnimation()
    {
        yield return new WaitForSeconds(5);
        llight.Play("sunlight");
        yield return new WaitForSeconds(0.3f);
        mlight.Play("sunlight");
        yield return new WaitForSeconds(0.3f);
        rlight.Play("sunlight");
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("BoxScene");
    }

    public void SettingsButton()
    {
        showSettings = !showSettings;
        settings.SetBool("showSettings", showSettings);
    }

    public void SoundButton()
    {
        soundEnabled = !soundEnabled;
        if (soundEnabled)
        {
            soundIcon.GetComponent<Image>().color = new Color32(255,216,179,255);
        }
        else
        {
            soundIcon.GetComponent<Image>().color = new Color32(168, 117, 68, 255);
        }
        
    }

    public void MusicButton()
    {
        musicEnabled = !musicEnabled;
        if (musicEnabled)
        {
            musicIcon.GetComponent<Image>().color = new Color32(255,216,179,255);
        }
        else
        {
            musicIcon.GetComponent<Image>().color = new Color32(168, 117, 68, 255);

        }
    }
}
