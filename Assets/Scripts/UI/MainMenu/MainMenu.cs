using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    string[] saveFiles;
    GameObject saveButtonPrefab;
    public InputField fileNameField;
    public GameObject grid;
    public GameObject[] buttons;

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

    public void NewGame()
    {
        string text = fileNameField.GetComponent<InputField>().text;
        GlobalControl.instance.SaveData(text);
        GlobalControl.instance.hasStartedPlaying = true;
        HubManager.instance.ToTutorialScene();
    }

    public void LoadGame(string file)
    {
        GlobalControl.instance.LoadData(file);
        Debug.Log(file + " was loaded");
        GlobalControl.instance.hasStartedPlaying = true;
        gameObject.SetActive(false);
        Camera.main.GetComponent<CameraController>().PlayZoomAnim();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}
