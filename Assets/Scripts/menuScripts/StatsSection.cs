using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsSection : MonoBehaviour
{
    public HomeManager homeMan;
    public Transform[] ranks;

    public void DisplayScores()
    {
        for(int i=0; i<ranks.Length; i++)
        {
            ranks[i].Find("name text").GetComponent<TextMeshProUGUI>().text = (string)homeMan.playerInfo[i]["name"];
            ranks[i].Find("highscore text").GetComponent<TextMeshProUGUI>().text = ((int)homeMan.playerInfo[i]["highscore"]).ToString();
        }
    }

    public void CloseScoresPanel()
    {
        gameObject.SetActive(false);
    }
}
