using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public GameObject characterSelectPanel;
    public GameObject mainMenuPanel;
    public GameObject storePanel;
    public GameObject highscorePanel;
    public GameObject infoPanel;

    public int totalCoins;

    public int index; 

    public GameObject playerObject;
    public Animator transition;

    public gameData gdata;
    public Hashtable[] playerInfo;
    public SoundManager soundMan;

    public GameObject adErrorPanel;

    private bool music;

    void Start()
    {
        try
        {
            soundData data = saveSystem.LoadSoundPref();
            music = data.music;
        }catch(Exception e)
        {
            music = true;
        }

        if(music)
            StartCoroutine(AudioController.FadeIn(GetComponent<AudioSource>(), 1f));

        showSettings = false;
        //soundEnabled = true;
        //musicEnabled = true;
        StartCoroutine(startSunlightAnimation());
        LoadData();
        LoadPLayerData();
        //save both information
        playerObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.GetCharacterSprite(index);
        playerObject.GetComponent<Animator>().runtimeAnimatorController = GameAssets.i.controllers[index];
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
        soundMan.Play("SubButtons");
        mainMenuPanel.SetActive(false);
        playerObject.GetComponent<playerMovement>().MoveRight();
        StartCoroutine(TransitionToGameScene());
    }

    IEnumerator TransitionToGameScene()
    {
        yield return new WaitForSeconds(4.2f);
        playerObject.GetComponent<playerMovement>().Jump();
        yield return new WaitForSeconds(2f);
        StartCoroutine(AudioController.FadeOut(GetComponent<AudioSource>(), 1f));
        transition.Play("sceneOutro");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("BoxScene");
    }

    public void SettingsButton()
    {
        soundMan.Play("SubButtons");
        showSettings = !showSettings;
        settings.SetBool("showSettings", showSettings);
    }


    public void ShowCharacterSelectionScreen()
    {
        soundMan.Play("SubButtons");
        characterSelectPanel.SetActive(true);
        characterSelectPanel.GetComponent<characterSelect>().DisplayCharacters();
    }
    public void HideCharacterSelectionScreen()
    {
        soundMan.Play("Exit");
        characterSelectPanel.SetActive(false);
    }

    public void ShowStoreScreen()
    {
        soundMan.Play("SubButtons");
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
    public void ShowHighScorePanel()
    {
        soundMan.Play("SubButtons");
        highscorePanel.SetActive(true);
        highscorePanel.GetComponent<StatsSection>().DisplayScores();
    }

    public void HideAdErrorPanel()
    {
        adErrorPanel.SetActive(false);
    }

    public void ShowInfoPanel()
    {
        soundMan.Play("SubButtons");
        infoPanel.SetActive(true);
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("adHp", 0);
    }
}
