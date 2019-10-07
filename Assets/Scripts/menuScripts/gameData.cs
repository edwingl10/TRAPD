using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains information such as total coins, high score for different players
[System.Serializable]
public class gameData
{
    public int totalCoins;
    public int highScore;

    public gameData(levelManager levelman)
    {
        totalCoins = levelman.coinsValue;

        /**
        if (levelman.scoreValue > highScore)
        {
            highScore = levelman.scoreValue;
        }
        //highScore = levelman.scoreValue; **/
    }


}
