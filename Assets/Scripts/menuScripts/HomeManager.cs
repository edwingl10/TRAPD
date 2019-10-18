﻿using System.Collections;
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
    public GameObject storePanel;

    public int totalCoins;

    public Animator player;
    public int index; 

    public GameObject playerObject;
    public Animator transition;

    public gameData gdata;
    public Hashtable[] playerInfo;

    void Start()
    {
        showSettings = false;
        //soundEnabled = true;
        //musicEnabled = true;
        StartCoroutine(startSunlightAnimation());
        LoadData();
        LoadPLayerData();
        //save both information
        player.runtimeAnimatorController = GameAssets.i.controllers[index];
    }

    public void LoadData()
    {
        try
        {
            gdata = saveSystem.LoadGameData();
            totalCoins = gdata.totalCoins;
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
            playerInfo = data.playerInfo;
        }
        catch (Exception e)
        {
            index = 0;
            playerInfo = GameAssets.i.playerInfo;
        }
    }
    
    public void SavePlayerData()
    {
        saveSystem.saveCharacterInfo(this);
    }
    public void SaveCoinData()
    {
        saveSystem.saveCoinInfo(this);
        coinsText.text = totalCoins.ToString();
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
        characterSelectPanel.SetActive(true);
    }
    public void HideCharacterSelectionScreen()
    {
        characterSelectPanel.SetActive(false);
    }

    public void ShowStoreScreen()
    {
        storePanel.SetActive(true);

    }

    public void MoreCoinsButton()
    {
        ShowStoreScreen();
        StartCoroutine(showCoinsPanel());
    }
    private IEnumerator showCoinsPanel()
    {
        yield return new WaitForSeconds(0.1f);
        storePanel.GetComponent<store>().ShowSelectedPanel(2);
    }
}
