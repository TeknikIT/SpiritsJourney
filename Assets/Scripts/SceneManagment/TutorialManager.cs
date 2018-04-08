using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the tutorial
/// </summary>
public class TutorialManager : MonoBehaviour {

    public void ExitGame()
    {
        GlobalControl.instance.hasStartedPlaying = false;
        SceneManager.LoadScene(0);
    }
}
