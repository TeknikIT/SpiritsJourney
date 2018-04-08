using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Saving and loading data
/// </summary>
public class GlobalControl : MonoBehaviour {

    public GameStatistics savedGameStatistics = new GameStatistics();

    public bool isSceneBeingLoaded = false;
    public bool hasStartedPlaying = false;

    public string currentSave;

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

    /// <summary>
    /// Saving the data to a file
    /// </summary>
    /// <param name="filename">Name of the save file</param>
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

    /// <summary>
    /// Loading data from a save file
    /// </summary>
    /// <param name="filename">Name of the save file</param>
    public void LoadData(string filename)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(filename, FileMode.Open);
        savedGameStatistics = (GameStatistics)formatter.Deserialize(saveFile);
        saveFile.Close();
        currentSave = filename;
    }

    public void NotPlaying()
    {
        hasStartedPlaying = false;
    }

    /// <summary>
    /// Get all the save files in the directory
    /// </summary>
    /// <returns>The savefiles in the directory</returns>
    public string[] DisplaySaves()
    {
        return Directory.GetFiles("Saves");
    }

}
