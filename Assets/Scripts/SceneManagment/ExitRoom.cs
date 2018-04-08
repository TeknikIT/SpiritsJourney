using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Registers collider and sends to hubworld
/// </summary>
public class ExitRoom : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HubManager.instance.ToHubworldScene();
        }
    }
}
