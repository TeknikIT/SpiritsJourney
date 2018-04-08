using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logoscreen handler
/// </summary>
public class LogoScreen : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            transform.parent.GetComponent<MainMenu>().LogoScreenToMenu();
        }
	}
}
