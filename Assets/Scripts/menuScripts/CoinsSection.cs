using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class CoinsSection : MonoBehaviour
{
    public HomeManager homeMan;
    public store Store;
    public SoundManager soundMan;

    private string gameId = "3337100";
    public string myPlacementId = "rewardedVideo";
    public Button adButton;
    public GameObject errorPanel;
    public GameObject successPanel;

    public void Start()
    {
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, false);
        }
    }
    private void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(myPlacementId);
        }
    }


    private void AdCoins(int amount)
    {
        homeMan.totalCoins += amount;
        homeMan.SaveCoinData();
        Store.UpdateCoins();
    }

    public void HideErrorPanel()
    {
        errorPanel.SetActive(false);
    }

    public void HideSucessPanel()
    {
        soundMan.Play("CoinReward");
        AdCoins(100);
        successPanel.SetActive(false);
    }
}
