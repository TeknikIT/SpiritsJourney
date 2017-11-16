using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    private List<GameObject> doors;
    private List<GameObject> doorCovers;
    public bool roomCompleted = false;
    public bool roomActive = false;
    public List<GameObject> monsters;
    public Vector2 roomGridPosition;
    public int roomArrayPosition;
    // Use this for initialization
    public void Initialize(Vector2 gridPosition, int arrayPosition) {
        doors = new List<GameObject>(4);
        doorCovers = new List<GameObject>(4);
        monsters = new List<GameObject>();
        foreach(Transform door in transform.Find("Doors").transform)
        {
            doors.Add(door.gameObject);
        }
        foreach(Transform doorCover in transform.Find("CoverWalls").transform)
        {
            doorCovers.Add(doorCover.gameObject);
        }
        foreach(Transform monster in transform.Find("Monsters").transform)
        {
            monsters.Add(monster.gameObject);
        }
        foreach(GameObject m in monsters)
        {
            m.GetComponent<EnemyManager>().room = this;
        }
        roomGridPosition = gridPosition;
        roomArrayPosition = arrayPosition;
	}
	
    public void placeDoor(int direction)
    {
        doors[direction].SetActive(true);
    }

    public void placeDoorCover(int direction)
    {
        doorCovers[direction].SetActive(true);
    }

    private void Update()
    {
        //Need to remove killed character 
        if(monsters == null || monsters.Count <= 0)
        {
            roomCompleted = true;
        }
        if (roomActive)
        {
            foreach (GameObject m in monsters)
            {
                m.GetComponent<EnemyController>().isActive = true;
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
            openAllDoors();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LevelManager.SetActiveRoom(roomArrayPosition);
            roomActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
        }
    }
    public void openAllDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door.GetComponent<Animator>().gameObject.activeSelf)
                door.GetComponent<Animator>().SetBool("IsOpen", true);
        }
    }
    public void openADoor(int direction)
    {
        if (doors[direction].GetComponent<Animator>().gameObject.activeSelf)
            doors[direction].GetComponent<Animator>().SetBool("IsOpen", true);
    }
}
