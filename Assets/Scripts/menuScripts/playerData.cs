using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class playerData
{
    public int playerID;
    public Hashtable[] playerInfo;

    public playerData(characterSelect charsel)
    {
        playerID = charsel.index;
        playerInfo = charsel.playerInfo;
    }


}
