using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour
{
    public enum ItemType
    {
        Pack1,
        Pack2,
    }

    public ItemType itemType;
    public TextMeshProUGUI priceText;
    //private string defaultText;



    void Start()
    {
        //defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }

    public void ClickBuy()
    {
        switch (itemType)
        {
            case ItemType.Pack1:
                IAPManager.instance.BuyPack1Gold();
                break;
            case ItemType.Pack2:
                IAPManager.instance.BuyPack2Gold();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (!IAPManager.instance.IsInitialized())
        {
            yield return null;
        }
        string loadedPrice = "";

        switch (itemType)
        {
            case ItemType.Pack1:
                loadedPrice = IAPManager.instance.GetProductPriceFromStore(IAPManager.instance.Pack1);
                break;
            case ItemType.Pack2:
                loadedPrice = IAPManager.instance.GetProductPriceFromStore(IAPManager.instance.Pack2);
                break;
        }
        //priceText.text = defaultText + " " + loadedPrice;
        priceText.text = loadedPrice;
    }


}
