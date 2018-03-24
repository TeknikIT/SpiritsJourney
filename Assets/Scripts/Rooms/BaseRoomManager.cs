using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoomManager : RoomManager {

    public bool LootHasDropped = false;


    protected void SpawnLoot()
    {
        Instantiate(lootManager.RandomContainerWithConsumable(), transform.Find("LootPoint"));
        LootHasDropped = true;
    }

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
        //Need to remove killed character 
        if (monsters == null || monsters.Count <= 0)
        {
            roomCompleted = true;
        }
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
