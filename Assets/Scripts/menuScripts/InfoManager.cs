using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public GameObject canonsPanel;
    public GameObject obstaclesPanel;
    public GameObject coinsPanel;
    public SoundManager soundMan;

    public void HideInfoPanel()
    {
        soundMan.Play("Exit");
        gameObject.SetActive(false);
    }

    public void HideSubPanel(string pan)
    {
        soundMan.Play("Exit");
        switch (pan)
        {
            case "canons":
                canonsPanel.SetActive(false);
                break;
            case "obstacles":
                obstaclesPanel.SetActive(false);
                break;
            case "coins":
                coinsPanel.SetActive(false);
                break;
        }
    }

    public void ShowSubPanel(string pan)
    {
        soundMan.Play("SubButtons");
        switch (pan)
        {
            case "canons":
                canonsPanel.SetActive(true);
                break;
            case "obstacles":
                obstaclesPanel.SetActive(true);
                break;
            case "coins":
                coinsPanel.SetActive(true);
                break;
        }
    }

    public void PrivacyButton()
    {
        Application.OpenURL("https://unity3d.com/legal/privacy-policy?_ga=2.152144895.798471311.1571806927-883960919.1570470741");
    }

    public void RateButton()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + Application.productName);
#endif
    }
}
