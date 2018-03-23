using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            BossRoomManager.LevelFinished();
        }
    }
}
