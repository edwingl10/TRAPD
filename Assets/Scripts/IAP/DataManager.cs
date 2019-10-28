using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public HomeManager homeMan;
    public store Store;
    public SoundManager soundMan;

    public void PurchaseCoins(int amount)
    {
        soundMan.Play("CoinReward");
        homeMan.totalCoins += amount;
        homeMan.SaveCoinData();
        Store.UpdateCoins();
    }
}
