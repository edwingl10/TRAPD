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

    public string[] dotzUpgradeDesc = new string[5];
    public string[] menderUpgradeDesc = new string[5];
    public string[] cocoUpgradeDesc = new string[5];
    public string[] verglasUpgradeDesc = new string[5];
    public string[] dartUpgradeDesc = new string[5];
    public string[] quashUpgradeDesc = new string[5];

    public string[][] upgradeDescriptions = new string[6][];
    

    private void Awake()
    {
        dotzInfo.Add("id",0);
        dotzInfo.Add("name","Dotz");
        dotzInfo.Add("unlocked", true);
        dotzInfo.Add("upgradelvl", 0);
        dotzInfo.Add("price", 0);
        dotzInfo.Add("upgradePrice", 800);
        dotzInfo.Add("highscore", 0);

        menderInfo.Add("id", 1);
        menderInfo.Add("name", "Mender");
        menderInfo.Add("unlocked", false);
        menderInfo.Add("upgradelvl", 0);
        menderInfo.Add("price", 15000);
        menderInfo.Add("upgradePrice", 850);
        menderInfo.Add("highscore", 0);

        cocoInfo.Add("id", 2);
        cocoInfo.Add("name","Coco");
        cocoInfo.Add("unlocked", false);
        cocoInfo.Add("upgradelvl", 0);
        cocoInfo.Add("price", 8000);
        cocoInfo.Add("upgradePrice", 300);
        cocoInfo.Add("highscore", 0);

        verglasInfo.Add("id", 3);
        verglasInfo.Add("name", "Verglas");
        verglasInfo.Add("unlocked", false);
        verglasInfo.Add("upgradelvl", 0);
        verglasInfo.Add("price", 13000);
        verglasInfo.Add("upgradePrice", 750);
        verglasInfo.Add("highscore", 0);

        dartInfo.Add("id", 4);
        dartInfo.Add("name", "Dart");
        dartInfo.Add("unlocked", false);
        dartInfo.Add("upgradelvl", 0);
        dartInfo.Add("price", 10500);
        dartInfo.Add("upgradePrice", 400);
        dartInfo.Add("highscore", 0);

        quashInfo.Add("id", 5);
        quashInfo.Add("name", "Quash");
        quashInfo.Add("unlocked", false);
        quashInfo.Add("upgradelvl", 0);
        quashInfo.Add("price", 11000);
        quashInfo.Add("upgradePrice", 500);
        quashInfo.Add("highscore", 0);

        playerInfo[0] = dotzInfo;
        playerInfo[1] = menderInfo;
        playerInfo[2] = cocoInfo;
        playerInfo[3] = verglasInfo;
        playerInfo[4] = dartInfo;
        playerInfo[5] = quashInfo;

        SetDotzUpgradeDesc();
        SetMenderUpgradeDesc();
        SetCocoUpgradeDesc();
        SetVerglasUpgradeDesc();
        SetDartUpgradeDesc();
        SetQuashUpgradeDesc();

        SetUpgradeDescriptions();
    }

    private void SetDotzUpgradeDesc()
    {
        dotzUpgradeDesc[0] = "Duration = 5 sec";
        dotzUpgradeDesc[1] = "Duration = 8 sec";
        dotzUpgradeDesc[2] = "Duration = 10 sec";
        dotzUpgradeDesc[3] = "Duration = 12 sec";
        dotzUpgradeDesc[4] = "Duration = 15 sec";
    }

    private void SetMenderUpgradeDesc()
    {
        menderUpgradeDesc[0] = "Regen = 25hp";
        menderUpgradeDesc[1] = "Regen = 35hp";
        menderUpgradeDesc[2] = "Regen = 60hp";
        menderUpgradeDesc[3] = "Regen = 40hp\n2x (total 80hp)";
        menderUpgradeDesc[4] = "Regen = 50hp\n2x (total 100hp)";
    }
    private void SetCocoUpgradeDesc()
    {
        cocoUpgradeDesc[0] = "Duration = 5 sec";
        cocoUpgradeDesc[1] = "Duration = 8 sec";
        cocoUpgradeDesc[2] = "Duration = 10 sec";
        cocoUpgradeDesc[3] = "Duration = 12 sec";
        cocoUpgradeDesc[4] = "Duration = 15 sec";
    }
    private void SetVerglasUpgradeDesc()
    {
        verglasUpgradeDesc[0] = "Duration = 5 sec\nSpeed = 10";
        verglasUpgradeDesc[1] = "Duration = 8 sec\nSpeed = 10";
        verglasUpgradeDesc[2] = "Duration = 8 sec\nSpeed = 8";
        verglasUpgradeDesc[3] = "Duration = 10 sec\nSpeed = 8";
        verglasUpgradeDesc[4] = "Duration = 15 sec\nSpeed = 5";
    }
    private void SetDartUpgradeDesc()
    {
        dartUpgradeDesc[0] = "Duration = 5 sec\nSpeed = 33";
        dartUpgradeDesc[1] = "Duration = 8 sec\nSpeed = 33";
        dartUpgradeDesc[2] = "Duraiton = 10 sec\nSpeed = 33";
        dartUpgradeDesc[3] = "Duration = 12 sec\nSpeed = 38";
        dartUpgradeDesc[4] = "Duration = 15 sec\nSpeed = 45";
    }
    private void SetQuashUpgradeDesc()
    {
        quashUpgradeDesc[0] = "Duration = 5 sec\nRecharge = 6";
        quashUpgradeDesc[1] = "Duration = 8 sec\nRecharge = 5";
        quashUpgradeDesc[2] = "Duration = 10 sec\nRecharge = 4";
        quashUpgradeDesc[3] = "Duration = 12 sec\nRecharge = 3";
        quashUpgradeDesc[4] = "Duration = 15 sec\nRecharge = 2";
    }
    private void SetUpgradeDescriptions()
    {
        upgradeDescriptions[0] = dotzUpgradeDesc;
        upgradeDescriptions[1] = menderUpgradeDesc;
        upgradeDescriptions[2] = cocoUpgradeDesc;
        upgradeDescriptions[3] = verglasUpgradeDesc;
        upgradeDescriptions[4] = dartUpgradeDesc;
        upgradeDescriptions[5] = quashUpgradeDesc;
    }
}
