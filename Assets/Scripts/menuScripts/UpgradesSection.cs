using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesSection : MonoBehaviour
{
    public GameObject[] charButtons;
    public GameObject[] inactiveButtons;
    public GameObject upgradeButton;
    public GameObject upgradeTextLabel;

    //color for character buttons
    private Color32 defaultColor;
    private Color32 selectedColor;

    //color for power bars
    private Color32 powerDefaultColor;
    private Color32 powerupColor;

    //scroll content
    public GameObject characterImage;
    public TextMeshProUGUI characterLevelText;
    public GameObject[] levelBars;
    public TextMeshProUGUI currentLevelDesc;
    public TextMeshProUGUI nextLevelDesc;
    public TextMeshProUGUI upgradePrice;

    public HomeManager homeMan;
    private int currentIndex;

    public store Store;

    private void Start()
    {
        currentIndex = 0;
        defaultColor = new Color32(255,228,179,255);
        selectedColor = new Color32(176,130,158,255);
        powerDefaultColor = new Color32(57,72,78,255);
        powerupColor = new Color32(103,0,192,255);
        ShowCharacterStats(0);
    }

    public void DisplayCharacterButtons()
    {
        for(int i=0; i<charButtons.Length; i++)
        {
            bool unlocked = (bool)homeMan.playerInfo[i]["unlocked"];
            inactiveButtons[i].SetActive(!unlocked);
            charButtons[i].SetActive(unlocked);
        }
    }

    public void ShowCharacterStats(int index)
    {
        charButtons[currentIndex].GetComponent<Image>().color = defaultColor;
        charButtons[index].GetComponent<Image>().color = selectedColor;
        

        characterImage.GetComponent<Image>().sprite = GameAssets.i.GetCharacterSprite(index);
        characterLevelText.text = "Ability Level: " + (int)homeMan.playerInfo[index]["upgradelvl"];
        DisplayUpgradeBars(index);
        DisplayUpgradeDescriptions(index);
        upgradePrice.text = ((int)homeMan.playerInfo[index]["upgradePrice"]).ToString();

        currentIndex = index;
    }

    private void DisplayUpgradeBars(int index)
    {
        for(int i=0; i<levelBars.Length; i++)
        {
            levelBars[i].GetComponent<Image>().color = powerDefaultColor;
        }

        int power = (int)homeMan.playerInfo[index]["upgradelvl"];
        for(int i=0; i<power; i++)
        {
            levelBars[i].GetComponent<Image>().color = powerupColor;
        }
    }

    private void DisplayUpgradeDescriptions(int index)
    {
        int playerIndex = (int)homeMan.playerInfo[index]["upgradelvl"];
        currentLevelDesc.text = "Current Level:\n"+GameAssets.i.upgradeDescriptions[index][playerIndex];

        if (playerIndex == 4)
        {
            upgradeButton.SetActive(false);
            upgradeTextLabel.SetActive(false);
            nextLevelDesc.text = "Ability is fully powered up!";
        }
        else
        {
            upgradeButton.SetActive(true);
            upgradeTextLabel.SetActive(true);
            nextLevelDesc.text = "Next Level:\n"+GameAssets.i.upgradeDescriptions[index][playerIndex + 1];
        }
        
    }

    public void UpgradePurchase()
    {
        int upPrice = (int)homeMan.playerInfo[currentIndex]["upgradePrice"];

        if(homeMan.totalCoins >= upPrice)
        {
            int lvl = (int)homeMan.playerInfo[currentIndex]["upgradelvl"];

            homeMan.totalCoins -= upPrice;
            homeMan.playerInfo[currentIndex]["upgradePrice"] = upPrice + 200;
            homeMan.playerInfo[currentIndex]["upgradelvl"] = lvl + 1;

            homeMan.SavePlayerData();
            homeMan.SaveCoinData();

            Store.UpdateCoins();
            ShowCharacterStats(currentIndex);
        }
        else
        {
            Store.ShowSelectedPanel(2);
        }
        
    }

}
