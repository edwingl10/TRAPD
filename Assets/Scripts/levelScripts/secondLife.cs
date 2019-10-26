using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class secondLife : MonoBehaviour
{
    private int counter;
    public TextMeshProUGUI counterText;
    public levelManager levelMan;
    public GameObject secondLifePanel;
    public GameObject errorPanel;
    public TextMeshProUGUI coinsText;

    public GameObject insufficientFunds;

    public Player player;

    private void Start()
    {
        counter = 5;
        coinsText.text = levelMan.gdata.totalCoins.ToString();
    }

    public void ChangeCounter()
    {
        counter--;
        counterText.text = "Revive " + counter.ToString() +"s";
    }

    public void ShowGameOverPanel()
    {
        secondLifePanel.SetActive(false);
        levelMan.ShowGameOverFromOther();
    }

    public void PayCoins(int price)
    {
        if(levelMan.gdata.totalCoins >= price)
        {
            levelMan.gdata.totalCoins -= price;
            levelMan.UpdateCoins();
            player.RestoreLifeWrapper();
        }
        else
        {
            insufficientFunds.SetActive(true);
        }
    }


    public void HideErrorPanel()
    {
        errorPanel.SetActive(false);
        ShowGameOverPanel();
    }

    public void AdReward()
    {
        levelMan.reviveAd = true;
        player.RestoreLifeWrapper();
    }
}
