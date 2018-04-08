using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager of the boss room
/// </summary>
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
        GameManager.instance.NewLevel();
    }

}
