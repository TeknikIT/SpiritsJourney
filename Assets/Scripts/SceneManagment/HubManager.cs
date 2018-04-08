using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the hubworld and transitions
/// </summary>
public class HubManager : MonoBehaviour {

    #region Singleton
    public static HubManager instance;


    private void Awake()
    {
        instance = this;
    }
    #endregion

    /// <summary>
    /// Moves to tutorial scene
    /// </summary>
    public void ToTutorialScene()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Moves to the game
    /// </summary>
    public void ToGameScene()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Moves to the hubworld
    /// </summary>
    public void ToHubworldScene()
    {
        SceneManager.LoadScene(0);
    }
}
