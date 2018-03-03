using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void LogoScreenToMenu()
    {
        transform.Find("LogoScreen").gameObject.SetActive(false);
        transform.Find("Menu").gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        transform.parent.gameObject.SetActive(false);
        //Zoom Camera
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}
