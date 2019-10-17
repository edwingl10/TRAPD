using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class playerData
{
    public int playerID;
    public Hashtable[] playerInfo;

    public playerData(characterSelect charsel, Hashtable[] pinfo)
    {
        playerID = charsel.index;
        playerInfo = pinfo;
    }

    public playerData(HomeManager homeMan)
    {
        playerID = homeMan.index;
        playerInfo = homeMan.playerInfo;
    }

}
