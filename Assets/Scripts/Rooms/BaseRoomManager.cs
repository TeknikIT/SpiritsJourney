using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base room, normal room
/// </summary>
public class BaseRoomManager : RoomManager {

    // Checks if the loot has been dropped
    public bool LootHasDropped = false;

    /// <summary>
    /// Spawns the loot
    /// </summary>
    protected void SpawnLoot()
    {
        Instantiate(lootManager.RandomContainerWithConsumable(), transform.Find("LootPoint"));
        LootHasDropped = true;
    }

    /// <summary>
    /// Initializes the room
    /// </summary>
    /// <param name="gridPosition">Aquiring the grid position</param>
    /// <param name="arrayPosition">Aquiring the array position</param>
    public override void Initialize(Vector2 gridPosition, int arrayPosition)
    {
        base.Initialize(gridPosition, arrayPosition);
        monsters = new List<GameObject>();
        foreach (Transform monster in transform.Find("Monsters").transform)
        {
            monsters.Add(monster.gameObject);
        }
        foreach (GameObject m in monsters)
        {
            m.GetComponent<EnemyManager>().room = this;
        }
    }

    protected override void Update()
    {
        base.Update();
        //Checks if the room has been completed
        if (monsters == null || monsters.Count <= 0)
        {
            roomCompleted = true;
        }
        //Checks if the room is active
        if (roomActive)
        {
            foreach (GameObject m in monsters)
            {
                if (!m.GetComponent<EnemyController>().hasBeenActivated)
                {
                    m.GetComponent<EnemyController>().isActive = true;
                    m.GetComponent<EnemyController>().hasBeenActivated = true;
                }
            }
        }
        else
        {
            foreach (GameObject m in monsters)
            {
                m.GetComponent<EnemyController>().isActive = false;
            }
        }
        //Checks if the room has been completed
        if (roomCompleted)
        {
            OpenAllDoors();
            if (!LootHasDropped && transform.Find("LootPoint") != null)
            {
                SpawnLoot();
            }
        }
    }
}
