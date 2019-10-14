using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }

    }

    public Transform pfDamagePopup;
    public RuntimeAnimatorController[] controllers;
    public Sprite[] characterSprites;
    public string[] characterDescription;


    public Sprite GetCharacterSprite(int index)
    {
        int len = characterSprites.Length;

        if(index < 0 )
        {
            return characterSprites[len - (Mathf.Abs(index)%len)];
        }
        else if (index >= len)
        {
            return characterSprites[index % len];
        }
        return characterSprites[index];
    }

    private Hashtable dotzInfo = new Hashtable();
    private Hashtable menderInfo = new Hashtable();
    private Hashtable cocoInfo = new Hashtable();
    private Hashtable verglasInfo = new Hashtable();
    private Hashtable dartInfo = new Hashtable();
    private Hashtable quashInfo = new Hashtable();

    public Hashtable[] playerInfo = new Hashtable[6];

    private void Awake()
    {
        dotzInfo.Add("id",0);
        dotzInfo.Add("name","Dotz");
        dotzInfo.Add("unlocked", true);
        dotzInfo.Add("upgradelvl", 0);
        dotzInfo.Add("price", 0);
        dotzInfo.Add("upgradePrice", 500);
        dotzInfo.Add("highscore", 0);

        menderInfo.Add("id", 1);
        menderInfo.Add("name", "Mender");
        menderInfo.Add("unlocked", true);
        menderInfo.Add("upgradelvl", 0);
        menderInfo.Add("price", 10000);
        menderInfo.Add("upgradePrice", 1000);
        menderInfo.Add("highscore", 0);

        cocoInfo.Add("id", 2);
        cocoInfo.Add("name","Coco");
        cocoInfo.Add("unlocked", false);
        cocoInfo.Add("upgradelvl", 0);
        cocoInfo.Add("price", 5000);
        cocoInfo.Add("upgradePrice", 500);
        cocoInfo.Add("highscore", 0);

        verglasInfo.Add("id", 3);
        verglasInfo.Add("name", "Verglas");
        verglasInfo.Add("unlocked", false);
        verglasInfo.Add("upgradelvl", 0);
        verglasInfo.Add("price", 10000);
        verglasInfo.Add("upgradePrice", 1000);
        verglasInfo.Add("highscore", 0);

        dartInfo.Add("id", 4);
        dartInfo.Add("name", "Dart");
        dartInfo.Add("unlocked", false);
        dartInfo.Add("upgradelvl", 0);
        dartInfo.Add("price", 5000);
        dartInfo.Add("upgradePrice", 500);
        dartInfo.Add("highscore", 0);

        quashInfo.Add("id", 5);
        quashInfo.Add("name", "Quash");
        quashInfo.Add("unlocked", false);
        quashInfo.Add("upgradelvl", 0);
        quashInfo.Add("price", 5000);
        quashInfo.Add("upgradePrice", 500);
        quashInfo.Add("highscore", 0);

        playerInfo[0] = dotzInfo;
        playerInfo[1] = menderInfo;
        playerInfo[2] = cocoInfo;
        playerInfo[3] = verglasInfo;
        playerInfo[4] = dartInfo;
        playerInfo[5] = quashInfo;

    }
}
