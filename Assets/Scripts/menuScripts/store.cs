using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    public HomeManager homeMan;
    public TextMeshProUGUI coinsText;
    private int currentIndex;


    public GameObject[] storePanels;
    public Button[] storeButtons;

    private Color32 defaultColor;
    private Color32 selectedColor;

    public CharSection charSelectionPanel;
    public UpgradesSection upgradePanel;

    private void Start()
    {
        currentIndex = 0;
        coinsText.text = homeMan.totalCoins.ToString();
        defaultColor = new Color32(255,222,187,255);
        selectedColor = new Color32(99,158,241, 255);
        storeButtons[currentIndex].GetComponent<Image>().color = selectedColor;
        charSelectionPanel.DisplayButtons();
    }
    public void CloseStorePanel()
    {
        gameObject.SetActive(false);
    }

    public void ShowSelectedPanel(int index)
    {
        if (index == 1)
        {
            upgradePanel.DisplayCharacterButtons();
        }
        storePanels[currentIndex].SetActive(false);
        storeButtons[currentIndex].GetComponent<Image>().color = defaultColor;

        storePanels[index].SetActive(true);
        storeButtons[index].GetComponent<Image>().color = selectedColor;

        currentIndex = index;
    }

    public void UpdateCoins()
    {
        coinsText.text = homeMan.totalCoins.ToString();
    }
}
