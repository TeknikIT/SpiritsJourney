using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomManager : RoomManager {

    public static void LevelFinished()
    {
        LevelManager.instance.Reload();
    }

}
