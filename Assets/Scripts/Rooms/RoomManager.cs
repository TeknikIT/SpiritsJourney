using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    private List<GameObject> doors;
    private List<GameObject> doorCovers;
    public bool roomCompleted = false;
    private List<GameObject> monsters;
    // Use this for initialization
    public void Initialize() {
        doors = new List<GameObject>(4);
        doorCovers = new List<GameObject>(4);
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
        if(monsters == null || monsters.Count <= 0)
        {
            roomCompleted = true;
        }
        if (roomCompleted)
        {
            openAllDoors();
        }
    }
    public void openAllDoors()
    {
        try
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Animator>().SetBool("IsOpen", true);
            }
        }
        catch(InvalidCastException e)
        {

        }
        
    }

}
