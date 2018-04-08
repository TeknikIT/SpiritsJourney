using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the main menu
/// </summary>
public class MainMenu : MonoBehaviour {

    // All the saves
    string[] saveFiles;

    //Save button prefab
    GameObject saveButtonPrefab;

    //Input field for saving
    public InputField fileNameField;

    //Variables used for UI design
    public GameObject grid;
    public GameObject[] buttons;

    /// <summary>
    /// Move from logoscreen to the actuall menu
    /// </summary>
    public void LogoScreenToMenu()
    {
        transform.Find("LogoScreen").gameObject.SetActive(false);
        transform.Find("Menu").gameObject.SetActive(true);
    }

    /// <summary>
    /// Used to start the game. Has been replaced with the load function
    /// </summary>
    public void PlayGame()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (GlobalControl.instance.hasStartedPlaying == true)
        {
            gameObject.SetActive(false);
            Camera.main.GetComponent<CameraController>().PlayZoomAnim();
        }

        saveFiles = GlobalControl.instance.DisplaySaves();
        saveButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/SaveSelect");
        buttons = new GameObject[saveFiles.Length];
        for (int i = 0; i < saveFiles.Length; i++)
        {
            buttons[i] = Instantiate(saveButtonPrefab);
            buttons[i].transform.SetParent(grid.transform);
            buttons[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(saveFiles[i]);
            var clickEvent = buttons[i].GetComponent<Button>().onClick;
            int x = i;
            clickEvent.AddListener(() => LoadGame(saveFiles[x]));
        }
    }
            
        //buttons[i].GetComponent<Button>().onClick.AddListener(delegate { LoadGame(saveFiles[i]);});
    /// <summary>
    /// Create a new game
    /// </summary>
    public void NewGame()
    {
        string text = fileNameField.GetComponent<InputField>().text;
        GlobalControl.instance.SaveData(text);
        GlobalControl.instance.hasStartedPlaying = true;
        HubManager.instance.ToTutorialScene();
    }

    /// <summary>
    /// Load a save file
    /// </summary>
    /// <param name="file"></param>
    public void LoadGame(string file)
    {
        GlobalControl.instance.LoadData(file);
        Debug.Log(file + " was loaded");
        GlobalControl.instance.hasStartedPlaying = true;
        gameObject.SetActive(false);
        Camera.main.GetComponent<CameraController>().PlayZoomAnim();
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}
