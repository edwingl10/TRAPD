using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterSelect : MonoBehaviour
{
    //public Animator player;
    public HomeManager homeman;
    public GameObject middleImg;
    public GameObject leftImg;
    public GameObject rightImg;
    private int length;

    public int index;

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
        }
        catch (Exception e)
        {
            index = 0;
        }
    }

    public void HandleCharacterSelection()
    {
        saveSystem.saveCharacterInfo(this);
        homeman.player.runtimeAnimatorController = GameAssets.i.controllers[index];
    }

    private void DisplayCharacters()
    {
        middleImg.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index);
        leftImg.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index-1);
        rightImg.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index + 1);
    }

    public void NextButton()
    {
        index++;
        if (index >= length)
        {
            index %= length;
        }
        DisplayCharacters();
    }

    public void PrevButton()
    {
        index--;
        if(index < 0)
        {
            index = length - (Mathf.Abs(index) % length);
        }
        DisplayCharacters();
    }

}
