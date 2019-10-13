using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HomeManager : MonoBehaviour
{
    //background lights
    public Animator llight;
    public Animator mlight;
    public Animator rlight;

    public TextMeshProUGUI coinsText;

    //settings
    public Animator settings;
    private bool showSettings;
    public GameObject soundIcon;
    private bool soundEnabled = true; 
    public GameObject musicIcon;
    private bool musicEnabled=true;

    public GameObject characterSelectPanel;
    public GameObject mainMenuPanel;

    private int totalCoins;

    public Animator player;
    private int index;

    public GameObject playerObject;
    public Animator transition;

    void Start()
    {
        showSettings = false;
        //soundEnabled = true;
        //musicEnabled = true;
        StartCoroutine(startSunlightAnimation());
        LoadData();
        LoadPLayerData();
        player.runtimeAnimatorController = GameAssets.i.controllers[index];
    }

    public void LoadData()
    {
        try
        {
            gameData data = saveSystem.LoadGameData();
            totalCoins = data.totalCoins;
        }
        catch (Exception e)
        {
           totalCoins =0;
        }
        coinsText.text = totalCoins.ToString();
        
    }

    private void LoadPLayerData()
    {
        try
        {
            playerData data = saveSystem.LoadCharacterInfo();
            index = data.playerID;
        }
        catch (Exception e)
        {
            index = 0;
        }
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
        mainMenuPanel.SetActive(false);
        player.GetComponent<playerMovement>().MoveRight();
        StartCoroutine(TransitionToGameScene());
        //SceneManager.LoadScene("BoxScene");
        //transition.SetBool("outro", true);
    }

    IEnumerator TransitionToGameScene()
    {
        yield return new WaitForSeconds(4.2f);
        player.GetComponent<playerMovement>().Jump();
        yield return new WaitForSeconds(2f);
        transition.Play("sceneOutro");
        yield return new WaitForSeconds(1.2f);
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

    public void ShowCharacterSelectionScreen()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }
    public void HideCharacterSelectionScreen()
    {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
}
