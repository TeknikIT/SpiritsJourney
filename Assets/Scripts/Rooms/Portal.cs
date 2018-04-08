using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The portal in the boss room
/// </summary>
public class Portal : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Finish level
            BossRoomManager.LevelFinished();
        }
    }
}
