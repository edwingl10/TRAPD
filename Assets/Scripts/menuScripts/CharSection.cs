using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSection : MonoBehaviour
{
    public GameObject[] buyButtons;
    public GameObject[] ownedLabels;
    public HomeManager homeMan;
    public store Store;
    public characterSelect charSelect;


    public void DisplayButtons()
    {
        for(int i=0; i<buyButtons.Length; i++)
        {
            bool unlocked = (bool)homeMan.playerInfo[i+1]["unlocked"];

            buyButtons[i].SetActive(!unlocked);
            ownedLabels[i].SetActive(unlocked);
            if (!unlocked)
            {
                buyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ((int)homeMan.playerInfo[i + 1]["price"]).ToString();
            }
        }
    }

    public void purchaseCharacter(int index)
    {
        if(homeMan.totalCoins >= (int)homeMan.playerInfo[index]["price"])
        {
            homeMan.totalCoins -= (int)homeMan.playerInfo[index]["price"];
            homeMan.playerInfo[index]["unlocked"] = true;
            homeMan.SavePlayerData();
            homeMan.SaveCoinData();

            Store.UpdateCoins();
            DisplayButtons();
            charSelect.DisplayCharacters();
        }
        else
        {
            Store.ShowSelectedPanel(2);
        }
    }

}
