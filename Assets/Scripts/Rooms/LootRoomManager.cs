using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootRoomManager : RoomManager {

    protected override void Update()
    {
        if (roomCompleted)
        {
            OpenAllDoors();
        }
    }

    protected void SpawnLoot()
    {
        Instantiate(lootManager.RandomContainerWithItem(), transform.Find("LootPoint"));
    }

    public override void Initialize(Vector2 gridPosition, int arrayPosition)
    {
        base.Initialize(gridPosition, arrayPosition);
        SpawnLoot();
    }
}
