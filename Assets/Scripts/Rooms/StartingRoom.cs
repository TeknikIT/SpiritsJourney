using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : RoomManager {

    private void Start()
    {
        roomCompleted = true;   
    }

    protected override void Update()
    {
        if (roomCompleted)
        {
            OpenAllDoors();
        }
    }

}
