using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GlobalControl : MonoBehaviour {

    public GameStatistics savedGameStatistics = new GameStatistics();

    public bool isSceneBeingLoaded = false;
    public bool hasStartedPlaying = false;

    #region Singleton
    public static GlobalControl instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public void SaveData(string filename)
    {
        if (!Directory.Exists("Saves"))
        {
            Directory.CreateDirectory("Saves");
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/" + filename);

        PlayerManager.instance.character.SaveStatistics();
        TokenManager.instance.SaveStatistics();

        formatter.Serialize(saveFile, savedGameStatistics);

        saveFile.Close();
    }

    public void LoadData(string filename)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(filename, FileMode.Open);
        savedGameStatistics = (GameStatistics)formatter.Deserialize(saveFile);
        saveFile.Close();
    }

    public string[] DisplaySaves()
    {
        return Directory.GetFiles("Saves");
    }

}
