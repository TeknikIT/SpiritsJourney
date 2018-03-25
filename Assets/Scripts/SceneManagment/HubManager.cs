using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour {

    #region Singleton
    public static HubManager instance;


    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void ToTutorialScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ToHubworldScene()
    {
        SceneManager.LoadScene(0);
    }
}
