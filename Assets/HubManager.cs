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

    public void ToDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
