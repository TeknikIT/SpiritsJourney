using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : RoomManager {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            roomCompleted = true;
        }
    }
    public static void LevelFinished()
    {
        TokenManager.instance.AddTokens();
        LevelManager.instance.Reload();
    }

}
