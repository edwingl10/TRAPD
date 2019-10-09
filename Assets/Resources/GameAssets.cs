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

}
