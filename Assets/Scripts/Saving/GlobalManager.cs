﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {

    public GameObject pauseMenu;

	void Update () {
        pauseMenu = PauseMenu.instance.transform.Find("PauseMenuCanvas").gameObject;
        if (GlobalControl.instance.hasStartedPlaying)
        {
            if (pauseMenu.activeSelf && Input.GetButtonDown("Cancel"))
            {
                pauseMenu.SetActive(false);
            }
            else if (!pauseMenu.activeSelf && Input.GetButtonDown("Cancel"))
            {
                pauseMenu.SetActive(true);
            }
        }
	}
}
