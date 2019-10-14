using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class characterSelect : MonoBehaviour
{
    //public Animator player;
    public HomeManager homeman;
    public GameObject middlePanel; //parent of below object
    public GameObject middleImg;
    public GameObject leftPanel; //parent of below object
    public GameObject leftImg;
    public GameObject rightPanel; //parent of below object
    public GameObject rightImg;
    private int length;

    public int index;
    private int originalIndex;
    public Hashtable[] playerInfo;

    public TextMeshProUGUI description;
    public GameObject selectBttn;
    public GameObject storeBttn;

    private void Awake()
    {
        LoadPLayerData();
        length = GameAssets.i.characterSprites.Length;
        DisplayCharacters();
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
        originalIndex = index;
    }

    //called when chooes button is clicked
    public void HandleCharacterSelection()
    {
        saveSystem.saveCharacterInfo(this);
        homeman.player.runtimeAnimatorController = GameAssets.i.controllers[index];
        originalIndex = index;
        DisplayCharacters();
    }

    private void DisplayCharacters()
    {

        bool unlocked = (bool)playerInfo[index]["unlocked"];
        selectBttn.SetActive(unlocked);
        storeBttn.SetActive(!unlocked);

        SetPanelColors(middlePanel, index);
        middleImg.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index);
        description.text = GameAssets.i.characterDescription[index];

        SetPanelColors(leftPanel, HandleIndex(index-1));
        leftImg.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index-1);
        SetPanelColors(rightPanel, HandleIndex(index + 1));
        rightImg.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index + 1);
    }

    private void SetPanelColors(GameObject panel, int slot)
    {
        if(slot == originalIndex)
        {
            panel.GetComponent<Image>().color = new Color32(102,209,182,255);
        }
        else if ((bool)playerInfo[slot]["unlocked"])
        {
            panel.GetComponent<Image>().color = Color.white;
        }
        else
        {
            panel.GetComponent<Image>().color = new Color32(217,181,177,255);
        }
    }

    public void NextButton()
    {
        index++;
        index = HandleIndex(index);
        /**
        if (index >= length)
        {
            index %= length;
        }**/
        DisplayCharacters();
    }

    public void PrevButton()
    {
        index--;
        index = HandleIndex(index);
        /**
        if(index < 0)
        {
            index = length - (Mathf.Abs(index) % length);
        }**/
        DisplayCharacters();
    }

    private int HandleIndex(int i)
    {
        if(i >= length)
        {
            return i % length;
        }
        else if(i < 0)
        {
            return length - (Mathf.Abs(i) % length);
        }
        else
        {
            return i;
        }
    }
}
