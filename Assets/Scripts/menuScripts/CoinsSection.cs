using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSection : MonoBehaviour
{
    public HomeManager homeMan;
    public store Store;


    public void PurchaseCoins(int amount)
    {
        homeMan.totalCoins += amount;
        homeMan.SaveCoinData();
        Store.UpdateCoins();
    }

}
