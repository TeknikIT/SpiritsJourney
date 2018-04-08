using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager for the loot room
/// </summary>
public class LootRoomManager : RoomManager {

    protected override void Update()
    {
        if (roomCompleted)
        {
            OpenAllDoors();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            roomCompleted = true;
        }
    }

    /// <summary>
    /// Spawn the loot
    /// </summary>
    protected void SpawnLoot()
    {
        Instantiate(lootManager.RandomContainerWithItem(), transform.Find("LootPoint"));
    }

    /// <summary>
    /// Initialize the room
    /// </summary>
    /// <param name="gridPosition">grid position</param>
    /// <param name="arrayPosition">array position</param>
    public override void Initialize(Vector2 gridPosition, int arrayPosition)
    {
        base.Initialize(gridPosition, arrayPosition);
        SpawnLoot();
    }
}
