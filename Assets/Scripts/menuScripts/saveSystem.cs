using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public static class saveSystem
{
    public static void saveLevelInfo(levelManager levelman)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/info.sunn";
        FileStream stream = new FileStream(path, FileMode.Create);

        gameData data = new gameData(levelman);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveCoinInfo(HomeManager homeMan)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/info.sunn";
        FileStream stream = new FileStream(path, FileMode.Create);

        gameData data = new gameData(homeMan);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static gameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/info.sunn";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            gameData data = formatter.Deserialize(stream) as gameData;
            stream.Close();
            return data;
        }
        else
        {
            throw new Exception("Save file not found in " + path);
        }
    }

    // ---------------------------------------------------------------- //
    public static void saveCharacterInfo(characterSelect charsel, Hashtable[] pinfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/char.sunn";
        FileStream stream = new FileStream(path, FileMode.Create);

        playerData data = new playerData(charsel, pinfo);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void saveCharacterInfo(HomeManager homeMan)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/char.sunn";
        FileStream stream = new FileStream(path, FileMode.Create);

        playerData data = new playerData(homeMan);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static playerData LoadCharacterInfo()
    {
        string path = Application.persistentDataPath + "/char.sunn";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            playerData data = formatter.Deserialize(stream) as playerData;
            stream.Close();
            return data;
        }
        else
        {
            throw new Exception("Save file not found in " + path);
        }
    }
}