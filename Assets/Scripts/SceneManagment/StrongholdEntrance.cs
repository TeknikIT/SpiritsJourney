using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collider trigger to game scene
/// </summary>
public class StrongholdEntrance : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HubManager.instance.ToGameScene();
        }
    }
}
