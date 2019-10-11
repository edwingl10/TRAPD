using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gameData
{
    public int totalCoins;

    public gameData(levelManager levelman)
    {
        totalCoins = levelman.coinsValue;
    }


}
